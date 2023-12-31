using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using Unity.VisualScripting;

namespace TowerDef
{
    public class TDPlayer : Player
    {
        [SerializeField] private int m_Gold = 0;

        private static event Action<int> OnGoldUpdate;
        public static void GoldUpdateSubscribe(Action<int> action)
        {
            OnGoldUpdate += action;
            action((Player.Instance as TDPlayer).m_Gold);
        }

        public static event Action<int> OnLifeUpdate;
        public static void LifeUpdateSubscribe(Action<int> action)
        {
            OnLifeUpdate += action;
            action((Player.Instance as TDPlayer).AmountLife);
        }

        // изменение голды и оповещение худа через эвент
        public void ChangeGold(int change)
        {
            m_Gold += change;
            OnGoldUpdate(m_Gold);
        }

        // уменьшение жизней (наследование) и оповещение худа через эвент
        public void ReduceLife(int change)
        {
            TakeDamage(change);
            OnLifeUpdate(AmountLife);
        }

        [SerializeField] private GameObject m_TowerAsset;

        public void TryBuild(TowerAsset m_ta, Transform m_BuildSite)
        {
            if(m_Gold >= m_ta.goldCoast)
            {
                ChangeGold(m_ta.goldCoast);
                var tower = Instantiate(m_TowerAsset, m_BuildSite.position , Quaternion.identity);
                tower.GetComponentInChildren<SpriteRenderer>().sprite = m_ta.TowerSprite;
                tower.GetComponentInChildren<Turret>().m_TurretProperties = m_ta.Projectile;
                Destroy(m_BuildSite.gameObject);
            }
        }
    }
}


