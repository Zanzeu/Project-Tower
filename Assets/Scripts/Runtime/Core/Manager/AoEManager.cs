using Tower.Runtime.Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.Core
{
    public class AoEManager : Singleton<AoEManager>
    {
        protected override bool IsPersistent => false;

        private readonly List<AoEBase> _activeAoEs = new();
        private readonly List<AoEBase> _waitingAdd = new();
        private bool _isUpdating;

        private void Update()
        {
            float dt = Time.deltaTime;
            _isUpdating = true;

            for (int i = _activeAoEs.Count - 1; i >= 0; i--)
            {
                var aoe = _activeAoEs[i];
                if (!aoe.IsActive)
                {
                    _activeAoEs.RemoveAt(i);
                    continue;
                }

                aoe.OnUpdate(dt);
            }

            _isUpdating = false;

            if (_waitingAdd.Count > 0)
            {
                _activeAoEs.AddRange(_waitingAdd);
                _waitingAdd.Clear();
            }
        }

        public void CreateAoE(AoEBase aoe)
        {
            if (_isUpdating)
            {
                _waitingAdd.Add(aoe);
            }
            else
            {
                _activeAoEs.Add(aoe);
            }

            aoe.OnStart();
        }

        public void DestroyAoE(AoEBase aoe)
        {
            aoe.OnStop();
            _activeAoEs.Remove(aoe);
            _waitingAdd.Remove(aoe);
        }

        public void ClearAllAoE()
        {
            foreach (var aoe in _activeAoEs)
            {
                aoe.OnStop();
            }

            _activeAoEs.Clear();
            _waitingAdd.Clear();
        }
    }
}