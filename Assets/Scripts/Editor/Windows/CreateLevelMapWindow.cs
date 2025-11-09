using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using System.IO;
using Tower.Runtime.Gameplay;
using Tower.Runtime.Util;
using UnityEditor;
using UnityEngine;

namespace Tower.Editor
{
    public class CreateLevelMapWindow : OdinEditorWindow
    {
        private string path = "Assets/JsonData/MapData.json";

        [LabelText("关卡列表"), TableList]
        public List<LevelInfo> levelList = new();

        [MenuItem("MyTools/数据管理/关卡数据")]
        private static void OpenWindow()
        {
            var window = GetWindow<CreateLevelMapWindow>("关卡数据");
            window.minSize = new Vector2(800, 600);
            window.Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ReloadJson();
        }

        [Button(ButtonSizes.Medium, Name = "刷新途径点")]
        private void RefreshSelectedLevelPoints()
        {
            if (levelList == null || levelList.Count == 0)
            {
                Debug.LogWarning("关卡列表为空。");
                return;
            }

            var targetLevel = levelList[0];
            RefreshPoints(targetLevel);
        }

        private void RefreshPoints(LevelInfo level)
        {
            level.PointPositions.Clear();

            GameObject pointsRoot = GameObject.Find("Map/Points");
            if (pointsRoot == null)
            {
                Debug.LogWarning("未找到 Map/Points 对象");
                return;
            }

            foreach (Transform child in pointsRoot.transform)
            {
                Vector2 pos2D = new Vector2(child.position.x, child.position.y);
                level.PointPositions.Add(pos2D);
            }
        }

        [Button(ButtonSizes.Medium, Name = "保存为JSON")]
        private void SaveJson()
        {
            var data = new LevelListData { Levels = levelList };

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                Converters = { new Vector2JsonConverter() }
            };

            string json = JsonConvert.SerializeObject(data, settings);

            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(path, json);
            AssetDatabase.Refresh();        }

        [Button(ButtonSizes.Medium, Name = "重新加载JSON")]
        private void ReloadJson()
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new Vector2JsonConverter());

                var data = JsonConvert.DeserializeObject<LevelListData>(json, settings);
                if (data != null && data.Levels != null)
                {
                    levelList = data.Levels;
                }
            }
            else
            {
                levelList = new List<LevelInfo>();
            }
        }
    }
}
