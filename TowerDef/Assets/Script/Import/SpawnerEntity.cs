using UnityEngine;

namespace TowerDef
{
    /// <summary>
    /// Script responsible for creating new object.
    /// The script is attached to the spawn-entity
    /// </summary>

    [RequireComponent(typeof(DrawCircleArea))]
    public class SpawnerEntity : SpawnerAbstractEntity
    {
        [SerializeField] private GameObject[] m_EntityPrefab;

        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(m_EntityPrefab[Random.Range(0, m_EntityPrefab.Length)]);
        }
    }
}


