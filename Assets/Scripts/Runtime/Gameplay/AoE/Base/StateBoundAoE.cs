using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public class StateBoundAoE : AoEBase
    {
        private Transform _followTarget;
        private float _interval;
        private float _intervalTimer;

        public StateBoundAoE(object caster, Transform followTarget, float radius, float interval = 0.3f)
            : base(caster, Vector3.zero, radius)
        {
            _followTarget = followTarget;
            _interval = interval;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!_isActive || _followTarget == null)
            {
                OnStop();
                return;
            }

            _pos = _followTarget.position;
            _intervalTimer += deltaTime;

            if (_intervalTimer >= _interval)
            {
                _intervalTimer = 0f;
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
        }
    }
}