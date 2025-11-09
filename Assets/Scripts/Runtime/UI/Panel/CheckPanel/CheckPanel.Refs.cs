using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Runtime.UI  
{
    public partial class CheckPanel
    {
        [SerializeField, LabelText("升级建造按钮")] private Button levelupBtn;
        [SerializeField, LabelText("价格TMP")] private TMP_Text costTMP;

        [SerializeField, LabelText("攻击力TMP")] private TMP_Text attackTMP;
        [SerializeField, LabelText("攻速TMP")] private TMP_Text hitspeedTMP;
        [SerializeField, LabelText("范围TMP")] private TMP_Text rangeTMP;
        [SerializeField, LabelText("dpsTMP")] private TMP_Text dpsTMP;
        [SerializeField, LabelText("描述TMP")] private TMP_Text desTMP;
    }
}
