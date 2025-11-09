using Tower.Runtime.Core;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.GameSystem
{
    public enum EGameState
    {   
        None,
        Game,
        Victory,
        Defeat,
    }

    public class GameStateSystem : SystemBase
    {
        private readonly Dictionary<EGameState, IGameEvent> _eventDic = new();

        [ShowInInspector, ReadOnly, LabelText("当前游戏状态")]
        public EGameState CurGameState { get; private set; }

        #region ==========游戏状态==========

        private T GetEvent<T>(EGameState state) where T : IGameEvent, new()
        {
            if (!_eventDic.TryGetValue(state, out var ev))
            {
                ev = new T();
                _eventDic[state] = ev;
            }

            return (T)ev;
        }

        public void SwitchGlobalState(EGameState newState, bool trigger = true)
        {
            if (CurGameState == newState)
            {
                return;
            }

            CurGameState = newState;

            if (trigger)
            {
                GetEvent<GameEvent>(newState).Trigger();
            }
        }

        public void SwitchGlobalState<T1>(EGameState newState, T1 arg1, bool trigger = true)
        {
            if (CurGameState == newState)
            {
                return;
            }

            CurGameState = newState;
            if (trigger)
            {
                GetEvent<GameEvent<T1>>(newState).Trigger(arg1);
            }
        }

        public void SwitchGlobalState<T1, T2>(EGameState newState, T1 arg1, T2 arg2, bool trigger = true)
        {
            if (CurGameState == newState)
            {
                return;
            }

            CurGameState = newState;

            if (trigger)
            {
                GetEvent<GameEvent<T1, T2>>(newState).Trigger(arg1, arg2);
            }
        }

        public void SwitchGlobalState<T1, T2, T3>(EGameState newState, T1 arg1, T2 arg2, T3 arg3, bool trigger = true)
        {
            if (CurGameState == newState)
            {
                return;
            }

            CurGameState = newState;
            if (trigger)
            {
                GetEvent<GameEvent<T1, T2, T3>>(newState).Trigger(arg1, arg2, arg3);
            }
        }

        public void Subscribe(EGameState state, EventParam eventParam) => GetEvent<GameEvent>(state).Subscribe(eventParam);
        public void Unsubscribe(EGameState state, Action action) => GetEvent<GameEvent>(state).Unsubscribe(action);

        public void Subscribe<T1>(EGameState state, EventParam<T1> eventParam) => GetEvent<GameEvent<T1>>(state).Subscribe(eventParam);
        public void Unsubscribe<T1>(EGameState state, Action<T1> action) => GetEvent<GameEvent<T1>>(state).Unsubscribe(action);

        public void Subscribe<T1, T2>(EGameState state, EventParam<T1, T2> eventParam) => GetEvent<GameEvent<T1, T2>>(state).Subscribe(eventParam);
        public void Unsubscribe<T1, T2>(EGameState state, Action<T1, T2> action) => GetEvent<GameEvent<T1, T2>>(state).Unsubscribe(action);

        public void Subscribe<T1, T2, T3>(EGameState state, EventParam<T1, T2, T3> eventParam) => GetEvent<GameEvent<T1, T2, T3>>(state).Subscribe(eventParam);
        public void Unsubscribe<T1, T2, T3>(EGameState state, Action<T1, T2, T3> action) => GetEvent<GameEvent<T1, T2, T3>>(state).Unsubscribe(action);


        #endregion
    }
}
