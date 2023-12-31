using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{

    [CreateAssetMenu]
    public class UpgradeAsset : ScriptableObject
    {
        public Sprite m_Sprite;
        public int[] m_CoastByLevel = { 3 };


    }
}