using Tower.Runtime.Common;
using Tower.Runtime.ToolKit;
using Tower.Runtime.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tower.Runtime.Gameplay
{
    public class TowerView : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {   
        private TowerAgent m_agent;
        private GameObject m_checkRange;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (UIKit.IsClicked || m_agent.Data.NextID == "MAX")
            {
                return;
            }

            UIKit.IsClicked = true;

            UIKit.GetUI<CheckPanel>().OnShowUI();
            UIKit.GetUI<CheckPanel>().SetAgent(m_agent);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_checkRange = EntityPoolManager.Release(DataKit.GetPrefab("CheckRange"), transform.position, Quaternion.identity, Vector2.one * m_agent.Attribute.GetAttrForge(EAttrForge.Range).CurValue<float>());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_checkRange.SetActive(false);
            m_checkRange = null;
        }

        public void SetComponent(TowerAgent agent)
        {
            m_agent = agent;
        }
    }
}
