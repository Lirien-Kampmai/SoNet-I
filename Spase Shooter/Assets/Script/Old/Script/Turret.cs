using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Projective generatedObject;
    [SerializeField] private Transform  generationPosition;

    [SerializeField] private float rateOfFire;
    [SerializeField] private float intervalObject;

    [SerializeField] public int amountObject;
    [SerializeField] public int damage;

    public int   Damage       => damage;
    public int   AmountObject => amountObject;
    public float RateOfFire   => rateOfFire;


    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void SpawnProjective()
    {
        float startPosX = generationPosition.position.x - intervalObject * (amountObject - 1) * 0.5f;

        for (int i = 0; i < amountObject; i++)
        {
            Projective projective = Instantiate(generatedObject, new Vector3(startPosX + i * intervalObject, generationPosition.position.y, generationPosition.position.z), transform.rotation);
            projective.SetDamage(damage);
        }
    }

    public void Fire()
    {
        if (timer >= rateOfFire)
        {
            SpawnProjective();
            timer = 0;
        }
    }

    public void AddDamage(int i)
    {
        damage += i;
    }

    public void AddAmountObject(int i)
    {
        amountObject += i;
    }

    public void AddRateOfFire(float i)
    {
        rateOfFire -= i;
    }
}
