using Tower.Runtime.Common;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tower.Runtime.UI
{   
    public interface IGameUI 
    {
        void OnAwake();
        void OnStart();
        void OnShowUI();
        void OnHideUI();
    }

    public abstract class UIBase : MonoBehaviour , IGameUI
    {
        [SerializeField, LabelText("Ö÷Ãæ°å")] protected GameObject mainPanel;

        private void Awake()
        {   
            UIKit.RegisterUI(this);
            OnAwake();
        }
        private void Start()
        {
            OnStart();
        }

        public virtual void OnAwake() { }

        public virtual void OnStart() { }

        public virtual void OnShowUI() 
        {   
            if (mainPanel == null)
            {
                gameObject.SetActive(true);
                return ; 
            }

            mainPanel.SetActive(true);
        }

        public virtual void OnHideUI() 
        {
            if (mainPanel == null)
            {
                gameObject.SetActive(false);
                return;
            }

            mainPanel.SetActive(false);
        }
    }
}
