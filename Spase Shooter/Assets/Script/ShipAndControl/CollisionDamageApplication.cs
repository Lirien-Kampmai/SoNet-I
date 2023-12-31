using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for collision damage.
    /// The script is attached to the player entity.
    /// Works in conjunction with a script "Destructible".
    /// </summary>
    public class CollisionDamageApplication : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";

        // damade modifier based on speed
        [SerializeField] private float m_VelocityDamageModifier;
        // basic damage
        [SerializeField] private float m_DamageConstant;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == IgnoreTag) return;

            var destructible = transform.root.GetComponent<Destructible>();

            if (destructible != null)
            {
                destructible.ApplyDamage((int)m_DamageConstant + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
            }
        }
    }
}

