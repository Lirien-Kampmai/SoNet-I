using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDef
{
    public class HudTextUpdate : MonoBehaviour
    {
        // перечисляем типы апдейта
        public enum UpdateSourse
        {
            Gold,
            Life
        }

        // событие апдейта по умолчанию
        public UpdateSourse sourse = UpdateSourse.Gold;

        // ссылка на изменяемый текст
        private Text m_UIText;

        void Start()
        {
            m_UIText = GetComponentInChildren<Text>();

            switch (sourse)
            {
                case UpdateSourse.Gold:
                    TDPlayer.GoldUpdateSubscribe(UpdateText);
                    break;

                case UpdateSourse.Life:
                    TDPlayer.LifeUpdateSubscribe(UpdateText);
                    break;
            }
        }

        // Update is called once per frame
        void UpdateText(int money)
        {
            m_UIText.text = money.ToString();
        }
    }
}


