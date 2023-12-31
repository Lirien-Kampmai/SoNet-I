using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for creating new debris object.
    /// The script is attached to the spawn-entity
    /// </summary>
    [RequireComponent(typeof(DrawCircleArea))]
    public class SpawnerDebris : MonoBehaviour
    {
        // array prefab of the created debtis object
        [SerializeField] private Destructible[] m_DebtisPrefab;

        // zone of spawn
        private DrawCircleArea m_Area;

        // number of objects debris created
        [SerializeField] private int m_NumberOfCreatedDebris;

        // debris flight speed
        private float m_RandomSpeed;

        private void Start()
        {
            for(int i = 0; i < m_NumberOfCreatedDebris; i++)
            {
                SpawnDebris();
            }
        }

        private void OnValidate()
        {
            m_Area = GetComponent<DrawCircleArea>();
        }

        private void SpawnDebris()
        {
            // picking a random value in an array 
            int index = Random.Range(0, m_DebtisPrefab.Length);

            // command to spawn selected random value in an array
            GameObject debris = Instantiate(m_DebtisPrefab[index].gameObject);

            debris.transform.position = m_Area.GetRandonInsideZone();

            // when trash is destroyed, we subscribe to an event that the trash spawner restart
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            // we get the rigidbody2D and set the direction of movement and speed
            Rigidbody2D rigidbody2D = debris.GetComponent<Rigidbody2D>();
            m_RandomSpeed = Random.Range(0, 3);
            if (rigidbody2D != null && m_RandomSpeed > 0)
            {
                rigidbody2D.velocity = UnityEngine.Random.insideUnitCircle * m_RandomSpeed;
            }
        }

        private void OnDebrisDead()
        {
            SpawnDebris();
        }
    }
}


