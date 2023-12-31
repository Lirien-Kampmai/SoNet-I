using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDef
{

    public class InvokeNextWave : MonoBehaviour
    {
        [SerializeField] private Text m_BonusAmount;

        private EnemyWaveManager m_Manager;
        private float m_TimeToNewWave;

        private void Start()
        {
            m_Manager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                m_TimeToNewWave = time;
            };
        }

        public void CallWave()
        {
            m_Manager.ForceNextWave();
        }

        private void Update()
        {

            var bonus = (int)m_TimeToNewWave;
            if (bonus <= 0) bonus = 0;

            m_BonusAmount.text = bonus.ToString();
            m_TimeToNewWave -= Time.deltaTime;
        }


    }
}