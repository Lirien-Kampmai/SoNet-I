using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDef
{
    public class SelectMapLevel : MonoBehaviour
    {
        private Episode m_Episode;
        [SerializeField] private Text m_TextResultBarPanel;

        public bool IsComplited { get { return gameObject.activeSelf && m_TextResultBarPanel.gameObject.activeSelf; } }

        public void LoadLevel()
        {
            if(m_Episode)
                LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
            m_Episode = episode;
            m_TextResultBarPanel.text = $"{score} / 3";
        }

    }
}