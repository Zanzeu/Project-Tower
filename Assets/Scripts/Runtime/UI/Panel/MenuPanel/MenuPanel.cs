using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using TMPro;
using Tower.Runtime.Common;
using Tower.Runtime.Gameplay;
using Tower.Runtime.ToolKit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tower.Runtime.UI
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private Button startBtn;
        [SerializeField] private Button enemyBtn;
        [SerializeField] private Transform enemyParent;
        [SerializeField] private Button quitBtn;
        [SerializeField] private GameObject enemyPanel;

        [SerializeField, LabelText("生命值TMP")] private TMP_Text healthTMP;
        [SerializeField, LabelText("速度TMP")] private TMP_Text speedTMP;
        [SerializeField, LabelText("名字TMP")] private TMP_Text nameTMP;
        [SerializeField, LabelText("金钱TMP")] private TMP_Text moneyTMP;
        [SerializeField, LabelText("描述TMP")] private TMP_Text desTMP;
        [SerializeField, LabelText("退出")] private Button quitCheckBtn;
        [SerializeField, LabelText("查看面板")] private GameObject checkPanel;

        public async void Start()
        {
            Screen.SetResolution(2560, 1440, false);

            SaveKit.Load();

            startBtn.onClick.AddListener(OnStartBtnClicked);
            enemyBtn.onClick.AddListener(OnEnemyBtnClicked);
            quitBtn.onClick.AddListener(() =>
            {
                enemyPanel.SetActive(false);
            });

            quitCheckBtn.onClick.AddListener(() =>
            {
                checkPanel.SetActive(false);
            });

            await UniTask.Delay(1000);

            var list = DataKit.GetEnemyList();
            int size = list.Count;

            for (int i = 0; i < size; i++)
            {
                EnemyCell cell = UIPoolManager.Release(DataKit.GetPrefab("enemyCell"), enemyParent).GetComponent<EnemyCell>();
                cell.OnSpawn(list[i].GetInstance());
            }
        }

        private void OnEnable()
        {
            EventKit.GlobalEvent.Subscribe(Core.EGlobalEvent.OnCheckEnemy, new Core.EventParam<EnemyData>(SetCheckPanel, 1));
        }

        private void OnDisable()
        {
            EventKit.GlobalEvent.Unsubscribe<EnemyData>(Core.EGlobalEvent.OnCheckEnemy, SetCheckPanel);
        }


        private void OnStartBtnClicked()
        {
            SceneManager.LoadScene(1);
        }

        private void OnEnemyBtnClicked()
        {
            enemyPanel.SetActive(true);
        }

        public void SetCheckPanel(EnemyData data)
        {
            healthTMP.text = data.Health.ToString();
            speedTMP.text = data.Speed.ToString();
            nameTMP.text = data.Name.ToString();
            moneyTMP.text = data.Money.ToString();
            desTMP.text = data.Des;

            checkPanel.SetActive(true);
        }
    }
}
