using Sirenix.OdinInspector;
using Tower.Runtime.Gameplay;
using UnityEngine;

namespace Tower.Runtime.UI
{
    public partial class EnemyCheckPanel : UIBase
    {
        private float m_timeScale;

        public override void OnStart()
        {
            quieBtn.onClick.AddListener(OnQuitBtnClicked);
        }

        public void SetPanel(EnemyData data)
        {
            nameTMP.text = data.Name;
            healthTMP.text = data.Health.ToString();
            speedTMP.text = data.Speed.ToString();
            moneyTMP.text = data.Money.ToString();
            desTMP.text = data.Des;
        }

        public override void OnShowUI()
        {   
            base.OnShowUI();
            m_timeScale = Time.timeScale;
            Time.timeScale = 0f;
        }

        public override void OnHideUI()
        {
            base.OnHideUI();
        }

        public void OnQuitBtnClicked()
        {
            Time.timeScale = m_timeScale;
            OnHideUI();
        }
    }
}
