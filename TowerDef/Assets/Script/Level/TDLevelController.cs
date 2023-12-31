using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    public class TDLevelController : LevelController
    {
        private int LevelScore = 3;
        //public int LevelScore => 1;

        private new void Start()
        {
            base.Start();
            // лямбда выражение
            TDPlayer.Instance.OnPlayerDead += () =>
            {
                StopLevelActiv();
                ResultPanelController.Instance.ShowResult(false);
            };

            m_ReferenceTime += Time.time;

            m_EventLevelComplited.AddListener(() =>
            {
                StopLevelActiv();
                if(m_ReferenceTime < Time.time)
                {
                    LevelScore -= 1;
                }

                MapCompletition.Instance.SaveEpisodeResult(LevelScore);
            });

            void LifeScoreChange(int _)
            {
                LevelScore -= 1;
                TDPlayer.OnLifeUpdate -= LifeScoreChange;
            }
            TDPlayer.OnLifeUpdate += LifeScoreChange;
        }

        private void StopLevelActiv()
        {
            void Disabled<T> () where T : MonoBehaviour
            {
                foreach(var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            Disabled<SpawnerEnemy>();
            Disabled<Projectile>();
            Disabled<TowerController>();
            Disabled<EnemySettings>();
            Disabled<InvokeNextWave>();
        }

    }
}