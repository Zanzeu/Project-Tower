using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{   
    public enum EBuffType
    {
        Buff,
        Debuff
    }

    public enum EBuffOppo
    {
        Start,
        End,
        Tick,
        Stack,
    }

    [Serializable]
    public class BuffJson
    {
        [BoxGroup("基础信息"), LabelText("ID"), LabelWidth(60)]
        public string ID;

        [BoxGroup("基础信息"), LabelText("名字"), LabelWidth(60)]
        public string Name;

        [BoxGroup("基础信息"), LabelText("类型"), LabelWidth(60)]
        public EBuffType Type;

        [BoxGroup("基础信息"), LabelText("图标路径"), LabelWidth(60)]
        public string IconPath;

        [BoxGroup("基础信息"), LabelText("描述"), LabelWidth(60)]
        public string Description;

        [BoxGroup("属性"), LabelText("最大层数"), LabelWidth(60)]
        public int MaxStack;

        [BoxGroup("属性"), LabelText("持续时间"), LabelWidth(60)]
        public float Duration;

        [BoxGroup("属性"), LabelText("触发间隔"), LabelWidth(60)]
        public float TickInterval;

        [Space(10)]
        [BoxGroup("等级效果器", CenterLabel = true)]
        [LabelText("等级效果器")]
        [HideLabel, PropertySpace(5)]
        [DictionaryDrawerSettings(KeyLabel = "时机", ValueLabel = "效果器")]
        [ShowInInspector, PropertyOrder(100)] public Dictionary<EBuffOppo, List<EffectArray>> Effects;

        public BuffData GetInstance(object caster, object target)
        {
            return new BuffData(this, caster, target);
        }
    }

    [System.Serializable]
    public class BuffData : DataBase
    {   
        public EBuffType Type { get; protected set; }
        public string IconPath { get; protected set; }
        public string Description { get; protected set; }
        public int MaxStack { get; protected set; }
        public float Duration { get; protected set; }
        public float TickInterval { get; protected set; }
        public Dictionary<EBuffOppo, List<EffectBase>> Effects { get; protected set; }
        public object Caster { get; protected set; }
        public object Target { get; protected set; }
        [ShowInInspector] public float LastTime { get; set; }
        public float TickTimer { get; set; }
        [ShowInInspector] public int CurStack { get; set; }

        public bool IsPermanent => Duration == -1f;

        public BuffData(BuffJson json, object caster, object target)
        {
            ID = json.ID;
            Name = json.Name;
            Type = json.Type;
            IconPath = json.IconPath;
            Description = json.Description;
            MaxStack = json.MaxStack;
            Duration = json.Duration;
            TickInterval = json.TickInterval;
            Caster = caster;
            Target = target;

            LastTime = Duration;
            TickTimer = TickInterval;

            Effects = new Dictionary<EBuffOppo, List<EffectBase>>();

            foreach (var array in json.Effects)
            {
                foreach (var effect in array.Value)
                {
                    if (!Effects.ContainsKey(array.Key))
                    {
                        Effects[array.Key] = new List<EffectBase>();
                    }

                    Effects[array.Key].Add(effect.GetInstance());
                }
            }
        }


        public void OnAwake()
        {
            OnStart();
        }

        private void OnStart()
        {
            TriggerEffect(EBuffOppo.Start);
        }

        public void OnTick()
        {
            TriggerEffect(EBuffOppo.Tick);
        }

        public void OnEnd()
        {
            if (Effects.TryGetValue(EBuffOppo.Start, out var effect))
            {
                foreach (var eff in effect)
                {
                    eff.OnEnd(this, Target);
                }
            }

            OnDestroy();
        }

        private void OnDestroy()
        {

        }

        public void DefaultStack(int stack)
        {
            CurStack = stack;
        }

        public void OnStack(int stack)
        {
            CurStack = Mathf.Min(MaxStack, CurStack + stack);

            TriggerEffect(EBuffOppo.Stack);
        }

        public bool RemoveStack(int stack)
        {
            CurStack -= stack;

            return CurStack <= 0;
        }

        public void ImmediateTick()
        {
            TriggerEffect(EBuffOppo.Tick);
        }

        private void TriggerEffect(EBuffOppo oppo)
        {
            if (Effects.TryGetValue(oppo, out var effect))
            {
                foreach (var eff in effect)
                {
                    eff.OnTrigger(this, Target);
                }
            }
        }
    }
}
