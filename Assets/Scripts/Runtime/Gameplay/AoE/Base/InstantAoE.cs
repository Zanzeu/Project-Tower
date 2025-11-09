using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public class InstantAoE : AoEBase
    {
        public InstantAoE(object caster, Vector3 pos, float radius)
            : base(caster, pos, radius) { }

        public override void OnStart()
        {
            List<AgentEntity> allEntity = SystemKit.GetSystem<AgentSystem>().GetAllEntity();

            foreach (var target in allEntity)
            {
                if (target == null || target == Caster as AgentEntity)
                {
                    continue;
                }

                float sqrDist = (target.transform.position - _pos).sqrMagnitude;
                if (sqrDist <= Radius * Radius)
                {
                    OnHit?.Invoke(target);
                }
            }
        }

        public override void OnUpdate(float deltaTime) { }
    }
}
