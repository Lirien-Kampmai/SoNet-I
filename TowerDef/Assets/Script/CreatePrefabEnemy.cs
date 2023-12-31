using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    [CreateAssetMenu]
    public sealed class CreateEnemyAsset : ScriptableObject
    {
        [Header("Visual")]
        public Color ColorEnemy = Color.white;
        public Vector2 spriteScale = new Vector2();
        public RuntimeAnimatorController animatorController;

        [Header("StatSettings")]
        public float movespeed = 1;
        public int hitpoint= 1;
        public int score = 1;
        public float radiusCollider = 0.20f;
        public int damage = 1;
        public int gold = 0;
    }
}


