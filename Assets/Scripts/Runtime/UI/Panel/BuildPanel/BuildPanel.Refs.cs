using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Runtime.UI
{
    public partial class BuildPanel
    {
        [SerializeField, LabelText("瞄准塔建造按钮")] private Button targetTowerBtn;
        [SerializeField,LabelText("瞄准建筑价格TMP")] private TMP_Text targetTowerCostTMP;

        [SerializeField, LabelText("减益塔建造按钮")] private Button deBuffTowerBtn;
        [SerializeField, LabelText("减益塔建造价格TMP")] private TMP_Text deBuffTowerCostTMP;

        [SerializeField, LabelText("范围塔建造按钮")] private Button aoeTowerBtn;
        [SerializeField, LabelText("范围塔建价格TMP")] private TMP_Text aoeTowerCostTMP;

        [SerializeField, LabelText("攻击力TMP")] private TMP_Text attackTMP;
        [SerializeField, LabelText("攻速TMP")] private TMP_Text hitspeedTMP;
        [SerializeField, LabelText("范围TMP")] private TMP_Text rangeTMP;
        [SerializeField, LabelText("dpsTMP")] private TMP_Text dpsTMP;
        [SerializeField, LabelText("描述TMP")] private TMP_Text desTMP;
        [SerializeField, LabelText("描述面板")] private GameObject desPanel;
    }
}
