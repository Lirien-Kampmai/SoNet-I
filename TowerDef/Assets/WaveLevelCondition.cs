using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{

    public class WaveLevelCondition : MonoBehaviour, LevelCondition
    {
        private bool isComplited;

        public bool IsComplited { get { return isComplited; } }

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavedead += () => { isComplited = true; };
        }
    }
}