using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Runtime.UI
{
    public partial class GameCompletePanel
    {
        [SerializeField,LabelText("标题TMP")] private TMP_Text titleTMP;
        [SerializeField, LabelText("主菜单按钮")] private Button menuBtn;

        [SerializeField, LabelText("暂停面板")] private GameObject pausePanel;
        [SerializeField, LabelText("主菜单按钮")] private Button pauseMenuBtn;
        [SerializeField, LabelText("返回游戏按钮")] private Button pauseReturnBtn;
    }
}
