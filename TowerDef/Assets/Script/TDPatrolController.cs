using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDef
{
    public class TDPatrolController : AIController
    {
        [SerializeField] private UnityEvent m_CameToTheLastPoint;
        private Path m_Path;
        private int m_PathIndex;

        private EnemySettings m_EnemySettings;

        private void Awake()
        {
            m_EnemySettings = GetComponent<EnemySettings>();
        }

        internal void SetPath(Path newPath)
        {
            m_Path = newPath;
            m_PathIndex = 0;

            SetPatrolBehaviour(newPath[m_PathIndex]);
        }

        protected override void GetNewPoint()
        {
            if(m_Path.LengthPointPatrol > ++m_PathIndex)
            {
                SetPatrolBehaviour(m_Path[m_PathIndex]);
            }
            else
            {
                m_EnemySettings.CameToTheLastPoint();
                Destroy(gameObject);
            }
        }
    }
}


