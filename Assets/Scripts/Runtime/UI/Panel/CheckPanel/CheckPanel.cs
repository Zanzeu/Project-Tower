using Sirenix.OdinInspector;
using Tower.Runtime.Common;
using Tower.Runtime.Gameplay;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Runtime.UI
{
    public partial class CheckPanel : UIBase
    {
        private TowerAgent m_towerAgent;
        [SerializeField, LabelText("Éý¼¶Í¼±ê")] private Image icon;

        public override void OnStart()
        {
            levelupBtn.onClick.AddListener(OnLevelupBtnClicked);
        }

        public override void OnHideUI()
        {
            m_towerAgent = null;

            mainPanel.SetActive(false);
        }


        public void SetAgent(TowerAgent agent)
        {   
            transform.position = agent.transform.position;
            m_towerAgent = agent;
            TowerJson json = DataKit.GetTowerJson(agent.Data.NextID);
            icon.sprite = ResKit.LoadRes<Sprite>(GlobalConst.ABName.ARTS, json.IconPath);
            costTMP.text = json.Cost.ToString();
            SetPanelTMP(json.GetInstance());
        }

        private void OnLevelupBtnClicked()
        {
            if (!SystemKit.GetSystem<StoreSystem>().CostMoney(DataKit.GetTowerJson(m_towerAgent.Data.NextID).Cost))
            {
                return;
            }

            m_towerAgent.LevelUp();
            UIKit.IsClicked = false;
            OnHideUI();
        }

        public void SetPanelTMP(TowerData data)
        {
            attackTMP.text = data.Attack.ToString();
            hitspeedTMP.text = data.HitSpeed.ToString();
            rangeTMP.text = data.Range.ToString();
            dpsTMP.text = (data.Attack / data.HitSpeed).ToString("F1");
            desTMP.text = data.Des;
        }
    }
}
