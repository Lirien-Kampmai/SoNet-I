using System;
using UnityEditor;
using UnityEngine;

namespace TowerDef
{
    [RequireComponent(typeof(TDPatrolController))]
    public class EnemySettings : MonoBehaviour
    {
        [SerializeField] private int m_DamageToPlayer;
        [SerializeField] private int m_DropGold;

        public event Action OnEnd;

        private void OnDestroy()
        {
            OnEnd?.Invoke();
        }

        public void UseEnemy(CreateEnemyAsset assetEnemy)
        {
            #region Sprite
            var spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = assetEnemy.ColorEnemy;
            spriteRenderer.transform.localScale = new Vector3(assetEnemy.spriteScale.x, assetEnemy.spriteScale.y, 1);
            #endregion
            
            #region Animator
            var animator = transform.GetComponentInChildren<Animator>();
            animator.runtimeAnimatorController = assetEnemy.animatorController;
            #endregion

            #region StatSettings
            GetComponent<SpaseShip>().Use(assetEnemy);
            var collider = transform.GetComponentInChildren<CircleCollider2D>();
            collider.radius = assetEnemy.radiusCollider;
            m_DamageToPlayer = assetEnemy.damage;
            m_DropGold = assetEnemy.gold;
            #endregion
        }

        public void CameToTheLastPoint()
        {
            (Player.Instance as TDPlayer).ReduceLife(m_DamageToPlayer);
        }

        public void GivePlayerGold()
        {
            (Player.Instance as TDPlayer).GetGold(m_DropGold);
        }

        [CustomEditor(typeof(EnemySettings))]
        public class EnemyInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                CreateEnemyAsset asset = EditorGUILayout.ObjectField(null, typeof(CreateEnemyAsset), false) as CreateEnemyAsset;

                if(asset)
                {
                    (target as EnemySettings).UseEnemy(asset);
                }
            }
        }
    }
}


