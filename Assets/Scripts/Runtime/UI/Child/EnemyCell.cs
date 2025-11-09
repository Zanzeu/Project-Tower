using Tower.Runtime.Common;
using Tower.Runtime.Gameplay;
using Tower.Runtime.ToolKit;
using UnityEngine;
using UnityEngine.UI;

namespace Tower.Runtime.UI
{
    public class EnemyCell : MonoBehaviour, IUIPool
    {
        public Transform Parent {  get; set; }

        [SerializeField] private Image icon;

        private EnemyData m_data;
        private Button m_btn;

        public void Enpool()
        {
            transform.SetParent(Parent);
            gameObject.SetActive(false);
        }

        public void OnAwake(Transform parent)
        {
            Parent = parent;
            m_btn = GetComponent<Button>();

            m_btn.onClick.AddListener(() =>
            {
                EventKit.GlobalEvent.Trigger(Core.EGlobalEvent.OnCheckEnemy, m_data);
            });
        }

        public void OnSpawn(EnemyData data)
        {   
            icon.sprite = ResKit.LoadRes<Sprite>(GlobalConst.ABName.ARTS, data.IconPath);
            m_data = data;
            if (!SaveKit.Get(data.ID))
            {
                icon.color = Color.black;
            }
        }
    }
}
