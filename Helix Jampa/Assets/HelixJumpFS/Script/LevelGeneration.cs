using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private Transform axis;
    [SerializeField] private Floor floorPrefab;

    [Header("Settings")]
    [SerializeField] private int defoltFloorAmount;
    [SerializeField] private float floorHeight;
    [SerializeField] private int amountEmptySegment;
    [SerializeField] private int minAmountTrapSegment;
    [SerializeField] private int maxAmountTrapSegment;

    private float floorAmount = 0;
    public Transform BALLTRANSFORM;

    private void Start()
    {
        Generate(1);
        BALLTRANSFORM.position = new Vector3(BALLTRANSFORM.position.x, floorAmount * floorHeight - floorHeight, BALLTRANSFORM.position.z);
    }

    public void Generate (int level)
    {
        DestroyChild();

        floorAmount = defoltFloorAmount + level;

        axis.transform.localScale = new Vector3(1, floorAmount * floorHeight + floorHeight, 1);

        for(int i = 0; i < floorAmount; i++)
        {
            Floor floor = Instantiate(floorPrefab, transform);
            floor.transform.Translate(0, i * floorHeight, 0);
            floor.name = "Floor " + i;

            if (i == 0) floor.SetFinishSegment();

            if (i > 0 ||  i < floorAmount - 1)
            {
                floor.SetRandomRotate();
                floor.AddEmptySegment(amountEmptySegment);
                floor.AddRandomTrapSegment(Random.Range(minAmountTrapSegment, maxAmountTrapSegment + 1));
            }

            if (i == floorAmount - 1) floor.AddEmptySegment(amountEmptySegment);

        }

    }

    private void DestroyChild()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) != axis) continue;

            Destroy(transform.GetChild(i).gameObject);
        }
    }


}
