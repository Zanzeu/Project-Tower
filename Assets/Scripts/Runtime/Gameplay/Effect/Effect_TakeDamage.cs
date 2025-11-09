using UnityEngine;
using Sirenix.OdinInspector;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：TakeDamage
    /// 自动生成于 2025-11-08 22:29:27
    /// </summary>
    public class Effect_TakeDamage : EffectBase
    {
        private readonly EffectParam_TakeDamage _p;

        public Effect_TakeDamage(IEffectParam param) : base(param)
        {
            _p = (EffectParam_TakeDamage)param;
        }

        public override void OnTrigger(object caster, object target)
        {
            if (target is EnemyAgent enemy)
            {
                enemy.Hurt(_p.damage * ((BuffData)caster).CurStack);
            }
        }
    }

    [System.Serializable]
    public class EffectParam_TakeDamage : IEffectParam
    {
        [LabelText("伤害")] public float damage;
    }
}