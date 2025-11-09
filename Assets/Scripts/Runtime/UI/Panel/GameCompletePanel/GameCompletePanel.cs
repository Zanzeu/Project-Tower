using System.Collections;
using Tower.Runtime.ToolKit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tower.Runtime.UI
{
    public partial class GameCompletePanel : UIBase
    {
        private bool m_pause = false;
        private float m_timeScale = 0f;

        private void OnEnable()
        {
            EventKit.GameState.Subscribe(GameSystem.EGameState.Victory, new Core.EventParam(OnShowUI, 1));
            EventKit.GameState.Subscribe(GameSystem.EGameState.Defeat, new Core.EventParam(OnShowUI, 1));
        }

        private void OnDisable()
        {
            EventKit.GameState.Unsubscribe(GameSystem.EGameState.Victory, OnShowUI);
            EventKit.GameState.Unsubscribe(GameSystem.EGameState.Defeat, OnShowUI);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                OnShowPausePanel();
            }
        }


        public override void OnStart()
        {
            menuBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1f;
                Menu();
            });

            pauseMenuBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1f;
                Menu();
            });

            pauseReturnBtn.onClick.AddListener(Return);
        }

        public override void OnShowUI()
        {
            titleTMP.text = EventKit.GameState.CurrentState.ToString();
            Time.timeScale = 0f;
            mainPanel.SetActive(true);  
        }

        private void OnShowPausePanel()
        {
            m_pause = !m_pause;

            if (m_pause)
            {
                m_timeScale = Time.timeScale;
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = m_timeScale;
            }

            pausePanel.SetActive(m_pause);
        }

        private void Menu()
        {
            SceneManager.LoadScene(0);
        }

        private void Return()
        {
            OnShowPausePanel();
        }
    }
}
