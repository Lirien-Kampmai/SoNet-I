using UnityEngine;

public class MoneyMovement : MonoBehaviour
{
    [SerializeField] private float gravity;

    private bool UseGravity;

    private void Start()
    {
        TryEnabledGravity();
    }

    private void Update()
    {
        Move();
    }

    private void TryEnabledGravity()
    {
        UseGravity = true;
    }

    private void Move()
    {
        if (UseGravity == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (gravity * Time.deltaTime), transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelEdge levelEdge = collision.GetComponent<LevelEdge>();

        if (levelEdge != null)
        {
            if (levelEdge.Type == EdgeType.Bottom)
            {
                UseGravity = false;
            }
        }
    }
}
