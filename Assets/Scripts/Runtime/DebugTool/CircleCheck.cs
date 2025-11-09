using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Runtime.DebugTool
{
    public class CircleCheck : MonoBehaviour
    {
        [SerializeField, LabelText("°ë¾¶")] private float radius = 1f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green; 
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
