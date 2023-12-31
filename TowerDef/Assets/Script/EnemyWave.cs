using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    public class EnemyWave : MonoBehaviour
    {
        public static Action<float> OnWavePrepare;

        [Serializable]
        private class Squad
        {
            public CreateEnemyAsset m_Asset;
            public int m_Count;
        }
        [Serializable]
        private class PathGroup
        {
            public Squad[] m_Squads;
        }

        [SerializeField] private float m_PrepareTime = 10f;
        public float GetRemainingTime () { return m_PrepareTime - Time.time; }

        [SerializeField] private PathGroup[] m_Groups;

        private void Awake()
        {
            enabled = false;
        }

        public IEnumerable<(CreateEnemyAsset asset, int count, int pathIndex)> Enumsquad()
        {
            for(int i = 0; i < m_Groups.Length; i++)
            {
                foreach(var squad in m_Groups[i].m_Squads)
                {
                    yield return (squad.m_Asset, squad.m_Count, i);
                }
            }
        }

        private event Action OnWaveReady;
        internal void Prepare(Action spawnEnemy)
        {
            OnWavePrepare?.Invoke(m_PrepareTime);
            m_PrepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemy;
        }

        [SerializeField] private EnemyWave m_Next;
        public EnemyWave PrepareNext(Action spawnEnemy)
        {
            OnWaveReady -= spawnEnemy;
            if(m_Next) m_Next.Prepare(spawnEnemy);
            return m_Next;

        }

        private void Update()
        {
            if (Time.time >= m_PrepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }
    }
}