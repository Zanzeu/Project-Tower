using System;
using System.Collections.Generic;

namespace Tower.Runtime.Core
{
    public interface IGameEvent { }

    public struct EventParam
    {
        public Action Action;
        public int Priority;

        public EventParam(Action action, int priority)
        {
            Action = action;
            Priority = priority;
        }
    }

    public class GameEvent : IGameEvent
    {
        private readonly List<EventParam> _events = new List<EventParam>();

        public void Subscribe(EventParam param)
        {
            if (param.Action == null)
            {
                return;
            }

            int index = _events.FindIndex(e => param.Priority > e.Priority);

            if (index >= 0)
            {
                _events.Insert(index, param);
            }
            else
            {
                _events.Add(param);
            }
        }

        public void Unsubscribe(Action action) => _events.RemoveAll(e => e.Action == action);

        public void Trigger()
        {
            foreach (var e in _events)
            {
                e.Action();
            }
        }

        public void Clear() => _events.Clear();
    }

    public struct EventParam<T1>
    {
        public Action<T1> Action;
        public int Priority;

        public EventParam(Action<T1> action, int priority)
        {
            Action = action;
            Priority = priority;
        }
    }

    public class GameEvent<T1> : IGameEvent
    {
        private readonly List<EventParam<T1>> _events = new List<EventParam<T1>>();

        public void Subscribe(EventParam<T1> param)
        {
            if (param.Action == null)
            {
                return;
            }

            int index = _events.FindIndex(e => param.Priority > e.Priority);

            if (index >= 0)
            {
                _events.Insert(index, param);
            }
            else
            {
                _events.Add(param);
            }
        }

        public void Unsubscribe(Action<T1> action) => _events.RemoveAll(e => e.Action == action);

        public void Trigger(T1 arg1)
        {
            foreach (var e in _events)
            {
                e.Action(arg1);
            }
        }

        public void Clear() => _events.Clear();
    }

    public struct EventParam<T1, T2>
    {
        public Action<T1, T2> Action;
        public int Priority;

        public EventParam(Action<T1, T2> action, int priority)
        {
            Action = action;
            Priority = priority;
        }
    }

    public class GameEvent<T1, T2> : IGameEvent
    {
        private readonly List<EventParam<T1, T2>> _events = new List<EventParam<T1, T2>>();

        public void Subscribe(EventParam<T1, T2> param)
        {
            if (param.Action == null)
            {
                return;
            }

            int index = _events.FindIndex(e => param.Priority > e.Priority);

            if (index >= 0)
            {
                _events.Insert(index, param);
            }
            else
            {
                _events.Add(param);
            }
        }

        public void Unsubscribe(Action<T1, T2> action) => _events.RemoveAll(e => e.Action == action);

        public void Trigger(T1 arg1, T2 arg2)
        {
            foreach (var e in _events)
            {
                e.Action(arg1, arg2);
            }
        }

        public void Clear() => _events.Clear();
    }

    public struct EventParam<T1, T2, T3>
    {
        public Action<T1, T2, T3> Action;
        public int Priority;

        public EventParam(Action<T1, T2, T3> action, int priority)
        {
            Action = action;
            Priority = priority;
        }
    }

    public class GameEvent<T1, T2, T3> : IGameEvent
    {
        private readonly List<EventParam<T1, T2, T3>> _events = new List<EventParam<T1, T2, T3>>();

        public void Subscribe(EventParam<T1, T2, T3> param)
        {
            if (param.Action == null)
            {
                return;
            }

            int index = _events.FindIndex(e => param.Priority > e.Priority);

            if (index >= 0)
            {
                _events.Insert(index, param);
            }
            else
            {
                _events.Add(param);
            }
        }

        public void Unsubscribe(Action<T1, T2, T3> action) => _events.RemoveAll(e => e.Action == action);

        public void Trigger(T1 arg1, T2 arg2, T3 arg3)
        {
            foreach (var e in _events)
            {
                e.Action(arg1, arg2, arg3);
            }
        }

        public void Clear() => _events.Clear();
    }
}