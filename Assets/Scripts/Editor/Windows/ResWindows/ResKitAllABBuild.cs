using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Tower.Editor
{
    public class ResKitAllABBuild
    {
        [ShowInInspector, LabelText("选择平台"), ValueDropdown("GetPlatforms")]
        private string selectPlatform;

        [ShowInInspector, LabelText("选择AB包"), ValueDropdown("GetABNames"), OnValueChanged("LoadABAssets")]
        private string selectAB;

        [ShowInInspector, TableList(AlwaysExpanded = true, HideToolbar = true, IsReadOnly = true)]
        private List<ABInfo> abAssets = new List<ABInfo>();

        [Serializable]
        private struct ABInfo
        {
            [HideLabel, ReadOnly, BoxGroup("资源列表", false)]
            public UnityEngine.Object ABAssets;

            public ABInfo(UnityEngine.Object aBAssets)
            {
                ABAssets = aBAssets;
            }
        }


        private IEnumerable<string> GetPlatforms()
        {
            var root = Application.streamingAssetsPath;
            if (!Directory.Exists(root)) yield break;

            foreach (var dir in Directory.GetDirectories(root))
                yield return Path.GetFileName(dir);
        }

        private IEnumerable<string> GetABNames()
        {
            if (string.IsNullOrEmpty(selectPlatform)) yield break;

            var platformPath = Path.Combine(Application.streamingAssetsPath, selectPlatform);
            if (!Directory.Exists(platformPath)) yield break;

            foreach (var file in Directory.GetFiles(platformPath))
            {
                var ext = Path.GetExtension(file);
                if (ext == ".meta" || ext == ".manifest") continue;
                yield return Path.GetFileName(file);
            }
        }

        private void LoadABAssets()
        {
            abAssets.Clear();

            if (string.IsNullOrEmpty(selectPlatform) || string.IsNullOrEmpty(selectAB))
            {   
                Debug.LogWarning("未选择平台或AB包");
                return;
            }

            var abPath = Path.Combine(Application.streamingAssetsPath, selectPlatform, selectAB);
            if (!File.Exists(abPath))
            {
                Debug.LogWarning($"AB文件不存在: {abPath}");
                return;
            }

            var bundle = AssetBundle.LoadFromFile(abPath);
            if (bundle == null)
            {
                Debug.LogError($"加载AB失败: {abPath}");
                return;
            }

            var assets = bundle.LoadAllAssets<UnityEngine.Object>();
            foreach (var asset in assets)
            {
                if (asset == null || asset is Texture2D) continue;

                abAssets.Add(new ABInfo(asset));
            }

            bundle.Unload(false);
        }

        public void OnOpen()
        {
            selectAB = "";
            abAssets.Clear();
        }
    }
}
