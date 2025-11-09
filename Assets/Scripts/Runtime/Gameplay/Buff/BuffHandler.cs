using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public class BuffHandler
    {
        [ShowInInspector] private Dictionary<string, BuffData> _activeBuffs = new Dictionary<string, BuffData>();
        private List<string> _buffsToRemove = new List<string>();
        private List<BuffData> _buffsToAdd = new List<BuffData>();
        private bool _isUpdating = false;

        //自身实体
        private EntityBase _entity;

        public BuffHandler(EntityBase entity)
        {
            _entity = entity;
        }

        public void Clear()
        {
            _activeBuffs.Clear();
            _buffsToRemove.Clear();
            _buffsToAdd.Clear();
            _isUpdating = false;
        }

        /// <summary>
        /// 查询buff
        /// </summary>
        /// <param name="buffId">buff id</param>
        /// <returns>buff</returns>
        public BuffData CheckBuff(string buffId)
        {
            if (_activeBuffs.TryGetValue(buffId, out var res))
            {
                return res;
            }

            return null;
        }

        /// <summary>
        /// 增加buff，如果存在则添加层数
        /// </summary>
        /// <param name="json">buff 模板</param>
        /// <param name="stackCount">默认层数</param>
        public void AddBuff(BuffJson json,object caster ,int stackCount = 1)
        {
            if (_isUpdating)
            {
                _buffsToAdd.Add(json.GetInstance(caster, _entity));
                return;
            }

            if (_activeBuffs.ContainsKey(json.ID))
            {
                BuffData existingBuff = _activeBuffs[json.ID];
                if (existingBuff.MaxStack != -1)
                {
                    existingBuff.OnStack(stackCount);
                }
            }
            else
            {
                BuffData newBuff = json.GetInstance(caster, _entity);

                if (newBuff.MaxStack != -1)
                {
                    newBuff.DefaultStack(stackCount);
                }

                _activeBuffs.Add(newBuff.ID, newBuff);
                newBuff.OnAwake();
            }
        }

        /// <summary>
        /// 增加buff，如果存在则添加层数
        /// </summary>
        /// <param name="buffConfig">buff id</param>
        /// <param name="stackCount">默认层数</param>
        public void AddBuff(string buffId, object caster, int stackCount = 1)
        {   
            if (_entity is EnemyAgent enemy)
            {
                if (enemy.Data.ImmuneBuff != null && enemy.Data.ImmuneBuff.Contains(buffId))
                {
                    return;
                }
            }

            AddBuff(DataKit.GetBuffJson(buffId), caster, stackCount);
        }

        /// <summary>
        /// 移除buff
        /// </summary>
        /// <param name="buffID">buff id</param>
        public void RemoveBuff(string buffID)
        {
            if (_isUpdating)
            {
                _buffsToRemove.Add(buffID);
                return;
            }

            if (_activeBuffs.ContainsKey(buffID))
            {
                BuffData buff = _activeBuffs[buffID];
                buff.OnEnd();
                _activeBuffs.Remove(buffID);
            }
        }

        /// <summary>
        /// 添加buff层数
        /// </summary>
        /// <param name="buffID">buff id</param>
        /// <param name="count">添加层数</param>
        public void AddBuffStack(string buffID,object caster ,int count)
        {
            if (_activeBuffs.ContainsKey(buffID))
            {
                BuffData existingBuff = _activeBuffs[buffID];
                if (existingBuff.MaxStack != -1)
                {
                   existingBuff.OnStack(count);
                }
            }
            else
            {
                AddBuff(DataKit.GetBuffJson(buffID), caster, count);
            }
        }

        /// <summary>
        /// 移除buff层数，如果为0，则销毁
        /// </summary>
        /// <param name="buffID">buff id</param>
        /// <param name="count">移除层数</param>
        public void RemoveBuffStack(string buffID, int count)
        {
            if (_activeBuffs.ContainsKey(buffID))
            {
                BuffData buff = _activeBuffs[buffID];
                bool zero = buff.RemoveStack(count);

                if (zero)
                {
                    RemoveBuff(buffID);
                }
            }
        }

        /// <summary>
        /// 更新buff
        /// </summary>
        /// <param name="deltaTime">时间</param>
        public void UpdateBuffs(float deltaTime)
        {
            _isUpdating = true;
            _buffsToRemove.Clear();

            foreach (var buffPair in _activeBuffs.ToArray())
            {   
                BuffData buff = buffPair.Value;

                if (buff.IsPermanent)
                {
                    continue;
                }

                buff.LastTime -= deltaTime;
                if (buff.LastTime <= 0)
                {
                    _buffsToRemove.Add(buff.ID);
                    continue;
                }

                if (buff.TickInterval != -1 && buff.TickTimer <= 0)
                {
                    buff.OnTick();
                    buff.TickTimer = buff.TickInterval;
                }
                else
                {
                    buff.TickTimer -= deltaTime;
                }
            }

            _isUpdating = false;

            foreach (var buffID in _buffsToRemove)
            {
                RemoveBuff(buffID);
            }

            foreach (var buff in _buffsToAdd)
            {
                if (!_activeBuffs.ContainsKey(buff.ID))
                {
                    _activeBuffs.Add(buff.ID, buff);
                    buff.OnAwake();
                }
            }

            _buffsToAdd.Clear();
        }

        /// <summary>
        /// 强制执行触发buff
        /// </summary>
        /// <param name="buffID">buff id</param>
        public void ImmediateTick(string buffID)
        {
            if (_activeBuffs.ContainsKey(buffID))
            {
                _activeBuffs[buffID].ImmediateTick();
            }
        }
    }
}