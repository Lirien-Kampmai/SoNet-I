using ProjectCar.Car;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCar
{
    namespace UI
    {
        public class SpeedIndicator : MonoBehaviour
        {
            [SerializeField] private CarInfoModel m_Car;
            [SerializeField] private Text m_Text;

            private void Start()
            {
                m_Car = GetComponentInParent<CarInfoModel>();
            }

            private void Update()
            {
                SpeedText();
            }

            private void SpeedText()
            {
                m_Text.text = m_Car.m_linearVelocity.ToString("F0");
            }

        }
    }
}

