using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public class DirectionBullet : BulletBase
    {
        private Vector3 _direction;

        public void OnSpawn(GameObject caster, Vector3 direction)
        {
            Caster = caster;
            _direction = direction;
        }

        private void Update()
        {
            if (IsFinished)
            {
                return;
            }

            transform.position += _direction * speed * Time.deltaTime;
        }

        public override void OnKill()
        {
            Caster = null;

            IsFinished = false;
            _direction = Vector3.zero;

            gameObject.SetActive(false);
        }
    }
}