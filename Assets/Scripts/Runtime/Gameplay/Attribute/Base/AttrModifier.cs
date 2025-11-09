using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public class AttrModifier : IAttrModifier
    {
        public EAttrForge AttrForge { get; set; }
        public bool IsPercent { get; set; }
        public float Value { get; set; }

        public AttrModifier(EAttrForge attrForge, bool isPercent, float value)
        {
            AttrForge = attrForge;
            IsPercent = isPercent;
            Value = value;
        }
    }
}
