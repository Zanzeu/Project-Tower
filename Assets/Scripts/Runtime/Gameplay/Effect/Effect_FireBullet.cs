using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Tower.Runtime.Common;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：FireBullet
    /// 自动生成于 2025-11-05 20:27:39
    /// </summary>
    public class Effect_FireBullet : EffectBase
    {
        private readonly EffectParam_FireBullet _p;

        public Effect_FireBullet(IEffectParam param) : base(param)
        {
            _p = (EffectParam_FireBullet)param;
        }

        public override void OnTrigger(object caster, object target)
        {   
            List<AgentEntity> targets = target as List<AgentEntity>;
            EntityBase entity = (EntityBase)caster;
            HashSet<EnemyAgent> agentSet = new HashSet<EnemyAgent>();

            int count = Mathf.Min(_p.targetCount, targets.Count);
            for (int i = 0; i < count; i++)
            {
                TargetBullet bullet = EntityPoolManager.Release(DataKit.GetPrefab(_p.bulletName), entity.transform.position).GetComponent<TargetBullet>();
                bullet.OnSpawn(entity.gameObject, targets[i].gameObject, _p.takeDamage);

                if (_p.boom)
                {
                    bullet.OnHit += (self) =>
                    {
                        PersistentAoE aoe = new PersistentAoE(bullet, bullet.transform.position, _p.boomRadius, 0.2f);
                        EntityPoolManager.Release(DataKit.GetPrefab(_p.boomVFX), bullet.transform.position, Quaternion.identity, Vector2.one * _p.boomRadius * 2f); 
                        aoe.OnHit += (target) =>
                        {  
                            if (target is EnemyAgent enemy)
                            {
                                if (!agentSet.Contains(enemy))
                                {
                                    agentSet.Add(enemy);
                                    enemy.Hurt((caster as TowerAgent).Attribute.GetAttrForge(EAttrForge.Attack).CurValue<int>() * _p.boomRate);
                                }   
                            }
                        };

                        GameKit.CreateAoE(aoe);
                    };
                }

                SystemKit.GetSystem<BulletSystem>().FireBullet(bullet);
            }
        }
    }

    [System.Serializable]
    public class EffectParam_FireBullet : IEffectParam
    {
        [LabelText("子弹名称")] public string bulletName;
        [LabelText("目标数量")] public int targetCount;
        [LabelText("是否造成伤害")] public bool takeDamage;
        [LabelText("是否爆炸")] public bool boom;
        [LabelText("爆炸半径"), ShowIf(nameof(boom))] public float boomRadius;
        [LabelText("爆炸伤害倍率"), ShowIf(nameof(boom))] public float boomRate;
        [LabelText("爆炸特效名称"), ShowIf(nameof(boom))] public string boomVFX;
    }
}