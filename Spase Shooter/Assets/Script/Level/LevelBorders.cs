using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Singlton script responsible for determining the boundaries of the level.
    /// The script is attached to the borders entity.
    /// </summary>
    public class LevelBorders : SingletonBase<LevelBorders>
    {
       
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        public enum Mode
        {
            Limit,
            Teleport,
            Death,
        }

        [SerializeField] private Mode m_LimitMode;
        public Mode LimitMode => m_LimitMode;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
#endif
    }

}



