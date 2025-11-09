using Tower.Runtime.Common;
using Tower.Runtime.ToolKit;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tower.Runtime.UI
{
    public class BuildBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string id;

        public void OnPointerEnter(PointerEventData eventData)  
        {
            UIKit.GetUI<BuildPanel>().SetPanelTMP(DataKit.GetTowerJson(id).GetInstance());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIKit.GetUI<BuildPanel>().HideDesPanel();
        }
    }
}
