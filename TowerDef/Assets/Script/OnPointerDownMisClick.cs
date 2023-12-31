using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDef
{

    public class OnPointerDownMisClick : OnPointerDownClick
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideBuildInterface();
        }
    }
}