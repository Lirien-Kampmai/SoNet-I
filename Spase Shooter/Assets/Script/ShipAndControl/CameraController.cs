using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for controlling the camera and linking it to the player.
    /// The script is attached to the controller entity.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        // target camera
        [SerializeField] private Camera m_Camera;
        // target entity
        [SerializeField] private Transform m_Target;

        // linear interpolation
        [SerializeField] private float m_InterpolationLinear;
        // angular interpolation
        [SerializeField] private float m_InterpolationAngular;
        // set position camera by Z
        [SerializeField] private float m_CameraZOffset;
        // camera deviation from tracking target
        [SerializeField] private float m_ForwardOffset;

        private void FixedUpdate()
        {
            // check for null
            if (m_Camera == null || m_Target == null) return;

            // start position camera
            Vector2 camPositipn = m_Camera.transform.position;

            // finish position camera
            Vector2 targetPosition = m_Target.position + (m_Target.transform.up * m_ForwardOffset);

            // calculate new position camera (interpolation)
            Vector2 newCamPosition = Vector2.Lerp(camPositipn, targetPosition, m_InterpolationLinear * Time.deltaTime);

            // move new position camera
            m_Camera.transform.position = new Vector3(newCamPosition.x, newCamPosition.y, m_CameraZOffset);

            // camera rotation
            if (m_InterpolationAngular > 0)
                m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation,
                                            m_Target.rotation, m_InterpolationAngular * Time.deltaTime);
        }

        // sets the target when the entity changes. Used in other scripts.
        public void SetTarget(Transform newTarget)
        {
            m_Target = newTarget;
        }
    }
}



