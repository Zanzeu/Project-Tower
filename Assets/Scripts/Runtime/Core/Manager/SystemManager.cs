using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Tower.Runtime.GameSystem;
using Tower.Runtime.Gameplay;

namespace Tower.Runtime.Core
{   
    public class SystemManager : Singleton<SystemManager>
    {   
        protected override bool IsPersistent => false;

        [SerializeField, LabelText("调试")] private bool Debugger;
        [ShowInInspector,LabelText("系统列表"),ShowIf("Debugger")] private readonly Dictionary<Type, ISystem> _systemDic = new();

        protected override async void OnAwake()
        {
            Register<GameStateSystem>();
            Register<GameEventSystem>();
            Register<AgentSystem>();
            Register<LevelSystem>();
            Register<BulletSystem>();
            Register<BuildSystem>();
            Register<StoreSystem>();

            foreach (var system in _systemDic.Values)
            {
                system.OnAwake();
            }

            await UniTask.NextFrame();

            foreach (var system in _systemDic.Values)
            {
                system.OnStart();
            }
        }

        private void Update()
        {
            foreach (var system in _systemDic.Values)
            {
                if (system is ISystemUpdate update)
                {
                    update.OnUpdate();
                }
            }
        }

        private T Register<T>() where T : class, ISystem
        {
            var type = typeof(T);
            if (_systemDic.ContainsKey(type))
            {
                return _systemDic[type] as T;
            }

            var ctor = type.GetConstructors()[0];
            var parameters = ctor.GetParameters();
            var args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var paramType = parameters[i].ParameterType;
                if (_systemDic.TryGetValue(paramType, out var dep))
                {
                    args[i] = dep;
                }
                else
                {
                    var method = typeof(SystemManager).GetMethod(nameof(Register))!.MakeGenericMethod(paramType);
                    dep = (ISystem)method.Invoke(this, null);
                    args[i] = dep;
                }
            }

            var system = Activator.CreateInstance(type, args) as T;
            _systemDic[type] = system;

            return system;
        }

        public T Get<T>() where T : class, ISystem
        {
            if (_systemDic.TryGetValue(typeof(T), out var system))
            {
                return system as T;
            }

            Debug.LogError($"系统 {typeof(T).Name} 不存在");
            return null;
        }
    }
}