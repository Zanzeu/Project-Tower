using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tower.Runtime.Common;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：CreateDamageModifierAoE
    /// 自动生成于 2025-11-08 22:23:04
    /// </summary>
    public class Effect_CreateDamageModifierAoE : EffectBase
    {
        private readonly EffectParam_CreateDamageModifierAoE _p;

        public Effect_CreateDamageModifierAoE(IEffectParam param) : base(param)
        {
            _p = (EffectParam_CreateDamageModifierAoE)param;
        }

        public override void OnTrigger(object caster, object target)
        {
            List<AgentEntity> targetList = (List<AgentEntity>)target;

            PersistentAoE aoe = new PersistentAoE(caster, targetList[0].transform.position, _p.boomRadius, 2f, 0.5f);
            aoe.OnHit += (target) =>
            {
                if (target is EnemyAgent enemy)
                {
                    enemy.BuffHandler.AddBuff("buff_2", caster);
                }
            };

            GameKit.CreateAoE(aoe);
        }
    }

    [System.Serializable]
    public class EffectParam_CreateDamageModifierAoE : IEffectParam
    {
        [LabelText("爆炸半径")] public float boomRadius;
    }
}