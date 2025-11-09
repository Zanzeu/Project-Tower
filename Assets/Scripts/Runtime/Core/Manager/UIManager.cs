using System;
using System.Collections.Generic;
using Tower.Runtime.UI;

namespace Tower.Runtime.Core
{
    public class UIManager : Singleton<UIManager>
    {
        protected override bool IsPersistent => false;

        private Dictionary<Type, IGameUI> _uiDic = new Dictionary<Type, IGameUI>();

        public bool IsClicked { get; set; } = false;


        public void Register(IGameUI ui)
        {
            if (!_uiDic.ContainsKey(ui.GetType()))
            {
                _uiDic[ui.GetType()] = ui;
            }
        }

        public T Get<T>() where T : class, IGameUI
        {
            if (_uiDic.TryGetValue(typeof(T), out var ui))
            {
                return ui as T;
            }

            return null;
        }

        public T Show<T>() where T : class, IGameUI
        {
            if (_uiDic.TryGetValue(typeof(T), out var ui))
            {
                ui.OnShowUI();
                return ui as T;
            }

            return null;
        }

        public T Hide<T>() where T : class, IGameUI
        {
            if (_uiDic.TryGetValue(typeof(T), out var ui))
            {
                ui.OnHideUI();
                return ui as T;
            }

            return null;
        }
    }
}
