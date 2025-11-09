using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Tower.Runtime.Common;
using Tower.Runtime.Gameplay;
using Tower.Runtime.ToolKit;
using Tower.Runtime.UI;
using UnityEngine;

namespace Tower.Runtime.GameSystem
{
    public class LevelSystem : SystemBase, ISystemUpdate
    {   
        public MapData Data { get; private set; }

        public LevelSystem(AgentSystem agentSystem)
        {
            m_agentSystem = agentSystem;
        }

        public List<Vector2> Path => Data.PointPositions;

        private AgentSystem m_agentSystem;
        private int m_wave;

        private int m_lift;

        private bool _spawning;

        public bool LastWave { get; private set; }

        private bool m_init = false;

        private Dictionary<string, bool> m_show = new Dictionary<string, bool>();


        public override void OnStart()
        {
            SetMapData(DataKit.GetMapData("map_level_1"));
            EventKit.GameState.Switch(EGameState.Game);
            _spawning = false;
            LastWave = false;
            Time.timeScale = 1f;
            m_lift = 20;

            m_init = true;
        }

        public void SetMapData(MapData data)
        {
            Data = data;
            m_timer = Data.SpawnInterval;
            UIKit.GetUI<GamePanel>().ShowSpawnPoint((int)m_timer);
            m_wave = 0;
        }

        private float m_timer;
        public async void OnUpdate()
        {   
            if (_spawning || !m_init)
            {
                return;
            }

            m_timer -= Time.deltaTime;
            UIKit.GetUI<GamePanel>().UpdateSpawnPointTime((int)m_timer);

            if (m_timer <= 0f)
            {
                _spawning = true;
                await SpawnWave(Data.WaveConfig[m_wave].Waves);

                m_wave++;

                if (m_wave >= Data.WaveConfig.Count)
                {
                    LastWave = true;
                    Debug.LogWarning("½áÊø");
                    return;
                }

                await UniTask.Delay(5000);

                m_timer = Data.SpawnInterval;
                UIKit.GetUI<GamePanel>().ShowSpawnPoint((int)m_timer);
                _spawning = false;
            }
        }

        private async UniTask SpawnWave(List<CountInfo> info)
        {
            foreach (var config in info)
            {
                for (int i = 0; i < config.SpawnCount; i++)
                {
                    EnemyAgent agent = EntityPoolManager
                        .Release(DataKit.GetPrefab(GlobalConst.PrefabName.AGENT_ENEMY), Path[0])
                        .GetComponent<EnemyAgent>();

                    if (!m_show.ContainsKey(config.EnemyID))
                    {
                        m_show[config.EnemyID] = false;
                    }

                    if (!SaveKit.Get(config.EnemyID) && !m_show[config.EnemyID])
                    {
                        m_show[config.EnemyID] = true;
                        UIKit.GetUI<GamePanel>().SetEnenyIcon(DataKit.GetEnemyJson(config.EnemyID).GetInstance());
                    }

                    agent.OnSpawn(DataKit.GetEnemyJson(config.EnemyID).GetInstance());

                    m_agentSystem.SpawnEntity(agent);

                    await UniTask.Delay(500);
                }
            }
        }

        public void SpawnEnemy()
        {
            m_timer = 0f;
            UIKit.GetUI<GamePanel>().UpdateSpawnPointTime((int)m_timer);
        }

        public void Hurt()
        {
            m_lift--;
            EventKit.GlobalEvent.Trigger(Core.EGlobalEvent.OnHurt, m_lift);
            if (m_lift <= 0)
            {
                EventKit.GameState.Switch(EGameState.Defeat);
            }
        }
    }
}
