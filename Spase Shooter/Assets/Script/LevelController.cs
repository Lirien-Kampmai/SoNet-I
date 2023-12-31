using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace SpaceShooter
{
    public interface LevelCondition
    {
        bool IsComplited { get; }
    }

    public class LevelController : SingletonBase<LevelController>
    {
        // время за которое начисляются очки
        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;

        // эвент при завершении уровня
        [SerializeField] private UnityEvent m_EventLevelComplited;

        private LevelCondition[] m_Conditions;

        private bool m_IsLevelComplited;
        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        private void Start()
        {
            m_Conditions = GetComponentsInChildren<LevelCondition>();
        }

        private void Update()
        {
            if(!m_IsLevelComplited)
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelCondition();
            }
        }

        private void CheckLevelCondition()
        {
            if (m_Conditions == null || m_Conditions.Length == 0) return;

            int numCompleted = 0;

            foreach(var v in m_Conditions)
            {
                if (v.IsComplited) numCompleted++;
            }

            if(numCompleted == m_Conditions.Length)
            {
                m_IsLevelComplited = true;
                m_EventLevelComplited?.Invoke();
                LevelSequenceController.Instance.FinishCurrentLevel(true);
            }

        }
    }
}

