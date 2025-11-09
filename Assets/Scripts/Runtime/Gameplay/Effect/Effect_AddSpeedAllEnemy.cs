using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tower.Runtime.ToolKit;
using Tower.Runtime.GameSystem;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：AddSpeedAllEnemy
    /// 自动生成于 2025-11-09 08:52:17
    /// </summary>
    public class Effect_AddSpeedAllEnemy : EffectBase
    {
        private readonly EffectParam_AddSpeedAllEnemy _p;

        public Effect_AddSpeedAllEnemy(IEffectParam param) : base(param)
        {
            _p = (EffectParam_AddSpeedAllEnemy)param;
        }

        public override void OnTrigger(object caster, object target)
        {
            List<AgentEntity> agents = SystemKit.GetSystem<AgentSystem>().GetAllAgents(ECamp.Enemy);

            foreach (EnemyAgent agent in agents)
            {
                agent.BuffHandler.AddBuff("buff_4", caster);
            }
        }
    }

    [System.Serializable]
    public class EffectParam_AddSpeedAllEnemy : IEffectParam
    {

    }
}