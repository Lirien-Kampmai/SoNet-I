using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDef
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        
        [SerializeField] private Text m_Numkill;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Gametime;
        

        [SerializeField] private GameObject m_ResultCanvas;
        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonNextText;

        [Header(nameof(Text))]
        [SerializeField] private string m_WinText;
        [SerializeField] private string m_LoseText;
        [SerializeField] private string m_WinButtonText;
        [SerializeField] private string m_LoseButtonText;

        private bool m_Success;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResult(bool success)
        {
            gameObject.SetActive(true);
            m_ResultCanvas.SetActive(true);

            m_Success = success;

            m_Result.text = success ? m_WinText : m_LoseText;
            m_ButtonNextText.text = success ? m_WinButtonText : m_LoseButtonText;

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


