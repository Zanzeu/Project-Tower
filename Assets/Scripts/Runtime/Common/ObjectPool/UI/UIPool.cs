using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Tower.Runtime.Common
{
    public interface IUIPool
    {   
        public Transform Parent { get; set; }
        void OnAwake(Transform parent);
        void Enpool();
    }

    [Serializable]
    public class UIPool
    {   
        public UIPool() { }

        public UIPool(GameObject prefab, int size)
        {
            this.prefab = prefab;
            this.size = size;
        }

        public GameObject Prefab
        {
            get => prefab;
        }

        public int Size
        {
            get => size;
        }

        public int RuntimeSize
        { get { return queue.Count; } }

        [LabelText("预制体")][Required("必须放入预制体")][SuffixLabel("Prefab")][SerializeField] private GameObject prefab;
        [LabelText("预生成数量")][MinValue(0)][SerializeField] private int size = 0;

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

            copy.GetComponent<IUIPool>()?.OnAwake(parent);
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

        public GameObject PreparedObject(Transform parent)
        {
            GameObject preparedObject = AvailableObject();

            preparedObject.transform.SetParent(parent);
            preparedObject.SetActive(true);

            return preparedObject;
        }

        public GameObject PreparedObject(Vector3 postion, Transform parent)
        {
            GameObject preparedObject = AvailableObject();

            preparedObject.transform.SetParent(parent);
            preparedObject.SetActive(true);
            preparedObject.GetComponent<RectTransform>().anchoredPosition = postion;

            return preparedObject;
        }

        public GameObject PreparedObject(Vector3 postion, Quaternion rotation, Transform parent)
        {
            GameObject preparedObject = AvailableObject();

            preparedObject.transform.SetParent(parent);
            preparedObject.SetActive(true);
            preparedObject.GetComponent<RectTransform>().anchoredPosition = postion;
            preparedObject.transform.rotation = rotation;

            return preparedObject;
        }

        public GameObject PreparedObject(Vector3 postion, Vector3 localScale, Transform parent)
        {
            GameObject preparedObject = AvailableObject();

            preparedObject.transform.SetParent(parent);
            preparedObject.SetActive(true);
            preparedObject.GetComponent<RectTransform>().anchoredPosition = postion;
            preparedObject.transform.localScale = localScale;

            return preparedObject;
        }

        public GameObject PreparedObject(Vector3 postion, Quaternion rotation, Vector3 localScale, Transform parent)
        {
            GameObject preparedObject = AvailableObject();

            preparedObject.transform.SetParent(parent);
            preparedObject.SetActive(true);
            preparedObject.GetComponent<RectTransform>().anchoredPosition = postion;
            preparedObject.transform.rotation = rotation;
            preparedObject.transform.localScale = localScale;

            return preparedObject;
        }
    }
}