using UnityEngine;

namespace Tower.Runtime.Core
{
    public interface ISingleton { }

    public abstract class Singleton<T> : MonoBehaviour, ISingleton where T : Component, ISingleton
    {
        private static T _instance;
        private static bool _quitting;

        /// <summary>
        /// 是否跨场景保留单例
        /// </summary>
        protected virtual bool IsPersistent => true;

        public static T Instance
        {
            get
            {
                if (_quitting) return null;
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;

                if (IsPersistent)
                {
                    DontDestroyOnLoad(gameObject);
                }

                OnAwake();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _quitting = true;
        }

        protected virtual void OnAwake() { }
    }
}
