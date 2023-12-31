using UnityEngine;

public class Gold : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bag bag = collision.transform.root.GetComponent<Bag>();

        if (bag != null)
        {
            bag.AddGold(1);
            Destroy(gameObject);
        }
        
    }
}
