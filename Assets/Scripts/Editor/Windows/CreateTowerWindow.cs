using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using System.IO;
using Tower.Runtime.Gameplay;
using Tower.Runtime.Util;
using UnityEditor;

namespace Tower.Editor
{
    public class CreateTowerWindow : OdinEditorWindow
    {
        private string path = "Assets/JsonData/TowerData.json";

        [LabelText("塔防列表"), TableList]
        public List<TowerJson> towerList = new();

        [MenuItem("MyTools/数据管理/塔防数据")]
        private static void OpenWindow()
        {
            var window = GetWindow<CreateTowerWindow>("塔防数据");
            window.Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ReloadJson();
        }

        [Button(ButtonSizes.Medium, Name = "保存为JSON")]
        private void SaveJson()
        {
            string directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var setting = new JsonSerializerSettings();
            setting.Converters.Add(new Vector2JsonConverter());
            setting.Formatting = Formatting.Indented;
            setting.TypeNameHandling = TypeNameHandling.Auto;
            string json = JsonConvert.SerializeObject(towerList, setting);

            File.WriteAllText(path, json);
            AssetDatabase.Refresh();
        }

        [Button(ButtonSizes.Medium, Name = "重新加载JSON")]
        private void ReloadJson()
        {
            if (!File.Exists(path))
            {
                towerList = new List<TowerJson>();
                return;
            }

            string json = File.ReadAllText(path);
            var setting = new JsonSerializerSettings();
            setting.TypeNameHandling = TypeNameHandling.Auto;
            setting.Converters.Add(new Vector2JsonConverter());

            towerList = JsonConvert.DeserializeObject<List<TowerJson>>(json, setting);

            if (towerList == null)
            {
                towerList = new List<TowerJson>();
            }
        }
    }
}
