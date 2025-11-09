using UnityEngine;
using Sirenix.OdinInspector;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：AddBuff
    /// 自动生成于 2025-11-08 21:32:56
    /// </summary>
    public class Effect_AddBuff : EffectBase
    {
        private readonly EffectParam_AddBuff _p;

        public Effect_AddBuff(IEffectParam param) : base(param)
        {
            _p = (EffectParam_AddBuff)param;
        }

        public override void OnTrigger(object caster, object target)
        {
            if (target is EnemyAgent enemy)
            {
                enemy.BuffHandler.AddBuff(_p.buffID, this);
            }
            else if (target is TowerAgent tower)
            {
                tower.BuffHandler.AddBuff(_p.buffID, this);
            }
        }
    }

    [System.Serializable]
    public class EffectParam_AddBuff : IEffectParam
    {
        [LabelText("BuffID")] public string buffID;
    }
}