using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// The script is responsible for adjusting the background parallax effect.
    /// The script is attached to the background entity.
    /// </summary>

    [RequireComponent (typeof(MeshRenderer))]
    public class BackgroundSpace : MonoBehaviour
    {
        // power parallax effect
        [Range(0.0f, 4.0f)]
        [SerializeField] private float m_ParallaxPower;

        // scale texture background
        [SerializeField] private float m_TextureScale;

        // link to material
        private Material m_StarsMaterial;
        // initialization point
        private Vector2 m_InitialOffset;

        private void Start()
        {
            // get link to material
            m_StarsMaterial = GetComponent<MeshRenderer>().material;

            // set random point inside or on a circle with radius 1.0f
            m_InitialOffset = UnityEngine.Random.insideUnitCircle;

            // main texture scale
            m_StarsMaterial.mainTextureScale = Vector2.one * m_TextureScale;
        }

        private void Update()
        {
            Vector2 offset = m_InitialOffset;

            offset.x += transform.position.x / transform.localScale.x / m_ParallaxPower;
            offset.y += transform.position.y / transform.localScale.y / m_ParallaxPower;

            m_StarsMaterial.mainTextureOffset = offset;
        }


    }
}



