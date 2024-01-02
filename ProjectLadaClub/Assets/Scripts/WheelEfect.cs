using ProjectCar.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCar
{
    namespace SFX
    {
        public class WheelEfect : MonoBehaviour
        {
            [SerializeField] private WheelCollider[] m_WheelColliders;
            [SerializeField] private float m_ForwardSlipLimit;
            [SerializeField] private float m_SidewaySlipLimit;
            [SerializeField] private GameObject m_SkidPrefab;

            private WheelHit wheelHit;
            private Transform[] m_SkidTrail;
            private CarInfoModel carInfoModel;

            private void Start()
            {
                carInfoModel = GetComponentInParent<CarInfoModel>();
                m_WheelColliders = carInfoModel.GetComponentsInChildren<WheelCollider>();
                m_SkidTrail = new Transform[m_WheelColliders.Length];
            }

            private void Update()
            {
                for (int i = 0; i < m_WheelColliders.Length; i++)
                {
                    m_WheelColliders[i].GetGroundHit(out wheelHit);

                    if (m_WheelColliders[i].isGrounded == true)
                    {
                        if(wheelHit.forwardSlip > m_ForwardSlipLimit || wheelHit.sidewaysSlip > m_SidewaySlipLimit)
                        {
                            if (m_SkidTrail[i] == null)
                            {
                                m_SkidTrail[i] = Instantiate(m_SkidPrefab).transform;
                            }

                            if (m_SkidTrail[i] != null)
                            {
                                m_SkidTrail[i].position = m_WheelColliders[i].transform.position - wheelHit.normal * m_WheelColliders[i].radius;
                                m_SkidTrail[i].forward = -wheelHit.normal;
                            }
                            continue;
                        }
                    }
                    else
                    {
                        m_SkidTrail[i] = null;
                    }
                }
            }
        }
    }
}

