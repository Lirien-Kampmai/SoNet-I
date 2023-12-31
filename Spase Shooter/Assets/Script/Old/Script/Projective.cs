using UnityEngine;

public class Projective : MonoBehaviour
{
    [Header("Settings Generated object")]
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
                     private int   damage;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Demolishing demolishing = collision.transform.parent.GetComponent<Demolishing>();

        if (demolishing != null) 
        {
            demolishing.ApplyDamage(damage);
        }
        Destroy(gameObject);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
