using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Numkill;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Gametime;
        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonNextText;
        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResult(PlayerStatistic levelStatistics, bool success)
        {
            gameObject.SetActive(true);

            m_Success = success;

            m_Result.text = success ? "Win" : "Lose";
            m_ButtonNextText.text = success ? "Main menu" : "Restart";

            m_Numkill.text = "Kills: " + levelStatistics.Numkill.ToString();
            m_Score.text = "Score: " + levelStatistics.Score.ToString();
            m_Gametime.text = "Time: " + levelStatistics.Gametime.ToString();

            Time.timeScale = 0;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (m_Success)
                LevelSequenceController.Instance.AdvanceLevel();
            else
                LevelSequenceController.Instance.RestartLevel();
        }
    }
}


