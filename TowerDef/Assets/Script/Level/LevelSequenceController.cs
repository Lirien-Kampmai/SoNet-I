using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDef
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneName = "level_map";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public static SpaseShip PlayerShip;

        public bool LastLevelResult { get; private set; }

        public PlayerStatistic LevelStatistics { get; private set; }

        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            LevelStatistics = new PlayerStatistic();
            LevelStatistics.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool success)
        {
            ResultPanelController.Instance.ShowResult(success);
        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

            CurrentLevel++;

            if(CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneName);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        private void CalculateLevelStatistic()
        {
            LevelStatistics.Score = Player.Instance.Score;
            LevelStatistics.Numkill = Player.Instance.NumKill;
            LevelStatistics.Gametime = (int)LevelController.Instance.LevelTime;
        }
    }
}


