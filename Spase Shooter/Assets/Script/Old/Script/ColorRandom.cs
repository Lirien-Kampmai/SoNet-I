using UnityEngine;

public class ColorRandom : MonoBehaviour
{
    private void Start()
    {
        ChangeColor();
    }
    private void ChangeColor()
    {
        Material material = this.GetComponent<Renderer>().material;
        material.color = new Color(Random.value, Random.value, Random.value, 1);
    }
}
