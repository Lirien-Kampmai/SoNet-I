using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private SpaseShip m_DefaultSpaseShip;

        [SerializeField] private GameObject m_EpisodeSelect;

        [SerializeField] private GameObject m_ShipSelect;

        [SerializeField] private PlayerStatistic m_playerStatistic;

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultSpaseShip;

        }

        public void OnButtonStartNew()
        {
            m_EpisodeSelect.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnSelectShip()
        {
            m_ShipSelect.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnButtonExit()
        {
            Application.Quit();
        }
    }
}


