using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private List<Segment> defoltSegments;

    public void AddEmptySegment (int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            defoltSegments[i].SetEmpty();
        }

        for (int i = amount; i >= 0; i--)
        {
            defoltSegments.RemoveAt(i);
        }
    }

    public void AddRandomTrapSegment(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            
            int index = UnityEngine.Random.Range(0, defoltSegments.Count);

            defoltSegments[index].SetTrap();
            defoltSegments.RemoveAt(index);
        }
    }
    public void SetFinishSegment()
    {
        for (int i = 0;i < defoltSegments.Count;i++)
        {
            defoltSegments[i].SetFinish();
        }
    }
    public void SetRandomRotate()
    {
        transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
    }

}
