using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Base script responsible for powerup the player.
    /// The scripts is not attached to entities, but interacts with other scripts.
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class PowerUp : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaseShip ship = collision.transform.root.GetComponent<SpaseShip>();

            if (ship != null && Player.Instance.ActiveShip)
            {
                OnPicketUp(ship);
                Destroy(gameObject);
            }
        }

        protected abstract void OnPicketUp(SpaseShip ship);
    }
}


