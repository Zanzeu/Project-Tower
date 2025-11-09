using System;
using System.Collections.Generic;

namespace Tower.Runtime.Common
{
    /// <summary>
    /// 时间轴模型
    /// </summary>
    public class TimelineModel
    {
        /// <summary>
        /// 时间轴总持续时间
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// 时间轴节点
        /// </summary>
        public List<TimelineNode> Nodes { get; private set; } = new List<TimelineNode>();

        /// <summary>
        /// 模型
        /// </summary>
        /// <param name="duration">持续总时间</param>
        /// <param name="nodes">执行节点</param>
        public TimelineModel(float duration, List<TimelineNode> nodes)
        {
            Duration = duration;
            Nodes = nodes;
        }
    }

    /// <summary>
    /// 时间轴执行对象
    /// </summary>
    public class Timeline
    {
        public TimelineModel Model { get; private set; }
        public object Caster { get; private set; }
        public float TimeElapsed { get; private set; } = 0f;
        public float TimeScale { get; set; } = 1f;
        public bool IsFinished { get; private set; } = false;

        private int _currentIndex = 0;

        /// <summary>
        /// Timeline完成回调
        /// </summary>
        public Action<Timeline> OnTimelineFinish;

        public void OnSpawn(TimelineModel model, object caster)
        {
            Model = model;
            Caster = caster;
        }

        public void OnEnpool()
        {
            TimeElapsed = 0f;
            _currentIndex = 0;
            IsFinished = false;
            Model = null;
            Caster = null;
        }

        public void OnUpdate(float deltaTime)
        {
            if (IsFinished || Model == null || Model.Nodes.Count == 0)
            {
                return;
            }

            TimeElapsed += deltaTime * TimeScale;

            while (_currentIndex < Model.Nodes.Count && TimeElapsed >= Model.Nodes[_currentIndex].TimeElapsed)
            {
                var node = Model.Nodes[_currentIndex];

                node.ITimelineEvent?.Execute(this);

                _currentIndex++;
            }

            if (TimeElapsed >= Model.Duration && !IsFinished)
            {
                IsFinished = true;
                OnTimelineFinish?.Invoke(this);
            }
        }

        /// <summary>
        /// 手动重置Timeline
        /// </summary>
        public void Reset()
        {
            TimeElapsed = 0f;
            _currentIndex = 0;
            IsFinished = false;
        }
    }

    /// <summary>
    /// 时间轴节点
    /// </summary>
    public class TimelineNode
    {
        /// <summary>
        /// 节点触发时间
        /// </summary>
        public float TimeElapsed { get; set; }

        /// <summary>
        /// 执行事件
        /// </summary>
        public ITimelineEvent ITimelineEvent { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        /// <param name="timeElapsed">执行时间点</param>
        /// <param name="iTimelineEvent">事件</param>
        public TimelineNode(float timeElapsed, ITimelineEvent iTimelineEvent)
        {
            TimeElapsed = timeElapsed;
            ITimelineEvent = iTimelineEvent;
        }
    }
}