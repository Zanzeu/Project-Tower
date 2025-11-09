using Cysharp.Threading.Tasks;
using Tower.Runtime.Core;
using System;
using System.Collections.Generic;

namespace Tower.Runtime.GameSystem
{
    public enum EGameEvent
    {

    }

    public class GameEventSystem : SystemBase
    {   

        private readonly Dictionary<EGameEvent, IGameEvent> _eventDic = new();

        public UniTask InitAsync() => UniTask.CompletedTask;

        private T GetEvent<T>(EGameEvent state) where T : IGameEvent, new()
        {
            if (!_eventDic.TryGetValue(state, out var ev))
            {
                ev = new T();
                _eventDic[state] = ev;
            }

            return (T)ev;
        }

        public void TriggerEvent(EGameEvent ev) => GetEvent<GameEvent>(ev).Trigger();
        public void Subscribe(EGameEvent ev, EventParam eventParam) => GetEvent<GameEvent>(ev).Subscribe(eventParam);
        public void Unsubscribe(EGameEvent ev, Action action) => GetEvent<GameEvent>(ev).Unsubscribe(action);

        public void TriggerEvent<T1>(EGameEvent ev, T1 arg1) => GetEvent<GameEvent<T1>>(ev).Trigger(arg1);
        public void Subscribe<T1>(EGameEvent ev, EventParam<T1> eventParam) => GetEvent<GameEvent<T1>>(ev).Subscribe(eventParam);
        public void Unsubscribe<T1>(EGameEvent ev, Action<T1> action) => GetEvent<GameEvent<T1>>(ev).Unsubscribe(action);

        public void TriggerEvent<T1, T2>(EGameEvent ev, T1 arg1, T2 arg2) => GetEvent<GameEvent<T1, T2>>(ev).Trigger(arg1, arg2);
        public void Subscribe<T1, T2>(EGameEvent ev, EventParam<T1, T2> eventParam) => GetEvent<GameEvent<T1, T2>>(ev).Subscribe(eventParam);
        public void Unsubscribe<T1, T2>(EGameEvent ev, Action<T1, T2> action) => GetEvent<GameEvent<T1, T2>>(ev).Unsubscribe(action);

        public void TriggerEvent<T1, T2, T3>(EGameEvent ev, T1 arg1, T2 arg2, T3 arg3) => GetEvent<GameEvent<T1, T2, T3>>(ev).Trigger(arg1, arg2, arg3);
        public void Subscribe<T1, T2, T3>(EGameEvent ev, EventParam<T1, T2, T3> eventParam) => GetEvent<GameEvent<T1, T2, T3>>(ev).Subscribe(eventParam);
        public void Unsubscribe<T1, T2, T3>(EGameEvent ev, Action<T1, T2, T3> action) => GetEvent<GameEvent<T1, T2, T3>>(ev).Unsubscribe(action);

    }
}