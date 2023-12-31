using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    // fire mode weapon
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    /// <summary>
    /// Script is responsible for creating scripted asset for the turrets.
    /// </summary>
    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        // select mod weapon
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        // link to prefab roket
        [SerializeField] private Projectile m_ProjectilePrefab;
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        // rate of fire turret
        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;

        // energy per sec for fire
        [SerializeField] private int m_EnegyUseWeapon;
        public int EnegyUseWeapon => m_EnegyUseWeapon;

        // ammo per sec for fire
        [SerializeField] private int m_AmmoUseWeapon;
        public int AmmoUseWeapon => m_AmmoUseWeapon;

        // sound fire
        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}


