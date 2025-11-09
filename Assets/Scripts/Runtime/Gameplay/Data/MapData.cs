using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    [System.Serializable]
    public struct CountInfo
    {   
        [LabelText("敌人ID")] public string EnemyID;
        [LabelText("生成数量")] public int SpawnCount;
    }

    [System.Serializable]
    public struct LevelInfo
    {
        [LabelText("关卡ID")] public string ID;
        [LabelText("关卡名称")] public string Name;
        [LabelText("生成间隔")] public int SpawnInterval;
        [LabelText("途径点"), ReadOnly, Space(10)] public List<Vector2> PointPositions;
        [LabelText("关卡配置")] public List<WaveInfo> WaveConfig;

        public LevelInfo(string id, string name, int spawnInterval, List<Vector2> pointPositions, List<WaveInfo> waveConfig)
        {
            ID = id;
            Name = name;
            SpawnInterval = spawnInterval;
            PointPositions = pointPositions;
            WaveConfig = waveConfig;
        }
    }

    [System.Serializable]
    public class LevelListData
    {
        public List<LevelInfo> Levels;
    }

    [System.Serializable]
    public struct WaveInfo
    {
        public List<CountInfo> Waves;
    }

    public class MapData : DataBase
    {   
        public int SpawnInterval { get; set; }
        public List<Vector2> PointPositions { get; set; }
        public List<WaveInfo> WaveConfig { get; set; }

        public MapData(string id,string name,int spawnInterval,  List<Vector2> pointPositions, List<WaveInfo> waveConfig)
        {
            ID = id;
            Name = name;
            SpawnInterval = spawnInterval;
            PointPositions = pointPositions;
            WaveConfig = waveConfig;
        }
    }
}