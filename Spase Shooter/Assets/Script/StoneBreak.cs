using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(DrawCircleArea))]
    public class StoneBreak : MonoBehaviour
    {
        [SerializeField] private Destructible [] m_DebtisPrefab;
        [SerializeField] private uint m_MaxDebrisGenerate;
        [SerializeField] private uint m_MinDebrisGenerate;
        private DrawCircleArea m_Radius;
        private Vector2 m_Velocity;

#if UNITY_EDITOR
        private void OnValidate()
        {
            m_Radius = GetComponent<DrawCircleArea>();
        }
#endif

        private void OnDestroy()
        {
            m_MinDebrisGenerate = 0;
            int spanwnvalue = (int)Random.Range(m_MinDebrisGenerate, m_MaxDebrisGenerate);
            for(int i = 0; i < spanwnvalue; i++) SpawnStone();
        }

        private void SpawnStone()
        {
            int index = Random.Range(0, m_DebtisPrefab.Length);
            
            GameObject debris = Instantiate(m_DebtisPrefab[index].gameObject, transform.parent);
            debris.transform.position = m_Radius.GetRandonInsideZone();
            debris.transform.Rotate(new Vector3(0, 0, Random.Range (-20, 20) ));
            

            #region random number generation except for 0 
            int number = 0;
            int newNumber;
            do
            {
                newNumber = Random.Range(-4, 4);
            }
            while (number == newNumber);
            number = newNumber;
            #endregion


            m_Velocity = new Vector2(number, number);

            Rigidbody2D rigidbody2D = debris.GetComponent<Rigidbody2D>();          
            if (rigidbody2D != null)
            {
                rigidbody2D.AddRelativeForce(m_Velocity, ForceMode2D.Impulse);
            }






        }

    }
}


