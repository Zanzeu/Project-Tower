using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Tower.Runtime.Common;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：CreateIceAoE
    /// 自动生成于 2025-11-09 09:11:29
    /// </summary>
    public class Effect_CreateIceAoE : EffectBase
    {
        private readonly EffectParam_CreateIceAoE _p;

        public Effect_CreateIceAoE(IEffectParam param) : base(param)
        {
            _p = (EffectParam_CreateIceAoE)param;
        }

        public override void OnTrigger(object caster, object target)
        {
            List<AgentEntity> targetList = SystemKit.GetSystem<AgentSystem>().GetAllAgents(ECamp.Tower, new GetAgentsInRangeFunc(_p.boomRadius),caster as EntityBase);
            EnemyAgent agent = (EnemyAgent)caster;
            EntityPoolManager.Release(DataKit.GetPrefab(_p.boomVFX), agent.transform.position, Quaternion.identity, Vector2.one * _p.boomRadius * 2f);
            if (targetList != null && targetList.Count > 0)
            {
                PersistentAoE aoe = new PersistentAoE(caster, agent.transform.position, _p.boomRadius, 0.2f);
                aoe.OnHit += (target) =>
                {
                    foreach (var tower in targetList)
                    {
                        tower.BuffHandler.AddBuff("buff_5", caster);
                    }
                };

                GameKit.CreateAoE(aoe);
            }
        }
    }

    [System.Serializable]
    public class EffectParam_CreateIceAoE : IEffectParam
    {
        [LabelText("爆炸特效")] public string boomVFX;
        [LabelText("爆炸半径")] public float boomRadius;
    }
}