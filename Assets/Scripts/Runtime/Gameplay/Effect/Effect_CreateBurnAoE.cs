using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：CreateBurnAoE
    /// 自动生成于 2025-11-08 22:34:55
    /// </summary>
    public class Effect_CreateBurnAoE : EffectBase
    {
        private readonly EffectParam_CreateBurnAoE _p;

        public Effect_CreateBurnAoE(IEffectParam param) : base(param)
        {
            _p = (EffectParam_CreateBurnAoE)param;
        }

        public override void OnTrigger(object caster, object target)
        {
            List<AgentEntity> targetList = (List<AgentEntity>)target;

            PersistentAoE aoe = new PersistentAoE(caster, targetList[0].transform.position, _p.boomRadius, 2f, 1f);
            aoe.OnHit += (target) =>
            {
                if (target is EnemyAgent enemy)
                {
                    enemy.BuffHandler.AddBuff("buff_3", caster);
                }
            };

            GameKit.CreateAoE(aoe);
        }
    }

    [System.Serializable]
    public class EffectParam_CreateBurnAoE : IEffectParam
    {
        [LabelText("爆炸半径")] public float boomRadius;
    }
}