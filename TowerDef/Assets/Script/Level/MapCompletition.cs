using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    public class MapCompletition : SingletonBase<MapCompletition>
    {
        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }

        [SerializeField] private EpisodeScore[] m_CompletitionData;

        [SerializeField] private int m_TotalScore;
        public int TotalScore { get { return m_TotalScore; } }



        public const string filename = "CompletitionData.dat";

        public bool TryIndexCompletitionData(int id, out Episode episode, out int score)
        {
            if(id >= 0 && id < m_CompletitionData.Length)
            {
                episode = m_CompletitionData[id].episode;
                score = m_CompletitionData[id].score;
                return true;
            }
            else
            {
                episode = null;
                score = 0;
                return false;
            }
        }

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref m_CompletitionData);

            foreach(var episodeScore in m_CompletitionData)
            {
                m_TotalScore += episodeScore.score;
            }
        }

        public void SaveEpisodeResult(int levelScore)
        {
            if(Instance) Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }

        private void SaveResult(Episode currentLevel, int levelScore)
        {
            foreach(var item in m_CompletitionData)
            {
                if(item.episode == currentLevel)
                {
                    if (levelScore > item.score)
                    {
                        item.score = levelScore;
                        Saver<EpisodeScore[]>.Save(filename, m_CompletitionData);
                    }
                }
            }
        }


    }
}