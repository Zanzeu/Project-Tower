using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    [Serializable]
    public class EnemyJson
    {
        [BoxGroup("基础信息"), LabelText("ID"), LabelWidth(60)] public string ID;
        [BoxGroup("基础信息"), LabelText("名称"), LabelWidth(60)] public string Name;
        [BoxGroup("基础信息"), LabelText("图标路径"), LabelWidth(60)] public string IconPath;
        [BoxGroup("基础信息"), LabelText("描述"), LabelWidth(60)] public string Des;

        [BoxGroup("属性"), LabelText("生命值"), LabelWidth(60)] public float Health;
        [BoxGroup("属性"), LabelText("速度"), LabelWidth(60)] public float Speed;
        [BoxGroup("属性"), LabelText("金币"), LabelWidth(60)] public int Money;
        [BoxGroup("属性"), LabelText("免疫Buff"), LabelWidth(60)] public List<string> ImmuneBuff;

        [Space(10)]
        [BoxGroup("效果器", CenterLabel = true)]
        [LabelText("效果器")]
        [HideLabel, PropertySpace(5)]
        [DictionaryDrawerSettings(KeyLabel = "时机", ValueLabel = "效果器")]
        [ShowInInspector, PropertyOrder(100)]
        public Dictionary<EEnemyOppo, List<EffectArray>> DefaultEffects = new Dictionary<EEnemyOppo, List<EffectArray>>();

        public EnemyData GetInstance()
        {
            return new EnemyData(this);
        }
    }

    public  class EnemyData : DataBase
    {       
        public string IconPath { get; protected set; }
        public float Health { get; protected set; }
        public float Speed { get; protected set; }
        public int Money { get; protected   set; }
        public string Des { get; protected set; }
        public List<string> ImmuneBuff { get; protected set; }
        public Dictionary<EEnemyOppo, List<EffectArray>> DefaultEffects { get; protected set; }

        public EnemyData(EnemyJson json)
        {
            ID = json.ID;
            Name = json.Name;
            IconPath = json.IconPath;
            Health = json.Health;
            Speed = json.Speed;
            Money = json.Money;
            DefaultEffects = json.DefaultEffects;
            ImmuneBuff = json.ImmuneBuff;
            Des = json.Des;
        }
    }
}