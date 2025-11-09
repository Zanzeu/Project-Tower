using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Tower.Editor
{
    public class ResExPathMap : SerializedScriptableObject
    {
#if UNITY_EDITOR
        public static ResExPathMap Instance => UnityEditor.AssetDatabase.LoadAssetAtPath<ResExPathMap>("Assets/Scripts/Editor/SO/ResKit/ResPathMap.asset");

        [SerializeField, ReadOnly, HideLabel]
        [DictionaryDrawerSettings(KeyLabel = "包名", ValueLabel = "路径")]
        private readonly Dictionary<string, string> _resPathDic = new Dictionary<string, string>();

        /// <summary>
        /// 获取包里某个文件的路径
        /// </summary>
        public string GetFilePath(string abName, string resName)
        {
            if (_resPathDic.ContainsKey(abName))
            {
                string folderPath = _resPathDic[abName];

                string[] guids = UnityEditor.AssetDatabase.FindAssets(resName, new[] { folderPath });
                foreach (var guid in guids)
                {
                    string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                    string fileName = Path.GetFileNameWithoutExtension(assetPath);
                    if (fileName.Equals(resName, System.StringComparison.OrdinalIgnoreCase))
                    {
                        return assetPath;
                    }
                }

                Debug.LogError($"资源 {resName} 在包 {abName} 中不存在！");
                return "";
            }

            Debug.LogError($"{abName} 包不存在！");
            return "";
        }

        /// <summary>
        /// 添加包路径映射
        /// </summary>
        public void AddPath(string abName, string folderPath)
        {
            _resPathDic[abName] = folderPath;
        }

        /// <summary>
        /// 移除包路径映射
        /// </summary>
        public void Remove(string abName)
        {
            if (_resPathDic.ContainsKey(abName))
            {
                _resPathDic.Remove(abName);
            }
        }

        /// <summary>
        /// 获取某个包下所有资源路径
        /// </summary>
        public List<string> GetAllFilePaths(string abName)
        {
            List<string> filePaths = new List<string>();

            if (_resPathDic.TryGetValue(abName, out var folderPath))
            {
                string[] guids = UnityEditor.AssetDatabase.FindAssets("", new[] { folderPath });
                foreach (var guid in guids)
                {
                    string assetPath = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                    if (!UnityEditor.AssetDatabase.IsValidFolder(assetPath))
                    {
                        filePaths.Add(assetPath);
                    }
                }
                return filePaths;
            }

            Debug.LogError($"{abName} 包不存在！");
            return filePaths;
        }
#endif
    }
}
