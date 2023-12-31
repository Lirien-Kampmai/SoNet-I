using System.Collections;
using System.Collections.Generic;
using SpaceShooter;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, LevelCondition
    {
        [SerializeField] private int m_Score;

        // переменная для установки достижения
        private bool m_Riched;

        bool LevelCondition.IsComplited
        {
            get
            {
                if(Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if(Player.Instance.Score >= m_Score)
                    {
                        m_Riched = true;
                    }
                }
                return m_Riched;
            }
        }
    }
}