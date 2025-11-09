using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

namespace Tower.Runtime.Gameplay
{
    public enum EEnemyOppo
    {
        Die,
    }

    public class EnemyAgent : AgentEntity
    {   
        public EnemyData Data { get; protected set; }
        public override ECamp Camp => ECamp.Enemy;

        public EnemyAttribute Attribute { get; protected set; }

        private NavMeshAgent m_agent;
        private SpriteRenderer m_bcgRenderer;

        private int m_currentIndex = 0;
        private float m_reachThreshold = 0.1f;

        private Dictionary<EEnemyOppo, List<EffectBase>> m_effectDic = new Dictionary<EEnemyOppo, List<EffectBase>>();

        private List<Vector2> m_path = new List<Vector2>();
        private List<Func<float, float>> m_damage;

        public int CurrentIndex => m_currentIndex;


        public override void OnAwake()
        {
            base.OnAwake();
            m_agent = GetComponent<NavMeshAgent>();
            m_bcgRenderer = GetComponentInChildren<SpriteRenderer>();

            m_damage = new List<Func<float, float>>();
            Attribute = new EnemyAttribute();

            m_agent.updateRotation = false;
            m_agent.updateUpAxis = false;
            m_agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        }

        public async void OnSpawn(EnemyData data)
        {
            Data = data;

            Attribute.Init(Data);

            _spriteRenderer.sprite = ResKit.LoadRes<Sprite>(GlobalConst.ABName.ARTS, data.IconPath);

            m_bcgRenderer.color = Color.black;

            foreach (var array in Data.DefaultEffects)
            {
                foreach (var effect in array.Value)
                {
                    if (!m_effectDic.ContainsKey(array.Key))
                    {
                        m_effectDic[array.Key] = new List<EffectBase>();
                    }

                    m_effectDic[array.Key].Add(effect.GetInstance());
                }
            }

            m_path = SystemKit.GetSystem<LevelSystem>().Path;
            m_agent.Warp(transform.position);
            await UniTask.Delay(1000);

            MoveStart();

            m_init = true;
        }

        public void OnSpawn(EnemyData data, int index)
        {
            Data = data;

            Attribute.Init(Data);

            _spriteRenderer.sprite = ResKit.LoadRes<Sprite>(GlobalConst.ABName.ARTS, data.IconPath);

            m_bcgRenderer.color = Color.black;

            foreach (var array in Data.DefaultEffects)
            {
                foreach (var effect in array.Value)
                {
                    if (!m_effectDic.ContainsKey(array.Key))
                    {
                        m_effectDic[array.Key] = new List<EffectBase>();
                    }

                    m_effectDic[array.Key].Add(effect.GetInstance());
                }
            }

            m_path = SystemKit.GetSystem<LevelSystem>().Path;
            m_agent.Warp(transform.position);
            MoveStart(index);
            m_init = true;
        }

        public override void OnKill()
        {
            Data = null;
            _spriteRenderer.sprite = null;
            m_bcgRenderer.DOKill();
            m_damage.Clear();
            m_effectDic.Clear();
            BuffHandler.Clear();
            Attribute.ClearAllModifier();

            SystemKit.GetSystem<AgentSystem>().DestroyEntity(this);
            m_init = false;

            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!m_init && (m_path.Count == 0 || m_agent.pathPending)) 
            {
                return;
            }

            BuffHandler.UpdateBuffs(Time.deltaTime);
            m_agent.speed = Attribute.GetAttrForge(EAttrForge.Speed).CurValue<float>();
            if (!m_agent.pathPending && m_agent.remainingDistance <= m_reachThreshold)
            {
                m_currentIndex++;
                if (m_currentIndex < m_path.Count)
                {
                    m_agent.SetDestination(m_path[m_currentIndex]);
                }
                else
                {
                    SystemKit.GetSystem<LevelSystem>().Hurt();
                    OnKill();
                }
            }
        }

        public void MoveStart()
        {
            m_currentIndex = 1;
            m_agent.SetDestination(m_path[m_currentIndex]);
        }

        public void MoveStart(int index)
        {
            m_currentIndex = index;
            m_agent.SetDestination(m_path[m_currentIndex]);
        }

        private float GetActualDamage(float damage)
        {
            if (m_damage == null || m_damage.Count == 0)
            {
                return damage;
            }

            float finalDamage = damage;

            for (int i = 0; i < m_damage.Count; i++)
            {
                var modifier = m_damage[i];
                if (modifier != null)
                {
                    finalDamage = modifier(finalDamage);
                }
            }

            return finalDamage;
        }

        public void Hurt(float damage)
        {   
            float actualDamage = GetActualDamage(damage);
            float hp = Attribute.Hurt(actualDamage);

            m_bcgRenderer.DOColor(Color.white, 0.25f).OnComplete(() =>
            {
                m_bcgRenderer.DOColor(Color.black, 0.25f);
            });

            if (hp <= 0f)
            {
                TriggerEffect(EEnemyOppo.Die, this, this);
                SystemKit.GetSystem<StoreSystem>().GetMoney(Attribute.GetAttrForge(EAttrForge.Money).CurValue<int>());
                OnKill();
            }
        }

        public void AddDamageModifier(Func<float, float> func)
        {
            if (!m_damage.Contains(func))
            {
                m_damage.Add(func);
            }
        }

        public void RemoveDamageModifier(Func<float, float> func)
        {
            if (m_damage.Contains(func))
            {
                m_damage.Remove(func);
            }
        }

        public void TriggerEffect(EEnemyOppo oppo, object caster, object target)
        {
            if (m_effectDic.TryGetValue(oppo, out var effects))
            {
                foreach (var effect in effects)
                {
                    effect.OnTrigger(caster, target);
                }
            }
        }
    }
}