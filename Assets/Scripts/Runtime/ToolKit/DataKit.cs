using System.Collections.Generic;
using Tower.Runtime.Core;
using Tower.Runtime.Gameplay;
using Tower.Runtime.TableData;
using UnityEngine;

namespace Tower.Runtime.ToolKit
{
    public class DataKit
    {
        public static GameObject GetPrefab(string resName)
        {
            return DataManager.Instance.Get<PrefabTableData>().GetPrefab(GlobalConst.ABName.PREFAB, resName);
        }

        public static void ClearPrefab()
        {
            DataManager.Instance.Get<PrefabTableData>().ClearTableData();
        }

        public static MapData GetMapData(string id)
        {
            return DataManager.Instance.Get<MapTableData>().GetMapData(id);
        }

        public static EnemyJson GetEnemyJson(string id)
        {
            return DataManager.Instance.Get<EnemyTableData>().GetEnemyJson(id);
        }

        public static List<EnemyJson> GetEnemyList()
        {
            return DataManager.Instance.Get<EnemyTableData>().GetEnemyList();
        }

        public static TowerJson GetTowerJson(string id)
        {
            return DataManager.Instance.Get<TowerTableData>().GetTowerJson(id);
        }

        public static BuffJson GetBuffJson(string id)
        {
            return DataManager.Instance.Get<BuffTableData>().GetBuffJson(id);
        }
    }
}