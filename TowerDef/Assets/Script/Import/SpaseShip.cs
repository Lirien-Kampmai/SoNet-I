using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace TowerDef
{
    /// <summary>
    /// Script responsible for the behavior of the ship, its shooting and indicators
    /// </summary>
    [RequireComponent (typeof (Rigidbody2D))]
    public class SpaseShip : Destructible
    {
        #region Properties
        // mass rigidbody
        [Header("Space Ship")]
        [SerializeField] private float m_Mass;

        // forward thrust
        [SerializeField] private float m_Thrust;

        // torque force
        [SerializeField] private float m_Mobility;

        // max linear speed
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;
        public float m_CurrentLinearVelocity;

        // max rotation speed. degrees per second
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        [SerializeField] private Turret[] m_Turrets;

        [SerializeField] private float m_BoostDuration;

        private float m_Timer;

        private Rigidbody2D m_Rigidbody2D;

        #endregion

        #region API
        // linear thrust control -1.0 to 1.0
        public float ThrustControl { get; set; }

        // rotation control -1.0 to 1.0
        public float TorqueControl { get; set; }
        #endregion

        #region Event
        protected override void Start()
        {
            base.Start();

            // caching component 
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Rigidbody2D.mass = m_Mass;
            m_Rigidbody2D.inertia = 1;

            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigitbody();
            UpdateEnergy();
            if (m_Timer < m_BoostDuration)
                m_Timer += Time.fixedDeltaTime;
            else
            {
                m_CurrentLinearVelocity = m_MaxLinearVelocity;
                AddIndestructible(false);
                m_Timer = 0;
            }
        }

        private void UpdateRigitbody()
        {
            m_Rigidbody2D.AddForce(m_Thrust * ThrustControl * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody2D.AddForce(-m_Rigidbody2D.velocity * (m_Thrust / m_CurrentLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody2D.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigidbody2D.AddTorque(-m_Rigidbody2D.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        #endregion

        // method responsible for firing from an array of turrets
        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode) m_Turrets[i].Fire();
            }
        }

        #region ammo and energy value
        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyRegenPerSec;
        #endregion

        private float m_CurrentEnergy;
        private int m_CurrentAmmo;

        // method add energy
        public void AddEnergy(int energy)
        {
            m_CurrentEnergy = Mathf.Clamp(m_CurrentEnergy + energy, 0, m_MaxEnergy);
        }

        // method add ammo
        public void AddAmmo(int ammo)
        {
            m_CurrentAmmo = Mathf.Clamp(m_CurrentAmmo + ammo, 0, m_MaxAmmo);
        }

        public void AddSpeed(float velocity)
        {
            m_CurrentLinearVelocity = Mathf.Clamp(m_CurrentLinearVelocity + velocity, 0, float.MaxValue);
        }

        // initialization at start to the maximum ammo, energy and thrust value
        private void InitOffensive()
        {
            m_CurrentEnergy = m_MaxEnergy;
            m_CurrentAmmo = m_MaxAmmo;
            m_CurrentLinearVelocity = m_MaxLinearVelocity;
        }

        // regeneration energy
        private void UpdateEnergy()
        {
            m_CurrentEnergy += (float) m_EnergyRegenPerSec * Time.fixedDeltaTime;
            m_CurrentEnergy = Mathf.Clamp(m_CurrentEnergy, 0, m_MaxEnergy);
        }

        // consumes energy
        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (m_CurrentEnergy >= count)
            {
                m_CurrentEnergy -= count;
                return true;
            }
            return false;
        }

        // consumes ammo
        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if (m_CurrentAmmo >= count)
            {
                m_CurrentAmmo -= count;
                return true;
            }
            return false;
        }

        // add a turret by working on an array of turrets
        public void AssightWeapon(TurretProperties properties)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssightLoadout(properties);
            }
        }

        public void Use(CreateEnemyAsset assetEnemy)
        {
            m_MaxLinearVelocity = assetEnemy.movespeed;
            base.UseDestr(assetEnemy);
        }
    }
}

