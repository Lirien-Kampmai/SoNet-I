using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    public class PurchaseController : MonoBehaviour
    {
        private RectTransform m_RectTransform;

        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();

            OnPointerDownClick.OnClickEvent += MoveToBuildPoint;
            gameObject.SetActive(false);
        }

        private void MoveToBuildPoint(Transform transformBuildPoint)
        {
            if(transformBuildPoint)
            {
                var position = Camera.main.WorldToScreenPoint(transformBuildPoint.position);
                m_RectTransform.anchoredPosition = position;
                gameObject.SetActive(true);

            }
            else
            {
                gameObject.SetActive(false);
            }

            foreach(var towerPurchaseController in GetComponentsInChildren<TowerPurchaseController>())
            {
                towerPurchaseController.BuildSite = transformBuildPoint;
            }

        }
    }
}