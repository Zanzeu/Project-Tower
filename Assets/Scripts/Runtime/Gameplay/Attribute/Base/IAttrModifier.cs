using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public interface IAttrModifier
    {
        EAttrForge AttrForge { get; set; }
        bool IsPercent { get; set; }
        float Value { get; set; }
    }
}