using Sirenix.OdinInspector;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：ChangeAttrType
    /// 自动生成于 2025-11-09 09:12:09
    /// </summary>
    public class Effect_ChangeAttrType : EffectBase
    {
        private readonly EffectParam_ChangeAttrType _p;

        public Effect_ChangeAttrType(IEffectParam param) : base(param)
        {
            _p = (EffectParam_ChangeAttrType)param;
        }

        private object m_target;

        public override void OnTrigger(object caster, object target)
        {
            m_target = target;

            if (m_target is EnemyAgent enemy)
            {
                enemy.Attribute.IncreaseAttrType(_p.attrType, _p.stack);
            }
            else if (m_target is TowerAgent tower)
            {
                tower.Attribute.IncreaseAttrType(_p.attrType, _p.stack);
            }
        }

        public override void OnEnd(object caster, object target)
        {
            if (m_target is EnemyAgent enemy)
            {
                enemy.Attribute.DecreaseAttrType(_p.attrType);
            }
            else if (m_target is TowerAgent tower)
            {
                tower.Attribute.DecreaseAttrType(_p.attrType);
            }
        }
    }

    [System.Serializable]
    public class EffectParam_ChangeAttrType : IEffectParam
    {
        [LabelText("标签类型")] public EAttrType attrType;
        [LabelText("是否可叠加")] public bool stack;
    }
}