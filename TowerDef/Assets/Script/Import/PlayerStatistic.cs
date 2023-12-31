using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    public class PlayerStatistic : MonoBehaviour
    {
        public int Numkill;
        public int Score;
        public int Gametime;

        public void Reset()
        {
            Gametime = 0;
            Score = 0;
            Numkill = 0;    
        }
    }
}


