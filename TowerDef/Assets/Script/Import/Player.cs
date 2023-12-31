using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDef
{
    /// <summary>
    /// Singleton script responsible for the player entity.
    /// </summary>
    public class Player : SingletonBase<Player>
    {
        #region Player
        // amount life
        [SerializeField] private int m_AmountLife;
        public int AmountLife { get { return m_AmountLife; } }

        // публичный эвент, на который можно подписаться но который нелзя вызвать извне
        public event Action OnPlayerDead;

        // link to ship
        [SerializeField] private SpaseShip m_Ship;

        // prefab player ship
        [SerializeField] private GameObject m_PrefabSpaseShip;
        public SpaseShip ActiveShip => m_Ship;

        //[SerializeField] private CameraController m_CameraController;
        //[SerializeField] private MovementController m_MovementController;

        private Vector3 StartPosition;

        private void Start()
        {
            Respawn();
        }

        private void OnShipDeath()
        {
            m_AmountLife--;
            if (m_AmountLife > 0)
                Respawn();
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        private void Respawn()
        {
            if(LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);
                m_Ship = newPlayerShip.GetComponent<SpaseShip>();

                if(m_Ship)
                    m_Ship.EventOnDeath.AddListener(OnShipDeath); 
            }

        }

        protected void TakeDamage(int m_DamageToPlayer)
        {
            m_AmountLife -= m_DamageToPlayer;
            if (m_AmountLife <= 0)
            {
                m_AmountLife = 0;
                OnPlayerDead?.Invoke();
            }
                
        }

        public void GetGold(int m_DropGold)
        {
            (Player.Instance as TDPlayer).ChangeGold(m_DropGold);
        }
        #endregion
        #region Score
        // score
        public int Score { get; private set; }

        // number of kill
        public int NumKill { get; private set; }

        // method for adding point to number of kill
        public void AddKill()
        {
            NumKill++;
        }

        // method for adding point to score
        public void AddScore(int score)
        {
            Score += score;
        }
        #endregion
        #region
        protected override void Awake()
        {
            base.Awake();
            if (m_Ship != null)
                Destroy(m_Ship.gameObject);
        }
        #endregion
    }
}



