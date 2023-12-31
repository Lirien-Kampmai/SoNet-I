using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script sinchronize the position of the object with the position of the target.
    /// </summary>
    public class SyncTransform : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;

        private void FixedUpdate()
        {
            transform.position = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
        }
    }
}