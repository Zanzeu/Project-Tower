using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Runtime.UI
{
    public partial class GamePanel
    {
        [SerializeField, LabelText("生成点按钮"), FoldoutGroup("生成点")] private Button spawnPoint;
        [SerializeField, LabelText("生成点时间TMP"), FoldoutGroup("生成点")] private TMP_Text spawnPointTimeTMP;

        [SerializeField, LabelText("生命值TMP"), FoldoutGroup("生命值")] private TMP_Text liftTMP;

        [SerializeField, LabelText("速度按钮"), FoldoutGroup("速度设置")] private Button gameSpeedBtn;
        [SerializeField, LabelText("速度TMP"), FoldoutGroup("速度设置")] private TMP_Text gameSpeedTMP;

        [SerializeField, LabelText("金币TMP"), FoldoutGroup("金币")] private TMP_Text moneyTMP;
        [SerializeField, LabelText("敌人查看"), FoldoutGroup("其他")] private Transform checkEnemyParent;
    }
}
