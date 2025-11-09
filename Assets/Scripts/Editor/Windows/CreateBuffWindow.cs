using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using System.IO;
using Tower.Runtime.Gameplay;
using UnityEditor;

namespace Tower.Editor
{
    public class CreateBuffWindow : OdinEditorWindow
    {
        private string path = "Assets/JsonData/BuffData.json";

        [LabelText("Buff列表"), TableList]
        public List<BuffJson> buffList = new();

        [MenuItem("MyTools/数据管理/Buff数据")]
        private static void OpenWindow()
        {
            var window = GetWindow<CreateBuffWindow>("Buff数据");
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
            setting.Formatting = Formatting.Indented;
            setting.TypeNameHandling = TypeNameHandling.Auto;
            string json = JsonConvert.SerializeObject(buffList, setting);

            File.WriteAllText(path, json);
            AssetDatabase.Refresh();
        }

        [Button(ButtonSizes.Medium, Name = "重新加载JSON")]
        private void ReloadJson()
        {
            if (!File.Exists(path))
            {
                buffList = new List<BuffJson>();
                return;
            }

            string json = File.ReadAllText(path);
            var setting = new JsonSerializerSettings();
            setting.TypeNameHandling = TypeNameHandling.Auto;

            buffList = JsonConvert.DeserializeObject<List<BuffJson>>(json, setting);

            if (buffList == null)
            {
                buffList = new List<BuffJson>();
            }
        }
    }
}
