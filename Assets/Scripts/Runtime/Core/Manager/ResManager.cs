using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Tower.Runtime.ToolKit;

namespace Tower.Runtime.Core
{
    public class ResManager : Singleton<ResManager>
    {
        [LabelText("读取模式")] public ELoadResMode Mode = ELoadResMode.ABLoad;

        private string ABPATH;
        private AssetBundle abMain = null;
        private AssetBundleManifest abManifest = null;

        private readonly Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();
        private readonly Dictionary<string, int> refCountDict = new Dictionary<string, int>();

        protected override void OnAwake()
        {
            string platformFolder = GetPlatformFolder();
            ABPATH = $"{Application.streamingAssetsPath}/{platformFolder}/";
        }

        #region --- AB加载核心 ---
        private void LoadAB(string abName)
        {
            if (abMain == null)
            {
                abMain = AssetBundle.LoadFromFile(ABPATH + GetPlatformFolder());
                abManifest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }

            string[] dependencies = abManifest.GetAllDependencies(abName);
            foreach (string dep in dependencies)
            {
                LoadSingleAB(dep);
            }

            LoadSingleAB(abName);
        }

        private void LoadSingleAB(string abName)
        {
            if (!bundles.ContainsKey(abName))
            {
                var ab = AssetBundle.LoadFromFile(ABPATH + abName);
                if (ab != null)
                {
                    bundles[abName] = ab;
                    refCountDict[abName] = 1;
                }
                else
                {
                    Debug.LogError($"[ResManager] 加载 AB 失败: {ABPATH + abName}");
                }
            }
            else
            {
                refCountDict[abName]++;
            }
        }
        #endregion

        #region --- 同步加载 ---
        public T LoadResources<T>(string abName, string resName) where T : Object
        {
            LoadAB(abName);
            return bundles[abName].LoadAsset<T>(resName);
        }

        public List<T> LoadAllResources<T>(string abName) where T : Object
        {
            LoadAB(abName);
            List<T> result = new List<T>();
            T[] assets = bundles[abName].LoadAllAssets<T>();
            if (assets != null && assets.Length > 0)
            {
                result.AddRange(assets);
            }
            return result;
        }
        #endregion

        #region --- 异步加载 ---
        private IEnumerator LoadResourcesAsyncCoroutine<T>(string abName, string resName, UnityAction<T> callBack, bool autoRelease = false) where T : Object
        {
            LoadAB(abName);
            AssetBundleRequest req = bundles[abName].LoadAssetAsync<T>(resName);
            yield return req;
            callBack?.Invoke(req.asset as T);

            if (autoRelease)
            {
                yield return null;
                Release(abName);
            }
        }

        public void LoadResourcesAsync<T>(string abName, string resName, UnityAction<T> callBack, bool autoRelease = false) where T : Object
        {
            StartCoroutine(LoadResourcesAsyncCoroutine(abName, resName, callBack, autoRelease));
        }
        #endregion

        #region --- 卸载与引用计数 ---

        public void Release(string abName)
        {
            if (!refCountDict.ContainsKey(abName))
            {
                Debug.LogWarning($"[ResManager] 尝试释放未加载的AB: {abName}");
                return;
            }

            refCountDict[abName]--;
            if (refCountDict[abName] <= 0)
            {
                UnloadAB(abName);
            }
        }

        private void UnloadAB(string abName)
        {
            if (bundles.ContainsKey(abName))
            {
                bundles[abName].Unload(false);
                bundles.Remove(abName);
                refCountDict.Remove(abName);
            }
        }

        public void Clear()
        {
            foreach (var kv in bundles)
            {
                kv.Value.Unload(false);
            }
            bundles.Clear();
            refCountDict.Clear();
            abMain = null;
            abManifest = null;
        }
        #endregion

        #region --- 平台路径 ---
        private string GetPlatformFolder()
        {
#if UNITY_EDITOR
            switch (UnityEditor.EditorUserBuildSettings.activeBuildTarget)
            {
                case UnityEditor.BuildTarget.StandaloneWindows:
                case UnityEditor.BuildTarget.StandaloneWindows64:
                    return "Windows";
                case UnityEditor.BuildTarget.Android:
                    return "Android";
                case UnityEditor.BuildTarget.iOS:
                    return "iOS";
                case UnityEditor.BuildTarget.StandaloneOSX:
                    return "Mac";
                default:
                    return "Unknown";
            }
#else
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    return "Windows";
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                    return "Mac";
                default:
                    return "Unknown";
            }
#endif
        }
        #endregion
    }
}
