using System;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public abstract class AoEBase
    {
        public object Caster { get; protected set; }
        public float Radius { get; protected set; }
        public Vector3 Position => _pos;
        public bool IsActive => _isActive;

        protected Vector3 _pos;
        protected bool _isActive;

        public Action<AgentEntity> OnHit;

        protected AoEBase(object caster, Vector3 pos, float radius)
        {
            Caster = caster;
            _pos = pos;
            Radius = radius;
        }

        public virtual void OnStart() => _isActive = true;
        public virtual void OnStop() => _isActive = false;

        public abstract void OnUpdate(float deltaTime);
    }
}