using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    /// <summary>
    /// Script responsible for projectile behavior.
    /// The scripts is attached to projectile entities.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        // speed projectile
        [SerializeField] private float m_VelocityProjectile;
        // damage projectile
        [SerializeField] private int m_DamageProjectile;
        // lifetime projectile
        [Range(0, 10)]
        [SerializeField] private float m_Lifetime;
        

        private float m_Timer;
        private Destructible m_Parent;

        private void Update()
        {
            #region formula for calculating the direction of the projectile
            float stepLenght = Time.deltaTime * m_VelocityProjectile;
            Vector2 step = transform.up * stepLenght;
            #endregion

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            if (hit)
            {
                // проверяем, есть ли дестрактибл в цели попадания проджектайла
                Destructible destr = hit.collider.transform.root.GetComponent<Destructible>();
                // проверка не является ли цель создателем проджектайла
                if(destr != null && destr != m_Parent)
                {
                    destr.ApplyDamage(m_DamageProjectile);

                    /*
                    ебанина, которую надо богоугодно переделать в скрипт начисления очков за убийство
                    // checking belonging to the player ship
                    if(m_Parent == Player.Instance.ActiveShip)
                    {
                        // add a point if the enemy ship is destroyed
                        Player.Instance.AddScore(destr.ScoreValue);
                        Player.Instance.AddKill();
                    }
                    */
                }

                // method is called when a raycast hits
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            #region self destroy timer
            m_Timer += Time.deltaTime;
            if (m_Timer > m_Lifetime)
                Destroy(gameObject);
            #endregion

            // projectile direction movement
            transform.position += new Vector3(step.x, step.y, 0);
        }

        private void OnProjectileLifeEnd (Collider2D collider, Vector2 position)
        {
            Destroy(gameObject);
        }

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}


