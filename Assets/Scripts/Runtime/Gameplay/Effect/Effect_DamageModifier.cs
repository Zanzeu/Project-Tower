using Sirenix.OdinInspector;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：DamageModifier
    /// 自动生成于 2025-11-06 17:04:13
    /// </summary>
    public class Effect_DamageModifier : EffectBase
    {
        private readonly EffectParam_DamageModifier _p;

        public Effect_DamageModifier(IEffectParam param) : base(param)
        {
            _p = (EffectParam_DamageModifier)param;
        }

        private BuffData m_buff;
        private EnemyAgent m_target;

        public override void OnTrigger(object caster, object target)
        {   
            if (target is EnemyAgent enemy && caster is BuffData buff)
            {
                m_target = enemy;
                m_buff = buff;

                m_target.AddDamageModifier(DamageChange);
            }
        }

        public override void OnEnd(object caster, object target)
        {
            if (m_buff != null && m_target != null)
            {
                m_target.RemoveDamageModifier(DamageChange);
            }
        }

        private float DamageChange(float value)
        {
            return value += (_p.value * m_buff.CurStack);
        }
    }

    [System.Serializable]
    public class EffectParam_DamageModifier : IEffectParam
    {
        [LabelText("伤害修正")] public int value;
    }
}