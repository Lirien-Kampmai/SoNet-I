using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{

    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Path[] m_Paths;

        [SerializeField] private EnemyWave m_CurrentWave;

        [SerializeField] private EnemySettings m_EnemyPrefab;

        private int m_ActiveEnemyCount = 0;

        public event Action OnAllWavedead;

        private void Start()
        {
            m_CurrentWave.Prepare(SpawnEnemy);
        }

        private void SpawnEnemy ()
        {
            foreach((CreateEnemyAsset asset, int count, int pathIndex) in m_CurrentWave.Enumsquad())
            {
                if (pathIndex < m_Paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var enemy = Instantiate(m_EnemyPrefab, m_Paths[pathIndex].StartSpawnArea.GetRandonInsideZone(), Quaternion.identity);

                        enemy.OnEnd += RecordEnemyDead;
                        enemy.UseEnemy(asset);
                        enemy.GetComponent<TDPatrolController>().SetPath(m_Paths[pathIndex]);

                        m_ActiveEnemyCount += 1;

                    }
                }
            }
            m_CurrentWave.PrepareNext(SpawnEnemy);
        }

        public void ForceNextWave()
        {
            if (m_CurrentWave)
            {
                (Player.Instance as TDPlayer).ChangeGold((int)m_CurrentWave.GetRemainingTime());
                SpawnEnemy();
            }
            else
            {
                if(m_ActiveEnemyCount == 0) OnAllWavedead?.Invoke();
            }
        }

        private void RecordEnemyDead()
        {
            if(--m_ActiveEnemyCount == 0)
            {
                ForceNextWave();
            }
        }
    }

}