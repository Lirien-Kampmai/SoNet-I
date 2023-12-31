using UnityEngine;
using UnityEngine.Events;

public class CartModel : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float cartWeight;

    [Header("Wheel")]
    [SerializeField] Transform[] wheel;
    [SerializeField] private float wheelRadius;
    private float deltaMovement;
    private float lastPositionX;

    private Vector3 movementTarget;

    [HideInInspector] public UnityEvent CollisionStone;

    private void Start()
    {
        movementTarget = transform.position;
    }
    private void Update()
    {
        Move();
        RotateWheel();
    }
    private void Move ()
    {
        lastPositionX = transform.position.x;
        transform.position = Vector3.MoveTowards(transform.position, movementTarget, movementSpeed * Time.deltaTime);
        deltaMovement = transform.position.x - lastPositionX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Stone stone = collision.transform.parent.GetComponent<Stone>();

        if (stone != null)
        {
            CollisionStone.Invoke();
            Debug.Log("collision stone + turret");
        }
    }

    private void RotateWheel()
    {
        float angle = (180 * deltaMovement) / (Mathf.PI * wheelRadius * 2);

        for (int i = 0; i < wheel.Length; i++)
        {
            wheel[i].Rotate(0, 0, - angle);
        }
    }

    public void SetMovementTarget(Vector3 target) 
    {
        movementTarget = ClampMovementTarget(target);
    }

    private Vector3 ClampMovementTarget(Vector3 target)
    {
        float leftBorder = LevelBoundary.Instance.leftBorder + cartWeight * 0.5f;
        float rightBorder = LevelBoundary.Instance.rightBorder - cartWeight * 0.5f;

        Vector3 movTarget = target;

        movTarget.z = transform.position.z;
        movTarget.y = transform.position.y;

        if (movTarget.x < leftBorder) movTarget.x = leftBorder;
        if (movTarget.x > rightBorder) movTarget.x = rightBorder;

        return movTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position - new Vector3(cartWeight * 0.5f, 0.5f, 0), transform.position + new Vector3(cartWeight * 0.5f, -0.5f, 0));
    }
#endif

}
