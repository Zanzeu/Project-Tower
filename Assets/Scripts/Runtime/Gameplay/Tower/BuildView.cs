using Tower.Runtime.Common;
using Tower.Runtime.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tower.Runtime.Gameplay
{
    public class BuildView : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            if (UIKit.IsClicked)
            {
                return;
            }

            UIKit.IsClicked = true;

            UIKit.GetUI<BuildPanel>().OnShowUI();
            UIKit.GetUI<BuildPanel>().SetView(this);
        }
    }
}
