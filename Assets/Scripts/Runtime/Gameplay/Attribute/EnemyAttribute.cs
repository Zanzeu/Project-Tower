namespace Tower.Runtime.Gameplay
{
    public class EnemyAttribute : AttributeBase<EnemyData>
    {   
        public EnemyAttribute()
        {
            AddAttrForge(EAttrForge.Health, new AttrForge(0));
            AddAttrForge(EAttrForge.Money, new AttrForge(0));
            AddAttrForge(EAttrForge.Speed, new AttrForge(0));
        }

        public override void Init(EnemyData data)
        {
            _attrForgeDic[EAttrForge.Health].Init(data.Health);
            _attrForgeDic[EAttrForge.Money].Init(data.Money);
            _attrForgeDic[EAttrForge.Speed].Init(data.Speed);
        }

        public int Hurt(float damage)
        {
            return _attrForgeDic[EAttrForge.Health].UpdateCurValue<int>(-damage);
        }
    }
}