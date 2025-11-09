using Tower.Runtime.Common;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Tower.Runtime.Gameplay
{
    public class BulletBase : EntityBase, IObjectPool
    {   
        public GameObject Caster { get; protected set; }
        public bool IsFinished { get; protected set; } = false;

        [SerializeField, LabelText("ËÙ¶È")] public float speed;
        [HideInInspector] public Action<BulletBase> OnHit;

        public virtual void OnAwake()
        {

        }

        public virtual void OnKill()
        {
            Caster = null;
            IsFinished = false;

            gameObject.SetActive(false);
        }

        protected virtual void Hit()
        {
            IsFinished = true;
            OnHit?.Invoke(this);
        }
    }
}