using Tower.Runtime.Gameplay;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using System;
using UnityEngine;

namespace Tower.Runtime.Common
{
    /// <summary>
    /// Timeline事件接口
    /// </summary>
    public interface ITimelineEvent
    {
        void Execute(Timeline timeline);
    }

    /// <summary>
    /// 播放动画时间轴事件
    /// </summary>
    public class PlayAnimationTimelineEvent : ITimelineEvent
    {
        private string _animName;

        public PlayAnimationTimelineEvent(string animName)
        {
            _animName = animName;
        }

        public void Execute(Timeline timeline)
        {
            if (timeline.Caster is GameObject caster)
            {
                var anim = caster.GetComponentInChildren<Animator>();
                if (anim == null)
                {
                    Debug.LogError($"{caster.name}无法获取到动画组件！");

                    return;
                }

                anim.Play(_animName);
            }
        }
    }

    /// <summary>
    /// 执行行为时间轴事件
    /// </summary>
    public class ExecuteActionTimelineEvent : ITimelineEvent
    {
        private Action _action;

        public ExecuteActionTimelineEvent(Action action)
        {
            _action = action;
        }

        public void Execute(Timeline timeline)
        {   
            if (_action == null)
            {
                Debug.LogError($"{timeline.Caster}无法获取到行为方法！");
                return;
            }

            _action();
        }
    }

    /// <summary>
    /// 发射子弹时间轴事件
    /// </summary>
    public class FireBulletTimelineEvent : ITimelineEvent
    {
        private BulletBase _bullet;

        public FireBulletTimelineEvent(BulletBase bullet)
        {
            _bullet = bullet;
        }

        public void Execute(Timeline timeline)
        {
            if (_bullet == null)
            {
                Debug.LogError($"{timeline.Caster}无法获取到子弹！");
                return;
            }

            SystemKit.GetSystem<BulletSystem>().FireBullet(_bullet);
        }
    }

    /// <summary>
    /// 创建AoE时间轴事件
    /// </summary>
    public class CreateAoETimelineEvent : ITimelineEvent
    {
        private AoEBase _AoE;

        public CreateAoETimelineEvent(AoEBase aoE)
        {
            _AoE = aoE;
        }

        public void Execute(Timeline timeline)
        {
            if (_AoE == null)
            {
                Debug.LogError($"{timeline.Caster}无法获取到AoE！");
                return;
            }
        }
    }
}