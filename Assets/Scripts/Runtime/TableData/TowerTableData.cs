using Newtonsoft.Json;
using System.Collections.Generic;
using Tower.Runtime.Core;
using Tower.Runtime.Gameplay;
using Tower.Runtime.ToolKit;
using Tower.Runtime.Util;
using UnityEngine;

namespace Tower.Runtime.TableData
{
    public class TowerTableData : TableDataBase
    {
        private readonly Dictionary<string, TowerJson> _towerDic = new Dictionary<string, TowerJson>();

        public override void LoadData()
        {
            string json = ResKit.LoadRes<TextAsset>(GlobalConst.ABName.JSONDATA, "TowerData")?.text;

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("TowerData º”‘ÿ ß∞‹");
                return;
            }

            var setting = new JsonSerializerSettings();
            setting.TypeNameHandling = TypeNameHandling.Auto;
            setting.Converters.Add(new Vector2JsonConverter());

            var towerList = JsonConvert.DeserializeObject<List<TowerJson>>(json, setting);

            if (towerList != null)
            {
                foreach (var data in towerList)
                {
                    _towerDic.Add(data.ID, data);
                }
            }
        }

        public TowerJson GetTowerJson(string id)
        {
            if (_towerDic.TryGetValue(id, out var res))
            {
                return res;
            }

            return null;
        }
    }
}
