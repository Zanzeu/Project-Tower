using Tower.Runtime.Common;
using Tower.Runtime.Gameplay;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tower.Runtime.UI
{
    public partial class BuildPanel : UIBase
    {
        private BuildView _buildView;

        public void SetView(BuildView view)
        {
            transform.position = view.transform.position;
            _buildView = view;
        }

        private BuildSystem _buildSystem;

        public override void OnStart()
        {
            targetTowerBtn.onClick.AddListener(OnTargetTowerBtnClicked);
            deBuffTowerBtn.onClick.AddListener(OnDeBuffTowerBtnClicked);
            aoeTowerBtn.onClick.AddListener(OnAoETowerBtnClicked);

            targetTowerCostTMP.text = DataKit.GetTowerJson("tower_1_level_1").Cost.ToString();
            aoeTowerCostTMP.text = DataKit.GetTowerJson("tower_2_level_1").Cost.ToString();
            deBuffTowerCostTMP.text = DataKit.GetTowerJson("tower_3_level_1").Cost.ToString();

            _buildSystem = SystemKit.GetSystem<BuildSystem>();
        }

        private void Update()
        {
            if (!UIKit.IsClicked)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    UIKit.GetUI<BuildPanel>().OnHideUI();
                    UIKit.GetUI<CheckPanel>().OnHideUI();
                    UIKit.IsClicked = false;
                }
            }
        }

        public override void OnHideUI()
        {
            _buildView = null;

            mainPanel.SetActive(false);
        }

        private void OnTargetTowerBtnClicked()
        {   
            if (!SystemKit.GetSystem<StoreSystem>().CostMoney(DataKit.GetTowerJson("tower_1_level_1").Cost))
            {
                return;
            }

            _buildSystem.BuildTower("tower_1_level_1", _buildView.transform.position);
            _buildView.transform.parent.parent.gameObject.SetActive(false);
            OnHideUI();
            UIKit.IsClicked = false;
        }

        private void OnDeBuffTowerBtnClicked()
        {
            if (!SystemKit.GetSystem<StoreSystem>().CostMoney(DataKit.GetTowerJson("tower_2_level_1").Cost))
            {
                return;
            }

            _buildSystem.BuildTower("tower_2_level_1", _buildView.transform.position);
            _buildView.transform.parent.parent.gameObject.SetActive(false);
            OnHideUI();
            UIKit.IsClicked = false;
        }

        private void OnAoETowerBtnClicked()
        {
            if (!SystemKit.GetSystem<StoreSystem>().CostMoney(DataKit.GetTowerJson("tower_3_level_1").Cost))
            {
                return;
            }

            _buildSystem.BuildTower("tower_3_level_1", _buildView.transform.position);
            _buildView.transform.parent.parent.gameObject.SetActive(false);
            OnHideUI();
            UIKit.IsClicked = false;
        }

        public void SetPanelTMP(TowerData data)
        {
            attackTMP.text = data.Attack.ToString();
            hitspeedTMP.text = data.HitSpeed.ToString();
            rangeTMP.text = data.Range.ToString();
            dpsTMP.text = (data.Attack / data.HitSpeed).ToString("F1");
            desTMP.text = data.Des;

            desPanel.SetActive(true);
        }

        public void HideDesPanel()
        {
            desPanel.SetActive(false);
        }
    }
}
