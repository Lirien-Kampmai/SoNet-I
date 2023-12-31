using System;
using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Stone       generatedObject;
    [SerializeField] private Transform[] spawnPosition;
    [SerializeField] private float       spawnRate;

    [SerializeField] Turret turret;
    [SerializeField]                    private int   amount;
    [SerializeField]                    private float maxHPRate;
    [SerializeField][Range(0.0f, 1.0f)] private float minHPPercentage;

                     private float timer;
                     private float amountSpawned;
    [SerializeField] private int   stoneMaxHP;
    [SerializeField] private int   stoneMixHP;

    [Space(10)] public UnityEvent Completed;

    [SerializeField] Transform parent;

    private void Start()
    {
        int perSecDamage = (int)((turret.Damage * turret.AmountObject) * (1 / turret.RateOfFire));
        stoneMaxHP = (int)(perSecDamage * maxHPRate);
        stoneMixHP = (int)(stoneMaxHP * minHPPercentage);
        timer = spawnRate;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            Spawn();
            timer = 0;
        }

        if (amountSpawned == amount)
        {
            enabled = false;
            Completed.Invoke();
        }
    }

    public void StartSpawn()
    {
        enabled = true;
        amountSpawned = 0;
    }

    private void Spawn()
    {
        Stone stone = Instantiate(generatedObject, spawnPosition[UnityEngine.Random.Range(0, spawnPosition.Length)].position, Quaternion.identity);
        stone.SetSize((Stone.Size) UnityEngine.Random.Range(1, 4));
        amountSpawned++;
        stone.maxHitPoint = UnityEngine.Random.Range(stoneMixHP, stoneMaxHP + 1);
        stone.transform.SetParent(parent);
    }
    public void AddAmount(int i)
    {
        amount += i;
    }
}
