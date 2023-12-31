using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Script limiting the position on the level.
    /// The script is bound to the object to be restricted.
    /// Works in conjunction with a script "LevelBorders".
    /// </summary>
    public class LevelBorderLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBorders.Instance == null) return;

            // link to LevelBorders
            var lbl = LevelBorders.Instance;
            // linr to radius
            var r = lbl.Radius;
            // link to Destructible
            var destructible = transform.root.GetComponent<Destructible>();

            if (transform.position.magnitude > r)
            {
                if (lbl.LimitMode == LevelBorders.Mode.Limit)
                {
                    transform.position = transform.position.normalized * r;
                }

                if (lbl.LimitMode == LevelBorders.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }

                if (lbl.LimitMode == LevelBorders.Mode.Death)
                {
                   if (destructible != null) destructible.ApplyDamage(int.MaxValue);
                }
            }
        }
    }
}



