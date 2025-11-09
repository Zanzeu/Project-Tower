using Sirenix.OdinInspector;
using System.Collections.Generic;
using Tower.Runtime.Common;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    /// <summary>
    /// 效果器：CreateAoE
    /// 自动生成于 2025-11-08 21:36:26
    /// </summary>
    public class Effect_CreateAoE : EffectBase
    {
        private readonly EffectParam_CreateAoE _p;
        private List<EffectBase> effectBases;

        public Effect_CreateAoE(IEffectParam param) : base(param)
        {
            _p = (EffectParam_CreateAoE)param;

            effectBases = new List<EffectBase>();
            foreach (var effect in _p.effectArrays)
            {
                effectBases.Add(effect.GetInstance());
            }
        }

        public override void OnTrigger(object caster, object target)
        {   
            
        }
    }

    [System.Serializable]
    public class EffectParam_CreateAoE : IEffectParam
    {

        [LabelText("效果器")] public List<EffectArray> effectArrays;
    }
}