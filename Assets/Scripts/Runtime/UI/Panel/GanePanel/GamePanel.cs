using System;
using System.Collections;
using System.Collections.Generic;
using Tower.Runtime.Common;
using Tower.Runtime.Gameplay;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.UI
{
    public partial class GamePanel : UIBase
    {
        private bool m_quick = false;

        private void OnEnable()
        {
            EventKit.GlobalEvent.Subscribe(Core.EGlobalEvent.OnHurt, new Core.EventParam<int>(OnUpdateHPTMP, 1));
            EventKit.GlobalEvent.Subscribe(Core.EGlobalEvent.OnUpdateMoneyUI, new Core.EventParam<int>(OnUpdateMoneyTMP, 2));
        }

        private void OnDisable()
        {
            EventKit.GlobalEvent.Unsubscribe<int>(Core.EGlobalEvent.OnHurt, OnUpdateHPTMP);
            EventKit.GlobalEvent.Unsubscribe<int>(Core.EGlobalEvent.OnUpdateMoneyUI, OnUpdateMoneyTMP);
        }

        public override void OnStart()
        {
            spawnPoint.onClick.AddListener(OnSpawnPointClicked);
            gameSpeedBtn.onClick.AddListener(OnGameSpeedBtnClicked);
        }

        #region ==========生成点==========

        private void OnSpawnPointClicked()
        {
            SystemKit.GetSystem<LevelSystem>().SpawnEnemy();
        }


        public void ShowSpawnPoint(int time)
        {
            spawnPoint.gameObject.SetActive(true);
            spawnPointTimeTMP.text = time.ToString();
        }

        public void UpdateSpawnPointTime(int time)
        {
            spawnPointTimeTMP.text = time.ToString();
            if (time <= 0)
            {
                spawnPoint.gameObject.SetActive(false);
            }
        }

        #endregion

        #region ==========生命值==========

        private void OnUpdateHPTMP(int value)
        {
            liftTMP.text = value.ToString();
        }

        #endregion

        #region ==========游戏速度==========

        public void OnGameSpeedBtnClicked()
        {
            m_quick = !m_quick;
            Time.timeScale = m_quick ? 1f : 2f;
            gameSpeedTMP.text = m_quick ? "x1" : "x2";
        }

        #endregion

        #region ==========金币==========

        private void OnUpdateMoneyTMP(int value)
        {
            moneyTMP.text = value.ToString();
        }

        #endregion

        public void SetEnenyIcon(EnemyData data)
        {
            EnemyCheckCell cell = UIPoolManager.Release(DataKit.GetPrefab("EnemyCheckCell"), checkEnemyParent).GetComponent<EnemyCheckCell>();
            cell.OnSpawn(data);
        }
    }
}
