using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Lifetime : MonoBehaviour
    {
        [SerializeField] private float m_LifeTime;
        private float m_Timer;

        void Update()
        {
            if (m_Timer < m_LifeTime)
                m_Timer += Time.deltaTime;
            else
                Destroy(gameObject);
        }
    }
}


