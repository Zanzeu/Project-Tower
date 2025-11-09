using Tower.Runtime.Gameplay;

namespace Tower.Runtime
{
    public class TowerAttribute : AttributeBase<TowerData>
    {
        public TowerAttribute()
        {
            AddAttrForge(EAttrForge.Attack, new AttrForge(0));
            AddAttrForge(EAttrForge.Range, new AttrForge(0));
            AddAttrForge(EAttrForge.HitSpeed, new AttrForge(0));
        }

        public override void Init(TowerData data)
        {
            _attrForgeDic[EAttrForge.Attack].Init(data.Attack);
            _attrForgeDic[EAttrForge.Range].Init(data.Range);
            _attrForgeDic[EAttrForge.HitSpeed].Init(data.HitSpeed);
        }
    }
}
