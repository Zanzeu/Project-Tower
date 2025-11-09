using Tower.Runtime.Core;
using Tower.Runtime.UI;
using UnityEngine;

namespace Tower.Runtime.Common
{
    public class UIKit
    {   
        public static void RegisterUI(IGameUI ui)
        {
            UIManager.Instance.Register(ui);
        }

        public static bool IsClicked
        {
            get
            {
                return UIManager.Instance.IsClicked;
            }
            set
            {
                UIManager.Instance.IsClicked = value;
            }
        }


        public static T GetUI<T>() where T : class, IGameUI
        {
           return UIManager.Instance.Get<T>();
        }

        public static T ShowUI<T>() where T : class, IGameUI
        {
            return UIManager.Instance.Show<T>();
        }

        public static T HideUI<T>() where T : class, IGameUI
        {
            return UIManager.Instance.Hide<T>();
        }
    }
}