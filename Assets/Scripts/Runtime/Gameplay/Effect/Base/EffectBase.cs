using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace Tower.Runtime.Gameplay
{   
    public interface IEffectParam { }

    public abstract class EffectBase
    {
        protected IEffectParam _param;

        public string EffectName { get; private set; }
        public int ID { get; set; }

        protected EffectBase(IEffectParam param)
        {
            _param = param;
            EffectName = GetType().Name;
        }

        public abstract void OnTrigger(object caster, object target);
        public virtual void OnEnd(object caster, object target) { }
    }

    public static class EffectFactory
    {
        public static EffectBase Create(string typeName, IEffectParam param)
        {
            Type type = Type.GetType(typeName);

            if (type == null)
            {
                foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = asm.GetType(typeName);
                    if (type == null)
                    {
                        type = asm.GetType($"Tower.Runtime.Gameplay.Effect_{typeName}");
                    }
                    if (type != null)
                        break;
                }
            }

            if (type == null)
            {
                Debug.LogError($"EffectFactory: 找不到类型 {typeName}");
                return null;
            }

            if (!typeof(EffectBase).IsAssignableFrom(type))
            {
                Debug.LogError($"EffectFactory: {typeName} 不是 EffectBase 子类");
                return null;
            }

            return (EffectBase)Activator.CreateInstance(type, param);
        }
    }

    [Serializable, HideReferenceObjectPicker]
    public class EffectArray
    {
        [ValueDropdown(nameof(GetEffectTypes))]
        [OnValueChanged(nameof(OnEffectTypeChanged))]
        [LabelText("效果类型名")]
        public string effectType;

        [SerializeReference, InlineProperty, HideReferenceObjectPicker, LabelText("参数")]
        [ShowIf(nameof(HasParam))]
        public IEffectParam param;

        private bool HasParam => !string.IsNullOrEmpty(effectType);

        private static IEnumerable<string> GetEffectTypes()
        {
            return EffectConfigDic.Dic.Keys;
        }

        private void OnEffectTypeChanged()
        {
            if (EffectConfigDic.Dic.TryGetValue(effectType, out var defaultParam))
            {
                var type = defaultParam.GetType();
                var newParam = (IEffectParam)Activator.CreateInstance(type);

                param = newParam;
            }   
        }

        public EffectBase GetInstance()
        {
            if (string.IsNullOrEmpty(effectType))
            {
                Debug.LogError("EffectArray: effectType 未设置");
                return null;
            }

            return EffectFactory.Create(effectType, param);
        }
    }
}