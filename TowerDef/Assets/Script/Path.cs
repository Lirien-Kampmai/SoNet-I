using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private DrawCircleArea m_StartSpawnArea;
        public DrawCircleArea StartSpawnArea { get { return m_StartSpawnArea; } }

        //массив точек патрулирования
        [SerializeField] private AIPointerPatrol[] m_AIPointerPatrol;

        // длинна возвращает колличество точек
        public int LengthPointPatrol { get => m_AIPointerPatrol.Length; }

        // возвращаем точку по индексу
        public AIPointerPatrol this[int i] { get => m_AIPointerPatrol[i]; }

        private void Start()
        {
            m_AIPointerPatrol = GetComponentsInChildren<AIPointerPatrol>();
        }

        #if UNITY_EDITOR
        private static readonly Color GizmosColor = Color.magenta;
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmosColor;
            foreach (var point in m_AIPointerPatrol)
            {
                Gizmos.DrawSphere(point.transform.position, point.Radius);
                //Gizmos.DrawLine(point.Vector3AIPointPatrol, point.Vector3AIPointPatrol);
            }
        }

        private void OnValidate()
        {
            m_AIPointerPatrol = GetComponentsInChildren<AIPointerPatrol>();
        }
        #endif


    }
}


