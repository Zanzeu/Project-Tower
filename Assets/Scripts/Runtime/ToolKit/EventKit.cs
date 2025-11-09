using System;
using Tower.Runtime.Core;
using Tower.Runtime.GameSystem;

namespace Tower.Runtime.ToolKit
{
    public static class EventKit
    {
        public struct GlobalState
        {
            public static EGlobalState CurrentState => GlobalStateManager.Instance.CurGlobalState;

            public static void Switch(EGlobalState newState, bool trigger = true) => GlobalStateManager.Instance.SwitchGlobalState(newState, trigger);
            public static void Switch<T1>(EGlobalState newState, T1 arg1, bool trigger = true) => GlobalStateManager.Instance.SwitchGlobalState(newState, arg1, trigger);
            public static void Switch<T1, T2>(EGlobalState newState, T1 arg1, T2 arg2, bool trigger = true) => GlobalStateManager.Instance.SwitchGlobalState(newState, arg1, arg2, trigger);
            public static void Switch<T1, T2, T3>(EGlobalState newState, T1 arg1, T2 arg2, T3 arg3, bool trigger = true) => GlobalStateManager.Instance.SwitchGlobalState(newState, arg1, arg2, arg3, trigger);

            public static void Subscribe(EGlobalState state, EventParam eventParam) => GlobalStateManager.Instance.Subscribe(state, eventParam);
            public static void Unsubscribe(EGlobalState state, Action action) => GlobalStateManager.Instance?.Unsubscribe(state, action);

            public static void Subscribe<T1>(EGlobalState state, EventParam<T1> eventParam) => GlobalStateManager.Instance.Subscribe(state, eventParam);
            public static void Unsubscribe<T1>(EGlobalState state, Action<T1> action) => GlobalStateManager.Instance?.Unsubscribe(state, action);

            public static void Subscribe<T1, T2>(EGlobalState state, EventParam<T1, T2> eventParam) => GlobalStateManager.Instance.Subscribe(state, eventParam);
            public static void Unsubscribe<T1, T2>(EGlobalState state, Action<T1, T2> action) => GlobalStateManager.Instance?.Unsubscribe(state, action);

            public static void Subscribe<T1, T2, T3>(EGlobalState state, EventParam<T1, T2, T3> eventParam) => GlobalStateManager.Instance.Subscribe(state, eventParam);
            public static void Unsubscribe<T1, T2, T3>(EGlobalState state, Action<T1, T2, T3> action) => GlobalStateManager.Instance?.Unsubscribe(state, action);
        }

        public struct GlobalEvent
        {
            public static void Trigger(EGlobalEvent ev) => GlobalEventManager.Instance.TriggerEvent(ev);
            public static void Subscribe(EGlobalEvent ev, EventParam eventParam) => GlobalEventManager.Instance.Subscribe(ev, eventParam);
            public static void Unsubscribe(EGlobalEvent ev, Action action) => GlobalEventManager.Instance?.Unsubscribe(ev, action);

            public static void Trigger<T1>(EGlobalEvent ev, T1 arg1) => GlobalEventManager.Instance.TriggerEvent(ev, arg1);
            public static void Subscribe<T1>(EGlobalEvent ev, EventParam<T1> eventParam) => GlobalEventManager.Instance.Subscribe(ev, eventParam);
            public static void Unsubscribe<T1>(EGlobalEvent ev, Action<T1> action) => GlobalEventManager.Instance?.Unsubscribe(ev, action);

            public static void Trigger<T1, T2>(EGlobalEvent ev, T1 arg1, T2 arg2) => GlobalEventManager.Instance.TriggerEvent(ev, arg1, arg2);
            public static void Subscribe<T1, T2>(EGlobalEvent ev, EventParam<T1, T2> eventParam) => GlobalEventManager.Instance.Subscribe(ev, eventParam);
            public static void Unsubscribe<T1, T2>(EGlobalEvent ev, Action<T1, T2> action) => GlobalEventManager.Instance?.Unsubscribe(ev, action);

            public static void Trigger<T1, T2, T3>(EGlobalEvent ev, T1 arg1, T2 arg2, T3 arg3) => GlobalEventManager.Instance.TriggerEvent(ev, arg1, arg2, arg3);
            public static void Subscribe<T1, T2, T3>(EGlobalEvent ev, EventParam<T1, T2, T3> eventParam) => GlobalEventManager.Instance.Subscribe(ev, eventParam);
            public static void Unsubscribe<T1, T2, T3>(EGlobalEvent ev, Action<T1, T2, T3> action) => GlobalEventManager.Instance?.Unsubscribe(ev, action);
        }

        public struct GameState
        {
            public static EGameState CurrentState => SystemKit.GetSystem<GameStateSystem>().CurGameState;

            public static void Switch(EGameState newState, bool trigger = true) => SystemKit.GetSystem<GameStateSystem>().SwitchGlobalState(newState, trigger);
            public static void Switch<T1>(EGameState newState, T1 arg1, bool trigger = true) => SystemKit.GetSystem<GameStateSystem>().SwitchGlobalState(newState, arg1, trigger);
            public static void Switch<T1, T2>(EGameState newState, T1 arg1, T2 arg2, bool trigger = true) => SystemKit.GetSystem<GameStateSystem>().SwitchGlobalState(newState, arg1, arg2, trigger);
            public static void Switch<T1, T2, T3>(EGameState newState, T1 arg1, T2 arg2, T3 arg3, bool trigger = true) => SystemKit.GetSystem<GameStateSystem>().SwitchGlobalState(newState, arg1, arg2, arg3, trigger);

            public static void Subscribe(EGameState state, EventParam eventParam) => SystemKit.GetSystem<GameStateSystem>().Subscribe(state, eventParam);
            public static void Unsubscribe(EGameState state, Action action) => SystemKit.GetSystem<GameStateSystem>()?.Unsubscribe(state, action);

            public static void Subscribe<T1>(EGameState state, EventParam<T1> eventParam) => SystemKit.GetSystem<GameStateSystem>().Subscribe(state, eventParam);
            public static void Unsubscribe<T1>(EGameState state, Action<T1> action) => SystemKit.GetSystem<GameStateSystem>().Unsubscribe(state, action);

            public static void Subscribe<T1, T2>(EGameState state, EventParam<T1, T2> eventParam) => SystemKit.GetSystem<GameStateSystem>().Subscribe(state, eventParam);
            public static void Unsubscribe<T1, T2>(EGameState state, Action<T1, T2> action) => SystemKit.GetSystem<GameStateSystem>().Unsubscribe(state, action);

            public static void Subscribe<T1, T2, T3>(EGameState state, EventParam<T1, T2, T3> eventParam) => SystemKit.GetSystem<GameStateSystem>().Subscribe(state, eventParam);
            public static void Unsubscribe<T1, T2, T3>(EGameState state, Action<T1, T2, T3> action) => SystemKit.GetSystem<GameStateSystem>().Unsubscribe(state, action);
        }

        public struct GameEvent
        {
            public static void Trigger(EGameEvent ev) => SystemKit.GetSystem<GameEventSystem>().TriggerEvent(ev);
            public static void Subscribe(EGameEvent ev, EventParam eventParam) => SystemKit.GetSystem<GameEventSystem>().Subscribe(ev, eventParam);
            public static void Unsubscribe(EGameEvent ev, Action action) => SystemKit.GetSystem<GameEventSystem>().Unsubscribe(ev, action);

            public static void Trigger<T1>(EGameEvent ev, T1 arg1) => SystemKit.GetSystem<GameEventSystem>().TriggerEvent(ev, arg1);
            public static void Subscribe<T1>(EGameEvent ev, EventParam<T1> eventParam) => SystemKit.GetSystem<GameEventSystem>().Subscribe(ev, eventParam);
            public static void Unsubscribe<T1>(EGameEvent ev, Action<T1> action) => SystemKit.GetSystem<GameEventSystem>().Unsubscribe(ev, action);

            public static void Trigger<T1, T2>(EGameEvent ev, T1 arg1, T2 arg2) => SystemKit.GetSystem<GameEventSystem>().TriggerEvent(ev, arg1, arg2);
            public static void Subscribe<T1, T2>(EGameEvent ev, EventParam<T1, T2> eventParam) => SystemKit.GetSystem<GameEventSystem>().Subscribe(ev, eventParam);
            public static void Unsubscribe<T1, T2>(EGameEvent ev, Action<T1, T2> action) => SystemKit.GetSystem<GameEventSystem>().Unsubscribe(ev, action);

            public static void Trigger<T1, T2, T3>(EGameEvent ev, T1 arg1, T2 arg2, T3 arg3) => SystemKit.GetSystem<GameEventSystem>().TriggerEvent(ev, arg1, arg2, arg3);
            public static void Subscribe<T1, T2, T3>(EGameEvent ev, EventParam<T1, T2, T3> eventParam) => SystemKit.GetSystem<GameEventSystem>().Subscribe(ev, eventParam);
            public static void Unsubscribe<T1, T2, T3>(EGameEvent ev, Action<T1, T2, T3> action) => SystemKit.GetSystem<GameEventSystem>().Unsubscribe(ev, action);
        }
    }
}