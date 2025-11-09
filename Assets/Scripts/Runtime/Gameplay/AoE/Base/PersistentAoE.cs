using System.Collections.Generic;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public class PersistentAoE : AoEBase
    {
        private float _duration;
        private float _elapsed;
        private float _interval;
        private float _elapsedInterval;

        public PersistentAoE(object caster, Vector3 pos, float radius, float duration, float interval = 0f)
            : base(caster, pos, radius)
        {
            _duration = duration;
            _interval = interval;
            _elapsed = 0f;
            _elapsedInterval = 0f;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!_isActive) return;

            _elapsed += deltaTime;
            _elapsedInterval += deltaTime;

            if (_elapsed >= _duration)
            {
                OnStop();
                return;
            }

            if (_elapsedInterval < _interval)
            {
                return;
            }

            _elapsedInterval = 0f;

            List<AgentEntity> allEntity = SystemKit.GetSystem<AgentSystem>().GetAllEntity();
            var snapshot = new List<AgentEntity>(allEntity);

            foreach (var target in snapshot)
            {
                if (target == null || target == Caster as EntityBase)
                    continue;

                if (Vector2.Distance(target.transform.position, Position) <= Radius)
                {
                    OnHit?.Invoke(target);
                }
            }
        }
    }
}
