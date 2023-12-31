using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script, responsible for the strength and radius of gravity of an entity.
    /// The script is attached to the entity, which should have its own gravity.
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        // strength gravity
        [SerializeField] private float m_Force;
        // radius gravity
        [SerializeField] private float m_Radius;

        private void OnTriggerStay2D(Collider2D collision)
        { 
            // check for null
            if (collision.attachedRigidbody == null) return;

            // distance from collision to transform
            Vector2 dir = transform.position - collision.transform.position;

            // set distance
            float dist = dir.magnitude;

            if (dist < m_Radius)
            {
                // set the force of attraction, the closer the stronger it is (dist / m_Radius)
                Vector2 force = dir.normalized * m_Force * (dist / m_Radius);

                // applying gravity to a body
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }

        // it is better not to include in the build because of possible errors
#if UNITY_EDITOR
        private void OnValidate()
        {
            // set the radius of the collider equal to the radius of gravity
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
#endif
    }
}


