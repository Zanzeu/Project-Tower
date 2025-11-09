using System.Collections.Generic;
using System.Linq;
using Tower.Runtime.Gameplay;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.GameSystem
{   
    public enum ECamp
    {
        Tower,
        Enemy,
    }

    public interface IGetAgentFunc
    {
        /// <summary>
        /// 在给定的智能体集合中选择一个符合条件的智能体
        /// </summary>
        /// <param name="self">发起请求的智能体（可为null）</param>
        /// <param name="candidates">候选智能体列表</param>
        /// <returns>目标智能体</returns>
        AgentEntity GetAgent(EntityBase self, List<AgentEntity> candidates);
    }

    /// <summary>
    /// 在给定的智能体集合中选择所有符合条件的智能体
    /// </summary>
    /// <param name="self">发起请求的智能体（可为null）</param>
    /// <param name="candidates">候选智能体列表</param>
    /// <returns>所有智能体</returns>
    public interface IGetAllAgentFunc
    {
        List<AgentEntity> GetAllAgents(EntityBase self, List<AgentEntity> candidates);
    }

    public class AgentSystem : SystemBase
    {   
        /// <summary>
        /// 所有存活的智能体
        /// </summary>
        private List<AgentEntity> _liveEntity = new List<AgentEntity>();

        /// <summary>
        /// 各阵营存活的智能体
        /// </summary>
        private Dictionary<ECamp, List<AgentEntity>> _liveCampEntity = new Dictionary<ECamp, List<AgentEntity>>();

        /// <summary>
        /// 生成智能体
        /// </summary>
        /// <param name="entity">待生成的智能体</param>
        /// <returns>智能体</returns>
        public AgentEntity SpawnEntity(AgentEntity entity)
        {   
            if (!_liveEntity.Contains(entity))
            {
                _liveEntity.Add(entity);

                ECamp camp = entity.Camp;

                if (!_liveCampEntity.ContainsKey(camp))
                {
                    _liveCampEntity[camp] = new List<AgentEntity>();
                }

                _liveCampEntity[camp].Add(entity);
            }

            return entity;
        }

        /// <summary>
        /// 销毁智能体
        /// </summary>
        /// <param name="entity">待销毁的智能体</param>
        /// <returns>智能体</returns>
        public AgentEntity DestroyEntity(AgentEntity entity)
        {
            if (_liveEntity.Contains(entity))
            {
                _liveEntity.Remove(entity);

                ECamp camp = entity.Camp;

                if (_liveCampEntity.ContainsKey(camp))
                {
                    _liveCampEntity[camp].Remove(entity);
                }

                if (SystemKit.GetSystem<LevelSystem>().LastWave && _liveCampEntity[camp].Count <= 0)
                {
                    Time.timeScale = 0f;
                    EventKit.GameState.Switch(EGameState.Victory);
                }
            }

            return entity;
        }

        /// <summary>
        /// 获取所有智能体
        /// </summary>
        /// <returns>智能体列表</returns>
        public List<AgentEntity> GetAllEntity()
        {
            return _liveEntity;
        }

        /// <summary>
        /// 获取智能体
        /// </summary>
        /// <param name="camp">阵营</param>
        /// <param name="func">自定义获取方式</param>
        /// <param name="requester">请求者</param>
        /// <returns>智能体</returns>
        public AgentEntity GetAgent(ECamp camp, IGetAgentFunc func = null, EntityBase requester = null)
        {
            if (!_liveCampEntity.TryGetValue(camp, out var list) || list.Count == 0)
            {
                return null;
            }

            if (func == null)
            {
                return list[0];
            }

            return func.GetAgent(requester, list);
        }

        public List<AgentEntity> GetAllAgents(ECamp camp, IGetAllAgentFunc func = null, EntityBase requester = null)
        {   
            if (!_liveCampEntity.TryGetValue(camp, out var list) || list.Count == 0)
            {   
                return null;
            }

            if (func != null)
            {
                return func.GetAllAgents(requester, list);
            }

            return list;
        }
    }
}