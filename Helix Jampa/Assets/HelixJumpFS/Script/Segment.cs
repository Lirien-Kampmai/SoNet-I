using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SegmentType
{
    Defolt,
    Trap,
    Empty,
    Finish
}

public class Segment : MonoBehaviour
{
    [SerializeField] Material trapMaterial;
    [SerializeField] Material finishMaterial;

    [SerializeField] SegmentType segmentType;

    public SegmentType Type => segmentType;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetTrap()
    {
        meshRenderer.enabled = true;
        meshRenderer.material = trapMaterial;

        segmentType = SegmentType.Trap;
    }

    public void SetEmpty()
    {
        meshRenderer.enabled = false;
        segmentType = SegmentType.Empty;
    }
    public void SetFinish()
    {
        meshRenderer.material = finishMaterial;
        meshRenderer.enabled = true;
        segmentType = SegmentType.Finish;
    }


}
