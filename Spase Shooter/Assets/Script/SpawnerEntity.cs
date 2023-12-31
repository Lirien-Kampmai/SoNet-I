using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for creating new object.
    /// The script is attached to the spawn-entity
    /// </summary>

    [RequireComponent(typeof(DrawCircleArea))]
    public class SpawnerEntity : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop,
        }

        // array prefab of the created object
        [SerializeField] private AIController [] m_EntityPrefab;

        // zone of spawn
        [SerializeField] private DrawCircleArea m_Area;

        // link to spawn mode
        [SerializeField] private SpawnMode m_SpawnMode;

        // number of objects created
        [SerializeField] private int m_NumberOfCreatedObject;

        // respawn time
        [SerializeField] private float m_RespawnTime;

        private AIPointerPatrol[] m_PointPatroll;
        public AIPointerPatrol[] PointPatroll => m_PointPatroll;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start) SpawnEntity();
            m_PointPatroll = GetComponentsInChildren<AIPointerPatrol>();
            m_Timer = m_RespawnTime;
        }

        private void OnValidate()
        {
            m_Area = GetComponent<DrawCircleArea>();
        }

        private void Update()
        {
            if(m_Timer > 0) m_Timer -= Time.deltaTime;

            if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
            {
                SpawnEntity();
                m_Timer = m_RespawnTime;
            }
        }

        private void SpawnEntity()
        {


            for (int i = 0; i < m_NumberOfCreatedObject; i++)
            {
                // picking a random value in an array 
                int index = Random.Range(0, m_EntityPrefab.Length);

                // command to spawn selected random value in an array
                GameObject entity = Instantiate(m_EntityPrefab[index].gameObject, transform);

                // spawn in a random zone
                entity.transform.position = m_Area.GetRandonInsideZone();

            }
        }
    }
}


