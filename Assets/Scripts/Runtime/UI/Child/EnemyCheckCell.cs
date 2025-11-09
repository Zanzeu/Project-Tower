using Sirenix.OdinInspector;
using Tower.Runtime.Common;
using Tower.Runtime.Gameplay;
using Tower.Runtime.ToolKit;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Runtime.UI
{
    public class EnemyCheckCell : MonoBehaviour, IUIPool
    {
        [SerializeField] private Button m_btn;
        [SerializeField,LabelText("Í¼±ê")] private Image icon;

        public Transform Parent {  get; set; }

        private string m_id;

        public void Enpool()
        {   
            transform.SetParent(Parent);
            gameObject.SetActive(false);
        }

        public void OnAwake(Transform parent)
        {
            Parent = parent;
            m_btn.onClick.AddListener(OnClicked);
        }

        public void OnSpawn(EnemyData data)
        {
            UIKit.GetUI<EnemyCheckPanel>().SetPanel(data);
            icon.sprite = ResKit.LoadRes<Sprite>(GlobalConst.ABName.ARTS, data.IconPath);
            m_id = data.ID;
            transform.localScale = Vector3.one;
        }

        public void OnClicked()
        {
            UIKit.GetUI<EnemyCheckPanel>().OnShowUI();
            Enpool();
            SaveKit.Set(m_id, true);
        }
    }
}
