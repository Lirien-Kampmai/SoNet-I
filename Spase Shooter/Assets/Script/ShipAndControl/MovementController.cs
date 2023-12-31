using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script responsible for motion control.
    /// </summary>
    public class MovementController : MonoBehaviour
    {
        // list of input method
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        // target control
        [SerializeField] private SpaseShip m_TargetShip;
        public void SetTargetShip(SpaseShip ship) => m_TargetShip = ship;
           
        // target Jjoistick
        [SerializeField] private VirtualJoistick m_MobileJoistick;
        // link to control method
        [SerializeField] private ControlMode m_ControllMode;

        // buttom fire primary and srcondary
        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;

        // control method start setting
        private void Start()
        {
            if (m_ControllMode == ControlMode.Keyboard)
            {
                m_MobileJoistick.gameObject.     SetActive(false);
                m_MobileFirePrimary.gameObject.  SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {
                m_MobileJoistick.gameObject.     SetActive(true);
                m_MobileFirePrimary.gameObject.  SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            // null check
            if (m_TargetShip == null) return;

            // call controll ship
            if (m_ControllMode == ControlMode.Keyboard) ControllKeyboard();
            if (m_ControllMode == ControlMode.Mobile  ) ControllMobile();
        }

        // joistick
        private void ControllMobile()
        {
            // direction
            var dir = m_MobileJoistick.Value;

            // set point value 
            m_TargetShip.ThrustControl =  dir.y;
            m_TargetShip.TorqueControl = -dir.x;

            if (m_MobileFirePrimary.IsHold   == true) m_TargetShip.Fire(TurretMode.Primary);
            if (m_MobileFireSecondary.IsHold == true) m_TargetShip.Fire(TurretMode.Secondary);
        }

        // keyboard
        private void ControllKeyboard()
        {
            // local variables
            float thrust = 0;
            float torque = 0;

            // set value variables
            if (Input.GetKey(KeyCode.W)) thrust =  1.0f;
            if (Input.GetKey(KeyCode.S)) thrust = -1.0f;
            if (Input.GetKey(KeyCode.A)) torque =  1.0f;
            if (Input.GetKey(KeyCode.D)) torque = -1.0f;

            // set buttom fire
            if (Input.GetKeyUp(KeyCode.Space)) m_TargetShip.Fire(TurretMode.Primary);
            if (Input.GetKeyUp(KeyCode.F    )) m_TargetShip.Fire(TurretMode.Secondary);

            // set value global variables
            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
    }
}

