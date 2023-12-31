using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDef
{


    public class BueUpgrade : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text m_LevelUpgrade, m_Coast;
        [SerializeField] private Button m_BueButton;
        [SerializeField] private UpgradeAsset asset;

        public void Initialize ()
        {
            var savedData = Upgrades.GetUpgradeLevel(asset);
            image.sprite = asset.m_Sprite;


            if(savedData >=asset.m_CoastByLevel.Length)
            {
                m_BueButton.interactable = false;
                m_LevelUpgrade.text = "X";
                m_Coast.text = "X";
            }
            else
            {
                m_LevelUpgrade.text = savedData.ToString();
                m_Coast.text = asset.m_CoastByLevel[savedData].ToString();
            }


        }

        public void Buy()
        {
            Upgrades.BuyUpgrade(asset);
            Initialize();
        }

    }
}