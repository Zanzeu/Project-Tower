using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Tower.Runtime.Core
{   
    public enum EGlobalEvent
    {
        OnUpdateMoneyUI,
        OnHurt,
        OnCheckEnemy,
    }

    public class GlobalEventManager : Singleton<GlobalEventManager>
    {   
        private readonly Dictionary<EGlobalEvent, IGameEvent> _eventDic = new();

        public UniTask InitAsync() => UniTask.CompletedTask;

        private T GetEvent<T>(EGlobalEvent state) where T : IGameEvent, new()
        {
            if (!_eventDic.TryGetValue(state, out var ev))
            {
                ev = new T();
                _eventDic[state] = ev;
            }

            return (T)ev;
        }

        public void TriggerEvent(EGlobalEvent ev) => GetEvent<GameEvent>(ev).Trigger();
        public void Subscribe(EGlobalEvent ev, EventParam eventParam) => GetEvent<GameEvent>(ev).Subscribe(eventParam);
        public void Unsubscribe(EGlobalEvent ev, Action action) => GetEvent<GameEvent>(ev).Unsubscribe(action);

        public void TriggerEvent<T1>(EGlobalEvent ev, T1 arg1) => GetEvent<GameEvent<T1>>(ev).Trigger(arg1);
        public void Subscribe<T1>(EGlobalEvent ev, EventParam<T1> eventParam) => GetEvent<GameEvent<T1>>(ev).Subscribe(eventParam);
        public void Unsubscribe<T1>(EGlobalEvent ev, Action<T1> action) => GetEvent<GameEvent<T1>>(ev).Unsubscribe(action);

        public void TriggerEvent<T1, T2>(EGlobalEvent ev, T1 arg1, T2 arg2) => GetEvent<GameEvent<T1, T2>>(ev).Trigger(arg1, arg2);
        public void Subscribe<T1, T2>(EGlobalEvent ev, EventParam<T1, T2> eventParam) => GetEvent<GameEvent<T1, T2>>(ev).Subscribe(eventParam);
        public void Unsubscribe<T1, T2>(EGlobalEvent ev, Action<T1, T2> action) => GetEvent<GameEvent<T1, T2>>(ev).Unsubscribe(action);

        public void TriggerEvent<T1, T2, T3>(EGlobalEvent ev, T1 arg1, T2 arg2, T3 arg3) => GetEvent<GameEvent<T1, T2, T3>>(ev).Trigger(arg1, arg2, arg3);
        public void Subscribe<T1, T2, T3>(EGlobalEvent ev, EventParam<T1, T2, T3> eventParam) => GetEvent<GameEvent<T1, T2, T3>>(ev).Subscribe(eventParam);
        public void Unsubscribe<T1, T2, T3>(EGlobalEvent ev, Action<T1, T2, T3> action) => GetEvent<GameEvent<T1, T2, T3>>(ev).Unsubscribe(action);

    }
}
