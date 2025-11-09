using Tower.Runtime.Gameplay;
using UnityEngine;

namespace Tower.Runtime.DebugTool
{
    [RequireComponent(typeof(LineRenderer))]
    public class AoEVisualizer : MonoBehaviour
    {
        private AoEBase _aoe;
        private LineRenderer _line;

        public void Init(AoEBase aoe)
        {
            _aoe = aoe;
            _line = GetComponent<LineRenderer>();
            _line.loop = true;
            _line.useWorldSpace = true;
            _line.positionCount = 60;
        }

        private void Update()
        {
            if (_aoe == null || !_aoe.IsActive)
            {
                gameObject.SetActive(false);
                return;
            }

            transform.position = _aoe.Position;
            DrawCircle(_aoe.Radius);
        }

        private void DrawCircle(float radius)
        {
            for (int i = 0; i < _line.positionCount; i++)
            {
                float angle = i * Mathf.PI * 2 / _line.positionCount;
                Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                _line.SetPosition(i, transform.position + pos);
            }
        }
    }
}