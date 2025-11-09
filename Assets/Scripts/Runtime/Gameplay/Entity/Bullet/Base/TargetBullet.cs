using Sirenix.OdinInspector;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public class TargetBullet : BulletBase
    {
        public GameObject BulletTarget { get; private set; }

        private bool m_takeDamage;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="target">设定目标</param>
        public void OnSpawn(GameObject caster, GameObject target,bool takeDamage)
        {
            IsFinished = false;
            Caster = caster;
            BulletTarget = target;
            m_takeDamage = takeDamage;
        }

        public override void OnKill()
        {
            Caster = null; 
            IsFinished = false;
            BulletTarget = null;
            OnHit = null;
            gameObject.SetActive(false);
        }

        protected void Update()
        {
            if (IsFinished || BulletTarget == null || !BulletTarget.activeSelf)
            {
                OnKill();
                return;
            }

            Vector3 dir = (BulletTarget.transform.position - transform.position).normalized;
            float moveDist = speed * Time.deltaTime;

            transform.position += dir * moveDist;

            if (Vector2.Distance(transform.position, BulletTarget.transform.position) <= 0.05f)
            {
                Hit();
            }
        }

        protected override void Hit()
        {   
            if (m_takeDamage)
            {
                BulletTarget.GetComponent<EnemyAgent>()
                .Hurt(Caster.GetComponent<TowerAgent>()
                .Attribute.GetAttrForge(EAttrForge.Attack).CurValue<int>());
            }

            IsFinished = true;
            OnHit?.Invoke(this);
        }
    }
}