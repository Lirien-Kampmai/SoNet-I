using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDef
{
    public class TowerController : MonoBehaviour
    {
        [Range(0, 100)]
        [SerializeField] private float m_RadiusOfFire;

        private Turret[] m_Turrets;
        private Destructible m_Target;

        private void Start()
        {
            // installing the turret component from child entities
            m_Turrets = GetComponentsInChildren<Turret>();
        }

        private void Update()
        {
            TowerLogic();
        }

        private void TowerLogic()
        {
            if (m_Target)
            {
                FireToTarget();
            }
            else
            {
                SeachTarget();
            }
        }

        private void FireToTarget()
        {
            // calculate the target vector
            Vector2 targetVector2 = m_Target.transform.position - transform.position;

            // the target is within the firing radius - we shoot. else set target null
            if (targetVector2.magnitude <= m_RadiusOfFire)
            {
                // make all turrets from the array shoot
                foreach (var m_Turrets in m_Turrets)
                {
                    // turn the turrets to our targets
                    m_Turrets.transform.up = targetVector2;
                    m_Turrets.Fire();
                }
            }
            else
            {
                m_Target = null;
            }
        }

        private void SeachTarget()
        {
            // checking if the collider has entered the fire zone
            var collider = Physics2D.OverlapCircle(transform.position, m_RadiusOfFire);
            if (collider)
            {
                // set target
                m_Target = collider.transform.root.GetComponent<Destructible>();
            }
        }

        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Color GizmosColor = Color.red;
            Handles.color = GizmosColor;
            Handles.DrawWireDisc(transform.position, transform.forward, m_RadiusOfFire);
        }
        #endif
    }
}


