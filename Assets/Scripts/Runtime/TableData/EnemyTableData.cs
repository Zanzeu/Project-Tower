using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Tower.Runtime.Core;
using Tower.Runtime.Gameplay;
using Tower.Runtime.ToolKit;
using Tower.Runtime.Util;
using UnityEngine;

namespace Tower.Runtime.TableData
{
    public class EnemyTableData : TableDataBase
    {
        private readonly Dictionary<string, EnemyJson> _enemyDic = new Dictionary<string, EnemyJson>();

        public override void LoadData()
        {
            string json = ResKit.LoadRes<TextAsset>(GlobalConst.ABName.JSONDATA, "EnemyData")?.text;

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("EnemyData º”‘ÿ ß∞‹");
                return;
            }

            var setting = new JsonSerializerSettings();
            setting.TypeNameHandling = TypeNameHandling.Auto;
            setting.Converters.Add(new Vector2JsonConverter());

            var enemyList = JsonConvert.DeserializeObject<List<EnemyJson>>(json, setting);

            if (enemyList != null)
            {
                foreach (var data in enemyList)
                {
                    _enemyDic.Add(data.ID, data);
                }
            }
        }

        public EnemyJson GetEnemyJson(string id)
        {
            if (_enemyDic.TryGetValue(id, out var res))
            {
                return res;
            }

            return null;
        }

        public List<EnemyJson> GetEnemyList()
        {
            return _enemyDic.Values.ToList();
        }
    }
}
