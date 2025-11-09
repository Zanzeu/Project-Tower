using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Tower.Runtime.ToolKit
{
    public class SaveKit
    {
        public static Dictionary<string, bool> m_enemyDic = new Dictionary<string, bool>();

        private static string Path => Application.persistentDataPath + "/SaveData.json";

        public static void Load()
        {
            if (!File.Exists(Path))
            {
                m_enemyDic = new Dictionary<string, bool>();
                Save();
                return;
            }

            try
            {
                string json = File.ReadAllText(Path);
                m_enemyDic = JsonConvert.DeserializeObject<Dictionary<string, bool>>(json);

                if (m_enemyDic == null)
                {
                    m_enemyDic = new Dictionary<string, bool>();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveKit] Failed to load save file: {e.Message}");
                m_enemyDic = new Dictionary<string, bool>();
            }
        }

        public static void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(m_enemyDic, Formatting.Indented);
                File.WriteAllText(Path, json);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveKit] Failed to save file: {e.Message}");
            }
        }

        public static void Set(string id, bool value)
        {
            m_enemyDic[id] = value;
            Save();
        }

        public static bool Get(string id)
        {
            if (!m_enemyDic.ContainsKey(id))
            {
                m_enemyDic[id] = false;
                Save();
            }
            return m_enemyDic[id];
        }
    }
}
