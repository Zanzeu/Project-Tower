using System.IO;
using UnityEditor;
using UnityEngine;

namespace Tower.Editor
{
    public class ResKitBuild
    {
        [MenuItem("Assets/AB包/标记")]
        private static void SettingABTag()
        {
            string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(folderPath) || !AssetDatabase.IsValidFolder(folderPath))
            {
                Debug.LogError("未选择有效的文件夹");
                return;
            }

            string folderName = Path.GetFileName(folderPath);
            string abName = folderName.ToLower();

            AssetImporter folderImporter = AssetImporter.GetAtPath(folderPath);
            if (folderImporter != null)
            {
                folderImporter.assetBundleName = abName;
            }

            var pathMap = ResExPathMap.Instance;
            if (pathMap != null)
            {
                pathMap.AddPath(abName, folderPath);
                EditorUtility.SetDirty(pathMap);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/AB包/清除")]
        private static void ClearABTag()
        {
            string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (string.IsNullOrEmpty(folderPath) || !AssetDatabase.IsValidFolder(folderPath))
            {
                Debug.LogError("未选择有效的文件夹");
                return;
            }

            AssetImporter folderImporter = AssetImporter.GetAtPath(folderPath);
            if (folderImporter != null && !string.IsNullOrEmpty(folderImporter.assetBundleName))
            {
                folderImporter.assetBundleName = string.Empty;
            }

            var pathMap = ResExPathMap.Instance;
            if (pathMap != null)
            {
                string abName = Path.GetFileName(folderPath).ToLower();
                pathMap.Remove(abName);
                EditorUtility.SetDirty(pathMap);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}