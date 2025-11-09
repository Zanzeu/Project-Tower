using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Tower.Editor
{
    public class ResKitSetting
    {
        [ShowInInspector, LabelText("打包路径"), Sirenix.OdinInspector.FilePath]
        private string selectedAB = "Assets/StreamingAssets";

        [ShowInInspector, LabelText("选择平台")]
        private BuildTarget buildPlatform = BuildTarget.StandaloneWindows64;

        [Button("删除未使用的AB包名称", ButtonSizes.Medium)]
        private void DeleteUnuseAB()
        {
            AssetDatabase.RemoveUnusedAssetBundleNames();
            AssetDatabase.Refresh();
        }

        [Button("清空AB包", ButtonSizes.Medium)]
        private void ClearAB()
        {
            string platformFolder = GetPlatformFolder(buildPlatform);
            string targetPath = Path.Combine(selectedAB, platformFolder);

            if (!string.IsNullOrEmpty(targetPath) && Directory.Exists(targetPath))
            {
                Directory.Delete(targetPath, true);
            }
            Directory.CreateDirectory(targetPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [Button("打包", ButtonSizes.Medium)]
        private void BuildAB()
        {
            if (string.IsNullOrEmpty(selectedAB))
            {
                Debug.LogError("请选择打包路径！");
                return;
            }

            string platformFolder = GetPlatformFolder(buildPlatform);

            string outputPath = Path.Combine(selectedAB, platformFolder);

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            BuildPipeline.BuildAssetBundles(
                outputPath,
                BuildAssetBundleOptions.ChunkBasedCompression,
                buildPlatform
            );

            AssetDatabase.Refresh();
            Debug.Log($"AB 打包完成 -> {outputPath}");
        }

        private string GetPlatformFolder(BuildTarget target)
        {
            switch (target)
            {
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.StandaloneOSX:
                    return "Mac";
                default:
                    return target.ToString();
            }
        }
    }
}