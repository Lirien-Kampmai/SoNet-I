using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    [RequireComponent(typeof(DrawCircleArea))]
    public abstract class SpawnerAbstractEntity : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop,
        }


        //
        protected abstract GameObject GenerateSpawnedEntity();

        // zone of spawn
        [SerializeField] private DrawCircleArea m_Area;

        // link to spawn mode
        [SerializeField] private SpawnMode m_SpawnMode;

        // number of objects created
        [SerializeField] private int m_NumberOfCreatedObject;

        // respawn time
        [SerializeField] private float m_RespawnTime;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start) SpawnEntity();

            m_Timer = m_RespawnTime;
        }

        private void Update()
        {
            if (m_Timer > 0) m_Timer -= Time.deltaTime;

            if (m_SpawnMode == SpawnMode.Loop && m_Timer <= 0)
            {
                SpawnEntity();

                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntity()
        {
            for (int i = 0; i < m_NumberOfCreatedObject; i++)
            {
                var entity = GenerateSpawnedEntity();
                entity.transform.position = m_Area.GetRandonInsideZone();
            }
        }

        private void OnValidate()
        {
            m_Area = GetComponent<DrawCircleArea>();
        }


    }
}


