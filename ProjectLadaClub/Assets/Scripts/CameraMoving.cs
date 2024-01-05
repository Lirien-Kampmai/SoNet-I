using ProjectCar.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace CamViev
    {
        public class CameraMoving : MonoBehaviour
        {
            [SerializeField] private Transform m_Target;
            [SerializeField] private Rigidbody m_Rigidbody;

            [SerializeField] private float m_VievHeigth;
            [SerializeField] private float m_Heigth;
            [SerializeField] private float m_Distance;

            [SerializeField] private float m_RotationDamping;
            [SerializeField] private float m_HeigthDamping;
            [SerializeField] private float m_SpeedHold;

            private void FixedUpdate()
            {

                Vector3 velocity = m_Rigidbody.velocity;
                Vector3 targetRotation = m_Target.eulerAngles;
                targetRotation.y = velocity.y;

                if(velocity.magnitude > m_SpeedHold)
                {
                    targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
                }

                float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, m_RotationDamping * Time.fixedDeltaTime);
                float currentHeigth = Mathf.Lerp(transform.position.y, m_Target.position.y + m_Heigth, m_HeigthDamping * Time.fixedDeltaTime);
               
                //position
                Vector3 posOffset = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * m_Distance;
                transform.position = m_Target.position - posOffset;
                transform.position = new Vector3(transform.position.x, currentHeigth, transform.position.z);


                //rotarion
                transform.LookAt(m_Target.position + new Vector3(0, m_VievHeigth, 0));
            }
        }

        /*
        public class CameraCorrector : MonoBehaviour
        {
            [SerializeField] private CarInfoModel m_Car;
            [SerializeField] private new Camera m_Camera;

            [SerializeField] private float m_MinFieldOfView;
            [SerializeField] private float m_MaxFieldOfView;

            private float m_defaultPOV;

            private void Start()
            {
                m_Camera.fieldOfView = m_defaultPOV;
            }

            private void Update()
            {
                m_Camera.fieldOfView = Mathf.Lerp(m_MinFieldOfView, m_MaxFieldOfView, m_Car.NormalizeLinearVelocity);
            }


        }

        public class CameraShaker : MonoBehaviour
        {
            [SerializeField] private CarInfoModel m_Car;
            [SerializeField] [Range(0.0f, 1.0f)] private float m_NormalizeSpeedShake;

            [SerializeField] private float m_ShakeAmount;

            private void Update()
            {
                if(m_Car.NormalizeLinearVelocity >= m_NormalizeSpeedShake)
                    transform.localPosition += Random.insideUnitSphere * m_ShakeAmount * Time.deltaTime;
            }
        }
        */
        
    }
}


