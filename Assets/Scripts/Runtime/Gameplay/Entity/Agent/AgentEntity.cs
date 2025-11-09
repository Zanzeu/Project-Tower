using Sirenix.OdinInspector;
using Tower.Runtime.Common;
using Tower.Runtime.GameSystem;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public class AgentEntity : EntityBase, IAgent, IObjectPool 
    {
        /// <summary>
        /// 智能体的阵营
        /// </summary>
        public virtual ECamp Camp { get; protected set; }

        [SerializeField, LabelText("图标渲染器")] protected SpriteRenderer _spriteRenderer;

        protected bool m_init;
        public BuffHandler BuffHandler { get; protected set; }

        public virtual void OnAwake()
        {
            m_init = false;
            BuffHandler = new BuffHandler(this);
        }

        public virtual void OnKill()
        {
            gameObject.SetActive(false);
        }
    }
}