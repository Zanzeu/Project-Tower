using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Runtime.UI
{
    public partial class EnemyCheckPanel
    {
        [SerializeField, LabelText("生命值TMP")] private TMP_Text healthTMP;
        [SerializeField, LabelText("速度TMP")] private TMP_Text speedTMP;
        [SerializeField, LabelText("名字TMP")] private TMP_Text nameTMP;
        [SerializeField, LabelText("金钱TMP")] private TMP_Text moneyTMP;
        [SerializeField, LabelText("描述TMP")] private TMP_Text desTMP;
        [SerializeField, LabelText("退出")] private Button quieBtn;
    }
}
