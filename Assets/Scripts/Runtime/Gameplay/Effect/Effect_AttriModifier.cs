using UnityEngine;
using Sirenix.OdinInspector;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：AttriModifier
    /// 自动生成于 2025-11-06 18:29:37
    /// </summary>
    public class Effect_AttriModifier : EffectBase
    {
        private readonly EffectParam_AttriModifier _p;

        public Effect_AttriModifier(IEffectParam param) : base(param)
        {
            _p = (EffectParam_AttriModifier)param;
        }

        private object m_target;
        private AttrModifier m_attrModifier;

        public override void OnTrigger(object caster, object target)
        {
            m_target = target;
            m_attrModifier = new AttrModifier(_p.attrForge, false, _p.value);

            if (m_target is EnemyAgent enemy)
            {
                enemy.Attribute.GetAttrForge(_p.attrForge).AddModifier(m_attrModifier);
            }
            else if (m_target is TowerAgent tower)
            {
                tower.Attribute.GetAttrForge(_p.attrForge).AddModifier(m_attrModifier);
            } 
        }

        public override void OnEnd(object caster, object target)
        {
            if (m_target is EnemyAgent enemy)
            {
                enemy.Attribute.GetAttrForge(_p.attrForge).RemoveModifier(m_attrModifier);
            }
            else if (m_target is TowerAgent tower)
            {
                tower.Attribute.GetAttrForge(_p.attrForge).RemoveModifier(m_attrModifier);
            }
        }
    }

    [System.Serializable]
    public class EffectParam_AttriModifier : IEffectParam
    {
        [LabelText("属性类型")] public EAttrForge attrForge;
        [LabelText("影响值")] public float value;
    }
}