using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDef
{
    public class TowerPurchaseController : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_ta;

        [SerializeField] private Text m_Text;

        [SerializeField] private Button m_Button;

        [SerializeField] private Transform m_BuildSite;

        public Transform BuildSite { set { m_BuildSite = value; } }

        private void Start()
        {
            // подписываемся на обновление
            TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);

            // загоняем цену башни в текст кнопки
            m_Text.text = m_ta.goldCoast.ToString();

            //
            m_Button.GetComponentInChildren<Image>().sprite = m_ta.HUDSprite;
            print(m_ta.HUDSprite.name);
        }

        private void GoldStatusCheck(int gold)
        {
            if(gold >= m_ta.goldCoast != m_Button.interactable)
            {
                // изменяем состояние кнопки
                m_Button.interactable = !m_Button.interactable;

                // если кнопка интерактивная, то цвет белый, если нет то красный
                m_Text.color = m_Button.interactable ? Color.white : Color.red;
            }
        }

        public void PurchaseTower()
        {
            (Player.Instance as TDPlayer).TryBuild(m_ta, m_BuildSite);
            OnPointerDownClick.HideBuildInterface();
        }


    }
}
