using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class GlobalStat : SingletonBase<GlobalStat>
    {
        private int GlobalScore;

        public void StartStat()
        {
            GlobalScore += Player.Instance.Score;
        }
    }
}


