using Tower.Runtime.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Core
{
    public class TimelineManager : Singleton<TimelineManager>
    {
        protected override bool IsPersistent => false;

        /// <summary>
        /// 所有正在运行的 Timeline
        /// </summary>
        private readonly List<Timeline> _activeTimelines = new List<Timeline>();

        /// <summary>
        /// 等待添加的 Timeline
        /// </summary>
        private readonly List<Timeline> _waitingAdd = new List<Timeline>();

        /// <summary>
        /// Timeline 对象池
        /// </summary>
        private readonly Stack<Timeline> _pool = new Stack<Timeline>();

        /// <summary>
        /// 是否在 Update 中
        /// </summary>
        private bool _isUpdating = false;

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            _isUpdating = true;

            for (int i = _activeTimelines.Count - 1; i >= 0; i--)
            {
                var timeline = _activeTimelines[i];
                timeline.OnUpdate(deltaTime);

                if (timeline.TimeElapsed >= timeline.Model.Duration)
                {
                    _activeTimelines.RemoveAt(i);
                    RecycleTimeline(timeline);
                }
            }

            _isUpdating = false;

            if (_waitingAdd.Count > 0)
            {
                _activeTimelines.AddRange(_waitingAdd);
                _waitingAdd.Clear();
            }
        }

        /// <summary>
        /// 创建并运行 Timeline
        /// </summary>
        public Timeline PlayTimeline(TimelineModel model, object caster)
        {
            if (model == null)
            {
                return null;
            }

            var timeline = GetTimelineFromPool();
            timeline.OnSpawn(model, caster);

            if (_isUpdating)
            {
                _waitingAdd.Add(timeline);
            }
            else
            {
                _activeTimelines.Add(timeline);
            }

            return timeline;
        }

        /// <summary>
        /// 手动停止 Timeline
        /// </summary>
        public void StopTimeline(Timeline timeline)
        {
            if (timeline == null)
            {
                return;
            }

            if (_activeTimelines.Remove(timeline))
            {
                RecycleTimeline(timeline);
            }

            _waitingAdd.Remove(timeline);
        }

        /// <summary>
        /// 停止所有 Timeline
        /// </summary>
        public void StopAllTimelines()
        {
            foreach (var t in _activeTimelines)
            {
                RecycleTimeline(t);
            }

            _activeTimelines.Clear();
            _waitingAdd.Clear();
        }

        private Timeline GetTimelineFromPool()
        {
            if (_pool.Count > 0)
            {
                return _pool.Pop();
            }

            return new Timeline();
        }

        private void RecycleTimeline(Timeline timeline)
        {
            timeline.OnEnpool();
            _pool.Push(timeline);
        }

        /// <summary>
        /// 清空对象池
        /// </summary>
        public void ClearPool()
        {
            _pool.Clear();
        }
    }
}
