using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Tower.Editor
{
    public class ResKitMenuEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("MyTools/ResKit")]
        private static void ShowWindow()
        {
            GetWindow<ResKitMenuEditorWindow>("资源管理").Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;

            var resExAllTag = new ResKitAllAB();
            var resExSetting = new ResKitSetting();
            var resExABRes = new ResKitAllABBuild();
            var resMap = AssetDatabase.LoadAssetAtPath<ResExPathMap>("Assets/Scripts/Editor/SO/ResKit/ResPathMap.asset");

            tree.Add("查看标记", resExAllTag);
            tree.Add("打包", resExSetting);
            tree.Add("查看AB包", resExABRes);
            tree.Add("路径映射", resMap);

            resExAllTag.RefreshABs();
            resExABRes.OnOpen();

            return tree;
        }
    }
}