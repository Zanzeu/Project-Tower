using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    [Serializable]
    public class TowerJson
    {
        [BoxGroup("基础信息"), LabelText("ID"), LabelWidth(60)]
        public string ID;

        [BoxGroup("基础信息"), LabelText("名称"), LabelWidth(60)]
        public string Name;

        [BoxGroup("基础信息"), LabelText("图标路径"), LabelWidth(60)]
        public string IconPath;

        [BoxGroup("基础信息"), LabelText("描述"), LabelWidth(60)]
        public string Des;

        [BoxGroup("属性"), LabelText("攻击力"), LabelWidth(60)]
        public float Attack;

        [BoxGroup("属性"), LabelText("攻击速度"), LabelWidth(60)]
        public float HitSpeed;

        [BoxGroup("属性"), LabelText("攻击范围"), LabelWidth(60)]
        public float Range;

        [BoxGroup("属性"), LabelText("建造花费"), LabelWidth(60)]
        public int Cost;

        [BoxGroup("属性"), LabelText("下一等级ID"), LabelWidth(60)]
        public string NextID;

        [Space(10)]
        [BoxGroup("等级效果器", CenterLabel = true)]
        [LabelText("等级效果器")]
        [HideLabel, PropertySpace(5)]
        [DictionaryDrawerSettings(KeyLabel = "时机",ValueLabel = "效果器")]
        [ShowInInspector, PropertyOrder(100)]
        public Dictionary<ETowerOppo, List<EffectArray>> DefaultEffects = new Dictionary<ETowerOppo, List<EffectArray>>();

        public TowerData GetInstance()
        {
            return new TowerData(this);
        }
    }

    public class TowerData : DataBase
    {
        public string IconPath { get; protected set; }
        public int Cost { get; protected set; }
        public float Attack { get; protected set; }
        public float HitSpeed { get; protected set; }
        public float Range { get; protected set; }
        public string NextID { get; protected set; }
        public string Des { get; protected set; }
        public Dictionary<ETowerOppo, List<EffectArray>> DefaultEffects { get; protected set; }

        public TowerData(TowerJson json)
        {
            ID = json.ID;
            Name = json.Name;
            IconPath = json.IconPath;
            Attack = json.Attack;
            HitSpeed = json.HitSpeed;
            Range = json.Range;
            DefaultEffects = json.DefaultEffects;
            NextID = json.NextID;
            Cost = json.Cost;
            Des = json.Des;
        }
    }
}
