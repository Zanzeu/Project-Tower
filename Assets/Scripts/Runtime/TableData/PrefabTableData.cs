using System.Collections.Generic;
using Tower.Runtime.Core;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.TableData
{
    public class PrefabTableData : TableDataBase
    {
        private readonly Dictionary<string, GameObject> _prefabDic = new Dictionary<string, GameObject>();

        public GameObject GetPrefab(string abName, string resName)
        {
            if (_prefabDic.ContainsKey(resName))
            {
                return _prefabDic[resName];
            }

            _prefabDic.Add(resName, ResKit.LoadRes<GameObject>(abName, resName));

            return _prefabDic[resName];
        }

        public void ClearTableData()
        {
            _prefabDic.Clear();
        }

        public override void LoadData() { }
    }
}
