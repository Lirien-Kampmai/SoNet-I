using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    /// <summary>
    /// Script checks if the button is pressed
    /// </summary>
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool m_Hold;
        public bool IsHold => m_Hold;

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Hold = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_Hold = false;
        }
    }
}


