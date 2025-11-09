using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tower.Runtime.Common;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：CreateSlowAoE
    /// 自动生成于 2025-11-08 21:50:14
    /// </summary>
    public class Effect_CreateSlowAoE : EffectBase
    {
        private readonly EffectParam_CreateSlowAoE _p;

        public Effect_CreateSlowAoE(IEffectParam param) : base(param)
        {
            _p = (EffectParam_CreateSlowAoE)param;
        }

        public override void OnTrigger(object caster, object target)
        {
            List<AgentEntity> targetList = (List<AgentEntity>)target;

            PersistentAoE aoe = new PersistentAoE(caster, targetList[0].transform.position, _p.boomRadius, 2f, 0.5f);
            EntityPoolManager.Release(DataKit.GetPrefab(_p.boomVFX), targetList[0].transform.position, Quaternion.identity, Vector2.one * _p.boomRadius * 2f);
            aoe.OnHit += (target) =>
            {
                if (target is EnemyAgent enemy)
                {
                    enemy.BuffHandler.AddBuff("buff_1", caster);
                }
            };

            GameKit.CreateAoE(aoe);
        }
    }

    [System.Serializable]
    public class EffectParam_CreateSlowAoE : IEffectParam
    {
        [LabelText("爆炸半径")] public float boomRadius;
        [LabelText("爆炸特效名称")] public string boomVFX;
    }
}