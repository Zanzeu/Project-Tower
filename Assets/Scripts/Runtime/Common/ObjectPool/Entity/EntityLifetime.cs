using Sirenix.OdinInspector;
using Tower.Runtime.Common;
using UnityEngine;

namespace Tower.Runtime
{
    public class EntityLifetime : MonoBehaviour, IObjectPool
    {
        [SerializeField, LabelText("ÉúÃüÖÜÆÚ")] private float lifetime;
        private float m_timer;

        public void OnAwake()
        {

        }

        public void OnKill()
        {
            gameObject.SetActive(false);
        }


        private void OnEnable()
        {
            m_timer = lifetime;
        }

        private void Update()
        {
            m_timer -= Time.deltaTime;
            if (m_timer <= 0f)
            {
                OnKill();
            }
        }
    }
}
