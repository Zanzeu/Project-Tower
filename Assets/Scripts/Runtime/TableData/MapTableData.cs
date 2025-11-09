using Newtonsoft.Json;
using System.Collections.Generic;
using Tower.Runtime.Core;
using Tower.Runtime.Gameplay;
using Tower.Runtime.ToolKit;
using Tower.Runtime.Util;
using UnityEngine;

namespace Tower.Runtime.TableData
{
    public class MapTableData : TableDataBase
    {
        private readonly Dictionary<string, MapData> _mapDic = new Dictionary<string, MapData>();

        public override void LoadData()
        {
            string json = ResKit.LoadRes<TextAsset>(GlobalConst.ABName.JSONDATA, "MapData")?.text;

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("MapData º”‘ÿ ß∞‹");
                return;
            }

            List<LevelInfo> datas = null;

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new Vector2JsonConverter());

            var levelList = JsonConvert.DeserializeObject<LevelListData>(json, settings);

            if (levelList != null && levelList.Levels != null)
            {
                datas = levelList.Levels;
            }

            if (datas != null)
            {
                foreach (var data in datas)
                {
                    _mapDic.Add(data.ID, new MapData(data.ID, data.Name, data.SpawnInterval, data.PointPositions, data.WaveConfig));
                }
            }
        }

        public MapData GetMapData(string id)
        {
            if (_mapDic.TryGetValue(id, out var res))
            {
                return res;
            }

            return null;
        }
    }
}
