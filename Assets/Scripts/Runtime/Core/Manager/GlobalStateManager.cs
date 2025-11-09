using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Tower.Runtime.Core
{
    public enum EGlobalState
    {

    }

    public class GlobalStateManager : Singleton<GlobalStateManager>
    {   
        private readonly Dictionary<EGlobalState, IGameEvent> _eventDic = new();

        [ShowInInspector, ReadOnly, LabelText("当前全局状态")]
        public EGlobalState CurGlobalState { get; private set; }

        #region ==========游戏状态==========

        private T GetEvent<T>(EGlobalState state) where T : IGameEvent, new()
        {
            if (!_eventDic.TryGetValue(state, out var ev))
            {
                ev = new T();
                _eventDic[state] = ev;
            }

            return (T)ev;
        }

        public void SwitchGlobalState(EGlobalState newState, bool trigger = true)
        {
            if (CurGlobalState == newState)
            {
                return;
            }

            CurGlobalState = newState;

            if (trigger)
            {
                GetEvent<GameEvent>(newState).Trigger();
            }
        }

        public void SwitchGlobalState<T1>(EGlobalState newState, T1 arg1, bool trigger = true)
        {
            if (CurGlobalState == newState)
            {
                return;
            }

            CurGlobalState = newState;
            if (trigger)
            {
                GetEvent<GameEvent<T1>>(newState).Trigger(arg1);
            }
        }

        public void SwitchGlobalState<T1, T2>(EGlobalState newState, T1 arg1, T2 arg2, bool trigger = true)
        {
            if (CurGlobalState == newState)
            {
                return;
            }

            CurGlobalState = newState;

            if (trigger)
            {
                GetEvent<GameEvent<T1, T2>>(newState).Trigger(arg1, arg2);
            }
        }

        public void SwitchGlobalState<T1, T2, T3>(EGlobalState newState, T1 arg1, T2 arg2, T3 arg3, bool trigger = true)
        {
            if (CurGlobalState == newState)
            {
                return;
            }

            CurGlobalState = newState;
            if (trigger)
            {
                GetEvent<GameEvent<T1, T2, T3>>(newState).Trigger(arg1, arg2, arg3);
            }
        }

        public void Subscribe(EGlobalState state, EventParam eventParam) => GetEvent<GameEvent>(state).Subscribe(eventParam);
        public void Unsubscribe(EGlobalState state, Action action) => GetEvent<GameEvent>(state).Unsubscribe(action);

        public void Subscribe<T1>(EGlobalState state, EventParam<T1> eventParam) => GetEvent<GameEvent<T1>>(state).Subscribe(eventParam);
        public void Unsubscribe<T1>(EGlobalState state, Action<T1> action) => GetEvent<GameEvent<T1>>(state).Unsubscribe(action);

        public void Subscribe<T1, T2>(EGlobalState state, EventParam<T1, T2> eventParam) => GetEvent<GameEvent<T1, T2>>(state).Subscribe(eventParam);
        public void Unsubscribe<T1, T2>(EGlobalState state, Action<T1, T2> action) => GetEvent<GameEvent<T1, T2>>(state).Unsubscribe(action);

        public void Subscribe<T1, T2, T3>(EGlobalState state, EventParam<T1, T2, T3> eventParam) => GetEvent<GameEvent<T1, T2, T3>>(state).Subscribe(eventParam);
        public void Unsubscribe<T1, T2, T3>(EGlobalState state, Action<T1, T2, T3> action) => GetEvent<GameEvent<T1, T2, T3>>(state).Unsubscribe(action);

        #endregion
    }
}