using Newtonsoft.Json;
using System.Collections.Generic;
using Tower.Runtime.Core;
using Tower.Runtime.Gameplay;
using Tower.Runtime.ToolKit;
using Tower.Runtime.Util;
using UnityEngine;

namespace Tower.Runtime.TableData
{
    public class BuffTableData : TableDataBase
    {
        private readonly Dictionary<string, BuffJson> _buffDic = new Dictionary<string, BuffJson>();

        public override void LoadData()
        {
            string json = ResKit.LoadRes<TextAsset>(GlobalConst.ABName.JSONDATA, "BuffData")?.text;

            if (string.IsNullOrEmpty(json))
            {
                Debug.LogWarning("BuffData º”‘ÿ ß∞‹");
                return;
            }

            var setting = new JsonSerializerSettings();
            setting.TypeNameHandling = TypeNameHandling.Auto;
            setting.Converters.Add(new Vector2JsonConverter());

            var buffList = JsonConvert.DeserializeObject<List<BuffJson>>(json, setting);
            if (buffList != null)
            {
                foreach (var data in buffList)
                {
                    _buffDic.Add(data.ID, data);
                }
            }
        }

        public BuffJson GetBuffJson(string id)
        {
            if (_buffDic.TryGetValue(id, out var res))
            {
                return res;
            }

            return null;
        }
    }
}
