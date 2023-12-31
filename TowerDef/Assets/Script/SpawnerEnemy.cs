using UnityEngine;


namespace TowerDef
{
    /// <summary>
    /// Script responsible for creating new object.
    /// The script is attached to the spawn-entity
    /// </summary>

    [RequireComponent(typeof(DrawCircleArea))]
    public class SpawnerEnemy : SpawnerAbstractEntity
    {
        // базовый префаб врага
        [SerializeField] private EnemySettings m_EnemyPrefab;

        // скриптабл обьект с настройками врагов
        [SerializeField] private CreateEnemyAsset[] m_EnemySettings;

        // сущность, хранящая точки следования
        [SerializeField] private Path m_Path;

        protected override GameObject GenerateSpawnedEntity()
        {
            var enemy = Instantiate(m_EnemyPrefab);
            enemy.UseEnemy(m_EnemySettings[Random.Range(0, m_EnemySettings.Length)]);
            enemy.GetComponent<TDPatrolController>().SetPath(m_Path);
            return enemy.gameObject;
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            m_Path = GetComponentInChildren<Path>();
        }
        #endif
    }
}


