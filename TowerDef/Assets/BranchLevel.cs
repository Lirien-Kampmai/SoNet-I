using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    [RequireComponent(typeof(SelectMapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private SelectMapLevel m_RootLevel;
        private int m_NeedPoint = 3;

        public bool RootIsActiv { get { return m_RootLevel.IsComplited; } }
    }
}