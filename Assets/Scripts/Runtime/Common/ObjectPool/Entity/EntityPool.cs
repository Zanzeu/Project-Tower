using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tower.Runtime.Common
{
    public interface IObjectPool
    {
        void OnAwake();
        void OnKill();
    }

    [Serializable]
    public class EntityPool
    {
        public EntityPool(GameObject prefab, int size)
        {
            this.prefab = prefab;
            this.size = size;
        }

        public GameObject Prefab
        { get { return prefab; } }

        public int Size
        { get { return size; } }

        public int RuntimeSize
        { get { return queue.Count; } }

        [SerializeField] private GameObject prefab;
        [SerializeField] private int size = 0;

        private Queue<GameObject> queue;

        private Transform parent;

        public void Init(Transform parent)
        {
            queue = new Queue<GameObject>();
            this.parent = parent;

            for (var i = 0; i < size; i++)
            {
                queue.Enqueue(Copy());
            }
        }

        private GameObject Copy()
        {
            var copy = GameObject.Instantiate(prefab, parent);
            IObjectPool pool = copy.GetComponent<IObjectPool>();
            if (pool != null)
            {
                pool.OnAwake();
            }
            copy.SetActive(false);

            return copy;
        }

        private GameObject AvailableObject()
        {
            GameObject availableObject = null;

            if (queue.Count > 0 && !queue.Peek().activeSelf)
            {
                availableObject = queue.Dequeue();
            }
            else
            {
                availableObject = Copy();
            }

            queue.Enqueue(availableObject);

            return availableObject;
        }

        public GameObject PreparedObject()
        {
            GameObject preparedObject = AvailableObject();

            preparedObject.SetActive(true);

            return preparedObject;
        }

        public GameObject PreparedObject(Vector3 postion)
        {
            GameObject preparedObject = AvailableObject();

            preparedObject.SetActive(true);
            preparedObject.transform.position = postion;

            return preparedObject;
        }

        public GameObject PreparedObject(Vector3 postion, Quaternion rotation)
        {
            GameObject preparedObject = AvailableObject();

            preparedObject.SetActive(true);
            preparedObject.transform.position = postion;
            preparedObject.transform.rotation = rotation;

            return preparedObject;
        }

        public GameObject PreparedObject(Vector3 postion, Vector3 localScale)
        {
            GameObject preparedObject = AvailableObject();

            preparedObject.SetActive(true);
            preparedObject.transform.position = postion;
            preparedObject.transform.localScale = localScale;

            return preparedObject;
        }

        public GameObject PreparedObject(Vector3 postion, Quaternion rotation, Vector3 localScale)
        {
            GameObject preparedObject = AvailableObject();

            preparedObject.SetActive(true);
            preparedObject.transform.position = postion;
            preparedObject.transform.rotation = rotation;
            preparedObject.transform.localScale = localScale;

            return preparedObject;
        }
    }
}