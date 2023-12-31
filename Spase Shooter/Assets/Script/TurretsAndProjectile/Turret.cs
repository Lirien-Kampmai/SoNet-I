using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for firing turrets and changes weapons.
    /// The scripts is attached to turret-entities.
    /// Works in conjunction with a script "TurretProperties".
    /// </summary>
    public class Turret : MonoBehaviour
    {
        // selected turret mode
        [SerializeField] TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        // link to scriptableobject asset
        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTime;

        public bool CanFire => m_RefireTime <= 0;

        private SpaseShip m_Ship;

        private void Start()
        {
            // link to ship
            m_Ship = transform.root.GetComponent<SpaseShip>();
        }

        private void Update()
        {
           if (m_RefireTime > 0)  m_RefireTime -= Time.deltaTime;
        }

        public void Fire()
        {
            #region possible fire check
            if (m_Ship.DrawEnergy(m_TurretProperties.EnegyUseWeapon) == false) return;
            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUseWeapon)    == false) return;
            if (m_TurretProperties == null)                                    return;
            if (m_RefireTime > 0)                                              return;
            #endregion

            // create projectile
            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            //set position
            projectile.transform.position = transform.position;
            //set rotation
            projectile.transform.up = transform.up;
            // set parent
            projectile.SetParentShooter(m_Ship);
            // set rate of fire 
            m_RefireTime = m_TurretProperties.RateOfFire;

            // SFX

        }

        // method that changes weapons on pickup
        public void AssightLoadout(TurretProperties props)
        {
            if (m_Mode != props.Mode) return;
            m_RefireTime = 0;
            m_TurretProperties = props;
        }
    }
}


