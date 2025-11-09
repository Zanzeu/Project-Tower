using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tower.Runtime.Common;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：ReleasePrefab
    /// 自动生成于 2025-11-09 08:22:48
    /// </summary>
    public class Effect_ReleasePrefab : EffectBase
    {
        private readonly EffectParam_ReleasePrefab _p;

        public Effect_ReleasePrefab(IEffectParam param) : base(param)
        {
            _p = (EffectParam_ReleasePrefab)param;
        }

        public override void OnTrigger(object caster, object target)
        {
            EnemyAgent enemy = caster as EnemyAgent;
            EnemyAgent agent = EntityPoolManager
                                .Release(DataKit.GetPrefab(GlobalConst.PrefabName.AGENT_ENEMY), enemy.transform.position)
                                .GetComponent<EnemyAgent>();

            agent.OnSpawn(DataKit.GetEnemyJson(_p.prefabName).GetInstance(), enemy.CurrentIndex);

            SystemKit.GetSystem<AgentSystem>().SpawnEntity(agent);  
        }
    }

    [System.Serializable]
    public class EffectParam_ReleasePrefab : IEffectParam
    {
        [LabelText("ID")] public string prefabName;
    }
}