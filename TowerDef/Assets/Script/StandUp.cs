using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDef
{
    public class StandUp : MonoBehaviour
    {
        private Rigidbody2D m_TargetRigidbody2D;
        private SpriteRenderer m_TargetSpriteRenderer;

        private void Start()
        {
            m_TargetRigidbody2D = transform.root.GetComponent<Rigidbody2D>();
            m_TargetSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            transform.up = Vector2.up;
            var motionX = m_TargetRigidbody2D.velocity.x;
            if (motionX > 0.01f)
                m_TargetSpriteRenderer.flipX = false;
            else if (motionX < 0.01f)
                m_TargetSpriteRenderer.flipX = true;
        }
    }
}


