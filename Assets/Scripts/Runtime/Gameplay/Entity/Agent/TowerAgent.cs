using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{   
    public enum ETowerOppo
    {
        Action,
    }

    public class TowerAgent : AgentEntity
    {   
        public TowerData Data { get; protected set; }
        public TowerAttribute Attribute { get; protected set; }

        public override ECamp Camp => ECamp.Tower;

        private Dictionary<ETowerOppo, List<EffectBase>> m_effectDic = new Dictionary<ETowerOppo, List<EffectBase>>();

        private float m_timer;
        private bool m_canAction;

        public override void OnAwake()
        {
            base.OnAwake();
            Attribute = new TowerAttribute();
            GetComponentInChildren<TowerView>().SetComponent(this);
        }

        public void OnSpawn(TowerData data)
        {
            Data = data;
            Attribute.Init(Data);

            _spriteRenderer.sprite = ResKit.LoadRes<Sprite>(GlobalConst.ABName.ARTS, data.IconPath);

            m_timer = Attribute.GetAttrForge(EAttrForge.HitSpeed).CurValue<float>();
            m_canAction = false;

            foreach (var array in Data.DefaultEffects)
            {
                foreach (var effect in array.Value)
                {   
                    if (!m_effectDic.ContainsKey(array.Key))
                    {
                        m_effectDic[array.Key] = new List<EffectBase>();
                    }

                    m_effectDic[array.Key] .Add(effect.GetInstance());
                }
            }

            m_init = true;
        }

        private void Update()
        {   
            if (!m_init)
            {
                return;
            }

            BuffHandler.UpdateBuffs(Time.deltaTime);

            if (Attribute.IsAttrType(EAttrType.Ice))
            {
                return;
            }

            if (m_canAction && CheckRange(out List<AgentEntity> target))
            {
                _spriteRenderer.transform.DOScale(1f, 0.25f).OnComplete(() =>
                {
                    _spriteRenderer.transform.DOScale(0.75f, 0.25f);
                });

                TriggerEffect(ETowerOppo.Action, this, target);

                m_timer = Attribute.GetAttrForge(EAttrForge.HitSpeed).CurValue<float>();
                m_canAction = false;
            }

            if (!m_canAction && m_timer > 0f)
            {
                m_timer -= Time.deltaTime;

                if (m_timer <= 0f)
                {
                    m_canAction = true;

                }
            }
        }

        public override void OnKill()
        {
            Data = null;
            _spriteRenderer.sprite = null;
            _spriteRenderer.transform.DOKill();
            m_effectDic.Clear();
            m_init = false;
            SystemKit.GetSystem<AgentSystem>().DestroyEntity(this);
            gameObject.SetActive(false);
        }

        public void LevelUp()
        {
            m_init = false;
            m_effectDic.Clear();
            OnSpawn(DataKit.GetTowerJson(Data.NextID).GetInstance());
        }

        public void TriggerEffect(ETowerOppo oppo, object caster, object target)
        {
            if (m_effectDic.TryGetValue(oppo, out var effects))
            {
                foreach (var effect in effects)
                {
                    effect.OnTrigger(caster, target);
                }
            }
        }

        public bool CheckRange(out List<AgentEntity> target)
        {
            target = SystemKit.GetSystem<AgentSystem>().GetAllAgents(ECamp.Enemy, new GetAgentsInRangeFunc(Attribute.GetAttrForge(EAttrForge.Range).CurValue<float>()), this);

            return target != null && target.Count > 0;
        }
    }

    /// <summary>
    /// 获取范围内所有敌人
    /// </summary>
    public class GetAgentsInRangeFunc : IGetAllAgentFunc
    {
        private float m_range;

        public GetAgentsInRangeFunc(float range)
        {
            m_range = range;
        }

        public List<AgentEntity> GetAllAgents(EntityBase self, List<AgentEntity> candidates)
        {
            if (self == null || candidates == null || candidates.Count == 0)
            {
                return null;
            }

            Vector3 selfPos = self.transform.position;

            var result = candidates.Where(agent => Vector2.Distance(agent.transform.position, selfPos) <= m_range)
                .OrderBy(agent => Vector2.Distance(agent.transform.position, selfPos)).ToList();
            return result.Count > 0 ? result : null;
        }
    }
}