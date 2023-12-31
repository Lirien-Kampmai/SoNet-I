using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDef
{
    public class OnPointerDownClick : MonoBehaviour, IPointerDownHandler
    {
        public static event Action<Transform> OnClickEvent;

        public static void HideBuildInterface()
        {
            OnClickEvent(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(transform.root);
        }
    }
}