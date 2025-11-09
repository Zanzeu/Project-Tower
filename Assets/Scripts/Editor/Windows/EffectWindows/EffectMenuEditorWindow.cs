using Sirenix.OdinInspector.Editor;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Tower.Editor
{
    public class EffectMenuEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("MyTools/数据管理/效果器")]
        private static void ShowWindow()
        {
            GetWindow<EffectMenuEditorWindow>("效果器管理").Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;

            var createScript = new EffectOperation();

            tree.Add("操作", createScript);

            var allAssets = AssetDatabase.GetAllAssetPaths()
                .Where(x => x.StartsWith("Assets/SO/Effect"))
                .OrderBy(x => x);

            foreach (var path in allAssets)
            {
                var fileName = Path.GetFileNameWithoutExtension(path);

                if (fileName.Equals("Effect", System.StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (fileName.StartsWith("EffectConfig"))
                    fileName = fileName.Substring("EffectConfig".Length);

                fileName = fileName.TrimStart('_');

                var displayPath = $"数据库/{fileName}";
                var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
                tree.Add(displayPath, asset);
            }

            return tree;
        }
    }
}