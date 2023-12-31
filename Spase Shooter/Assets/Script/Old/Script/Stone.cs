using UnityEngine;

[RequireComponent(typeof(StoneMovement))]
public class Stone : Demolishing
{
    public enum Size
    {
        Small,
        Medium,
        Big,
        VeryBig
    }

    [SerializeField] Size          size;
    [SerializeField] private float spawnUpForse;
    [SerializeField] Money         money;

    private StoneMovement movement;

    private void Awake()
    {
        movement = GetComponent<StoneMovement>();
        Die.AddListener(OnStoneDestroy);
        SetSize(size);
    }

    private void OnDestroy()
    {
        Die.RemoveListener(OnStoneDestroy);
    }

    private void OnStoneDestroy()
    {
        if (size != Size.Small)
        {
            SpawnStone();
            SpawnMoney();
        }
        Destroy(gameObject);
        Debug.Log("die");
    }

    public void SpawnMoney()
    {
        int drop = Random.Range(0, 100);
        int chance = Random.Range(10, 15);

        if (chance >= drop)
            Instantiate(money, transform.position, Quaternion.identity);
        else
            return;
    }

    private void SpawnStone()
    {
        for (int i = 0; i < 2; i++)
        {
            Stone stone = Instantiate(this, transform.position, Quaternion.identity, transform.parent);
            stone.SetSize(size - 1);
            stone.maxHitPoint = Mathf.Clamp(maxHitPoint / 2, 1, maxHitPoint);
            stone.movement.AddVerticalVelocity(spawnUpForse);
            stone.movement.SetHorizontalDirection((i % 2 * 2) - 1);
            transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.001f);
        }
    }

    public void SetSize(Size size)
    {
        if (size < 0) return;
        transform.localScale = GetVectorFromSize(size);
        this.size = size;
    }

    private Vector3 GetVectorFromSize (Size size)
    {
        if (size == Size.Small) return new Vector3(0.2f, 0.2f, 0.2f);
        if (size == Size.Medium) return new Vector3(0.4f, 0.4f, 0.4f);
        if (size == Size.Big) return new Vector3(0.6f, 0.6f, 0.6f);
        if (size == Size.VeryBig) return new Vector3(1f, 1f, 1f);

        return Vector3.one;
    }
}
