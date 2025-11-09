using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Common
{
    public class EntityPoolManager : MonoBehaviour
    {
        private static Dictionary<GameObject, EntityPool> _poolDic;
        private static Transform objParent;

        private void Awake()
        {
            _poolDic = new Dictionary<GameObject, EntityPool>();
            objParent = transform;
        }

        private void CheckPoolSize(params EntityPool[][] pools)
        {
            foreach (var poolArray in pools)
            {
                foreach (var pool in poolArray)
                {
                    if (pool.RuntimeSize > pool.Size)
                    {
                        Debug.LogWarning(string.Format("{0}的尺寸{1}大于初始对象池的尺寸{2}!",
                            pool.Prefab.name,
                            pool.RuntimeSize,
                            pool.Size));
                    }
                }
            }
        }

        private void Init(params EntityPool[][] pools)
        {
            foreach (var poolArray in pools)
            {
                foreach (var pool in poolArray)
                {
#if UNITY_EDITOR
                    if (_poolDic.ContainsKey(pool.Prefab))
                    {
                        Debug.LogError("发现相同预制体:" + pool.Prefab.name);
                        continue;
                    }
#endif

                    _poolDic.Add(pool.Prefab, pool);

                    Transform poolParent = new GameObject("Pool:" + pool.Prefab.name).transform;

                    poolParent.parent = transform;
                    pool.Init(poolParent);
                }
            }
        }

        public static void RegisterPool(GameObject prefab, int initialSize = 20)
        {
            if (!_poolDic.ContainsKey(prefab))
            {
                EntityPool newPool = new EntityPool(prefab, initialSize);

                _poolDic.Add(newPool.Prefab, newPool);
                Transform parent = new GameObject($"Pool:{prefab.name}").transform;
                parent.parent = objParent;
                newPool.Init(parent);
            }
        }

        #region  =====释放预制体=====

        public static GameObject Release(GameObject prefab)
        {
            if (!_poolDic.ContainsKey(prefab))
            {
                RegisterPool(prefab);
            }

            return _poolDic[prefab].PreparedObject();
        }

        public static GameObject Release(GameObject prefab, Vector3 postion)
        {

            if (!_poolDic.ContainsKey(prefab))
            {
                RegisterPool(prefab);
            }

            return _poolDic[prefab].PreparedObject(postion);
        }

        public static GameObject Release(GameObject prefab, Vector3 postion, Quaternion rotation)
        {

            if (!_poolDic.ContainsKey(prefab))
            {
                RegisterPool(prefab);
            }

            return _poolDic[prefab].PreparedObject(postion, rotation);
        }

        public static GameObject Release(GameObject prefab, Vector3 postion, Vector3 localScale)
        {
            if (!_poolDic.ContainsKey(prefab))
            {
                RegisterPool(prefab);
            }

            return _poolDic[prefab].PreparedObject(postion, localScale);
        }

        public static GameObject Release(GameObject prefab, Vector3 postion, Quaternion rotation, Vector3 localScale)
        {
            if (!_poolDic.ContainsKey(prefab))
            {
                RegisterPool(prefab);
            }

            return _poolDic[prefab].PreparedObject(postion, rotation, localScale);
        }
        #endregion
    }
}