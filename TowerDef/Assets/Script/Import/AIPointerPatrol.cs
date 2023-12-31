using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    public class AIPointerPatrol : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;
        //public Vector3 Vector3AIPointPatrol => gameObject.transform.position;


        #if UNITY_EDITOR
        private static readonly Color GizmosColor = Color.magenta;
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmosColor;
            Gizmos.DrawSphere(transform.position, m_Radius);
        }
        #endif

        // лист со всеми обьектами
        private static HashSet<AIPointerPatrol> m_AllAIPointerPatrol;
        public static IReadOnlyCollection<AIPointerPatrol> AllAIPointerPatrol => m_AllAIPointerPatrol;
    }
}


