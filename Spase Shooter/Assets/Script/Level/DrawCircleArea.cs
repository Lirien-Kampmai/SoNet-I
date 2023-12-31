using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// Drawing a visual circle.
    /// Script is bound to any object to whitch you want to draw the circle.
    /// </summary>
    public class DrawCircleArea : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        public Vector2 GetRandonInsideZone()
        {
            return (Vector2) transform.position + (Vector2) UnityEngine.Random.insideUnitSphere * m_Radius;
        }


#if UNITY_EDITOR
        // set color
        private static Color GizmosColor = Color.green;
        private void OnDrawGizmos()
        {
            Handles.color = GizmosColor;
            // draw gizmos to color
            Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
#endif


    }
}


