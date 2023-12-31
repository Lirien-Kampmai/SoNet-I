using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    public class TimeLevelCondition : MonoBehaviour, LevelCondition
    {
        [SerializeField] private float m_TimeLimit;

        private void Start()
        {
            m_TimeLimit += Time.time;
        }

        public bool IsComplited => Time.time > m_TimeLimit;
    }
}