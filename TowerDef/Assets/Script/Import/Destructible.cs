using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDef
{
    /// <summary>
    /// The script is responsible for the ability to deal damage.
    /// The scripts is not attached to entities, but interacts with other scripts.
    /// </summary>
    public class Destructible : Entity
    {
        #region Destructible
        // ignore damage
        [SerializeField] private bool m_Indestructible;
        // start hitpoint
        [SerializeField] private int m_StartHitPoints;
        // current hitpoint
        private int m_CurrentHitPoints;

        [SerializeField] private float m_DeBoostDuration;

        [SerializeField] private GameObject m_ExplosionPrefab;

        public bool IsIndestructible => m_Indestructible;
        public int HitPoints => m_CurrentHitPoints;

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_StartHitPoints;
        }

        // set damage to object
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
            {
                Kill();
            }
        }

        public void AddIndestructible(bool e)
        {
            m_Indestructible = e;
        }

        // redefining the event when the object is destroyed
        protected virtual void Kill()
        {
            if (m_ExplosionPrefab != null) Instantiate(m_ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        // лист со всеми уничтожаемыми обьектами
        private static HashSet<Destructible> m_AllDestructible;
        public static IReadOnlyCollection<Destructible> AllDestructible => m_AllDestructible;

        protected virtual void OnEnable()
        {
            if (m_AllDestructible == null)
                m_AllDestructible = new HashSet<Destructible>();

            m_AllDestructible.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructible.Remove(this);
        }
        #endregion

        #region TeamId
        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;
        #endregion

        #region Score
        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;
        #endregion

        protected void UseDestr (CreateEnemyAsset assetEnemy)
        {
            m_StartHitPoints = assetEnemy.hitpoint;
            m_ScoreValue = assetEnemy.score;
        }
    }

}


