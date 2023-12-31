using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private SelectMapLevel[] m_level;
        [SerializeField] private BranchLevel[] m_Branchlevel;

        private void Start()
        {
            var drawLevel = 0;
            while(drawLevel < m_level.Length && MapCompletition.Instance.TryIndexCompletitionData(drawLevel, out var episode, out var score))
            {
                m_level[drawLevel++].SetLevelData(episode, score);
                if (score == 0) break;
            }
            for(int i = drawLevel; i < m_level.Length; i++)
            {
                m_level[i].gameObject.SetActive(false);
            }
            for(int i = 0; i < m_Branchlevel.Length; i++)
            {
                m_Branchlevel[i].gameObject.SetActive(m_Branchlevel[i].RootIsActiv);
            }

        }

    }
}