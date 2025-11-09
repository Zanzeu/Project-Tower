using System;
using System.Collections.Generic;
using Tower.Runtime.TableData;

namespace Tower.Runtime.Core
{
    public class DataManager : Singleton<DataManager>
    {
        private readonly Dictionary<Type, ITableData> _tableDic = new Dictionary<Type, ITableData>();

        protected override void OnAwake()
        {
            Add(new PrefabTableData());
            Add(new BuffTableData());
            Add(new MapTableData());
            Add(new EnemyTableData());
            Add(new TowerTableData());

            foreach (var table in _tableDic.Values)
            {
                table.LoadData();
            }
        }

        private void Add(ITableData table)
        {
            if (!_tableDic.ContainsKey(table.GetType()))
            {
                _tableDic[table.GetType()] = table;
            }
        }

        public T Get<T>() where T : class, ITableData
        {
            if (_tableDic.TryGetValue(typeof(T), out var table))
            {
                return table as T;
            }

            return null;
        }
    }
}