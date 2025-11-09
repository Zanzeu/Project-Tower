using Tower.Editor;
using Tower.Runtime.Core;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Tower.Runtime.ToolKit
{
    public enum ELoadResMode
    {
        [LabelText("AB¶ÁÈ¡")] ABLoad,
        [LabelText("Â·¾¶¶ÁÈ¡")] PathLoad,
    }

    public class ResKit
    {
        public static T LoadRes<T>(string abName, string resName) where T : Object
        {
            if (ResManager.Instance.Mode == ELoadResMode.ABLoad)
            {
                return ResManager.Instance.LoadResources<T>(abName, resName);
            }

#if UNITY_EDITOR
            string assetPath = ResExPathMap.Instance.GetFilePath(abName, resName);
            if (!string.IsNullOrEmpty(assetPath))
            {
                return AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            return null;
#else
            return ResManager.Instance.LoadResources<T>(abName, resName);
#endif
        }

        public static List<T> LoadAll<T>(string abName) where T : Object
        {
            if (ResManager.Instance.Mode == ELoadResMode.ABLoad)
            {
                return ResManager.Instance.LoadAllResources<T>(abName);
            }

#if UNITY_EDITOR
            List<T> assets = new List<T>();
            List<string> assetPaths = ResExPathMap.Instance.GetAllFilePaths(abName);
            if (assetPaths != null)
            {
                foreach (var path in assetPaths)
                {
                    T asset = AssetDatabase.LoadAssetAtPath<T>(path);
                    if (asset != null)
                    {
                        assets.Add(asset);
                    }
                }
            }
            return assets;
#else
    return ResManager.Instance.LoadAllResources<T>(abName);
#endif
        }

        public static void LoadResAsyc<T>(string abName, string resName, UnityAction<T> callBack, bool autoRelease = false) where T : Object
        {
            if (ResManager.Instance.Mode == ELoadResMode.ABLoad)
            {
                ResManager.Instance.LoadResourcesAsync<T>(abName, resName, callBack, autoRelease);
            }
            else
            {
#if UNITY_EDITOR
                T res = LoadRes<T>(abName, resName);
                callBack?.Invoke(res);
#else
                ResManager.Instance.LoadResourcesAsync<T>(abName, resName, callBack, autoRelease);
#endif
            }
        }
    }
}
