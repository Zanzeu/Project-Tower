using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tower.Editor
{
    public class ResKitAllAB
    {
        [System.Serializable]
        public struct AssetInfo
        {
            [HideLabel, ReadOnly, BoxGroup("资源列表", false)]
            public string Path;

            [HideLabel, BoxGroup("资源列表", false), ReadOnly]
            public Object Asset;
        }

        [HorizontalGroup("AB选择", Width = 0.5f),LabelWidth(60)]
        [ShowInInspector, ValueDropdown("GetABNames"), OnValueChanged("OnABSelected"), LabelText("所选AB包")]
        private string selectedAB;

        [HorizontalGroup("AB选择", Width = 0.1f)]
        [Button("刷新", ButtonSizes.Medium)]
        public void RefreshABs()
        {
            AssetDatabase.Refresh();

            if (!string.IsNullOrEmpty(selectedAB) && AssetDatabase.GetAllAssetBundleNames().Length > 0)
            {
                OnABSelected();
            }
        }

        [HorizontalGroup("AB选择", Width = 0.1f)]
        [Button("清空", ButtonSizes.Medium)]
        public void ClearABs()
        {
            selectedAB = "";
            abAssets.Clear();

            AssetDatabase.Refresh();
        }

        [ShowInInspector, TableList(AlwaysExpanded = true, HideToolbar = true, IsReadOnly = true)]
        private List<AssetInfo> abAssets = new List<AssetInfo>();

        private IEnumerable<string> GetABNames()
        {   
            return AssetDatabase.GetAllAssetBundleNames();
        }


        private void OnABSelected()
        {
            abAssets.Clear();

            if (string.IsNullOrEmpty(selectedAB)) return;

            string[] assets = AssetDatabase.GetAssetPathsFromAssetBundle(selectedAB);
            foreach (string path in assets)
            {
                Object obj = AssetDatabase.LoadAssetAtPath<Object>(path);
                if (obj != null)
                {
                    abAssets.Add(new AssetInfo
                    {
                        Path = path,
                        Asset = obj
                    });
                }
            }
        }
    }
}
