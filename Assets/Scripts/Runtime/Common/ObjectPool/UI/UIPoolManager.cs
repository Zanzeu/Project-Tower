using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Common
{
    public class UIPoolManager : MonoBehaviour
    {
        private static Dictionary<GameObject, UIPool> _poolDic;
        private static Transform _objTransform;

        private void Awake()
        {
            _poolDic = new Dictionary<GameObject, UIPool>();
            _objTransform = transform;
        }

        public static void RegisterPool(GameObject prefab, int initialSize = 10)
        {
            if (!_poolDic.ContainsKey(prefab))
            {
                UIPool newPool = new UIPool(prefab, initialSize);

                _poolDic.Add(newPool.Prefab, newPool);
                Transform parent = new GameObject($"Pool:{prefab.name}").transform;
                parent.parent = _objTransform;
                newPool.Init(parent);
            }
        }

        #region  ===== Õ∑≈‘§÷∆ÃÂ=====

        public static GameObject Release(GameObject prefab, Transform parent)
        {
            if (!_poolDic.ContainsKey(prefab))
            {
                RegisterPool(prefab);
            }

            return _poolDic[prefab].PreparedObject(parent);
        }

        public static GameObject Release(GameObject prefab, Vector3 postion, Transform parent)
        {
            if (!_poolDic.ContainsKey(prefab))
            {
                RegisterPool(prefab);
            }

            return _poolDic[prefab].PreparedObject(postion, parent);
        }

        public static GameObject Release(GameObject prefab, Vector3 postion, Quaternion rotation, Transform parent)
        {
            if (!_poolDic.ContainsKey(prefab))
            {
                RegisterPool(prefab);
            }

            return _poolDic[prefab].PreparedObject(postion, rotation, parent);
        }

        public static GameObject Release(GameObject prefab, Vector3 postion, Vector3 localScale, Transform parent)
        {
            if (!_poolDic.ContainsKey(prefab))
            {
                RegisterPool(prefab);
            }

            return _poolDic[prefab].PreparedObject(postion, localScale, parent);
        }

        public static GameObject Release(GameObject prefab, Vector3 postion, Quaternion rotation, Vector3 localScale, Transform parent)
        {
            if (!_poolDic.ContainsKey(prefab))
            {
                RegisterPool(prefab);
            }

            return _poolDic[prefab].PreparedObject(postion, rotation, localScale, parent);
        }
        #endregion
    }
}