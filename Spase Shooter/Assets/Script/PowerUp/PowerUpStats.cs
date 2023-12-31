using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script is responsible for add ammo and energy when lifting powerup.
    /// The script is attached to the powerup entity.
    /// </summary>
    public class PowerUpStats : PowerUp
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            AddSpeed,
            AddIncreasedVulnerability,
        }

        [SerializeField] private EffectType m_EffectType;
        [SerializeField] private float m_Value;

        protected override void OnPicketUp(SpaseShip ship)
        {
            if (m_EffectType == EffectType.AddAmmo)
                ship.AddAmmo  ((int)m_Value);

            if (m_EffectType == EffectType.AddEnergy)
                ship.AddEnergy((int)m_Value);

            if (m_EffectType == EffectType.AddSpeed)
                ship.AddSpeed(m_Value);

            if (m_EffectType == EffectType.AddIncreasedVulnerability)
                ship.AddIndestructible(true);
                
        }
    }
}


