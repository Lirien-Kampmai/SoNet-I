using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDef
{

    public class UpgradeShop : MonoBehaviour
    {

        [SerializeField] private int m_Money;
        [SerializeField] private Text m_MoneyText;
        [SerializeField] private BueUpgrade[] sales;

        private void Start()
        {
            UpdateMoney();

            foreach(var window in sales)
            {
                window.Initialize(UpdateMoney);  
            }

        }

        public void UpdateMoney()
        {
            m_Money = MapCompletition.Instance.TotalScore;
            m_MoneyText.text = m_Money.ToString();
        }
    }
}