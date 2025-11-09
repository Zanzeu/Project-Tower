using Tower.Runtime.Common;
using Tower.Runtime.GameSystem;
using Tower.Runtime.ToolKit;
using UnityEngine;

namespace Tower.Runtime.Gameplay
{
    public class BuildSystem : SystemBase
    {
        private AgentSystem _agentSystem;

        public BuildSystem(AgentSystem agentSystem)
        {
            _agentSystem = agentSystem;
        }

        public void BuildTower(string towerID, Vector2 vector)
        {
            TowerAgent entity = EntityPoolManager.Release(DataKit.GetPrefab("TowerAgent"), vector)
                .GetComponent<TowerAgent>();

            entity.OnSpawn(DataKit.GetTowerJson(towerID).GetInstance());
            _agentSystem.SpawnEntity(entity);
        }
    }
}
