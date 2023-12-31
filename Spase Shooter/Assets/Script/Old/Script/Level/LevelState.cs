using UnityEngine;
using UnityEngine.Events;


public class LevelState : MonoBehaviour
{
    [SerializeField] CartModel    cart;
    [SerializeField] StoneSpawner spawner;

    private float timer;
    private bool  checkPassed;
    private int   level;

    public int Level => level;

    [Space(5)]
    public UnityEvent Passed;
    public UnityEvent Defeat;

    private void Awake()
    {
        spawner.Completed.AddListener(OnSpawnComplited);
        cart.CollisionStone.AddListener(OnCartCollisionStone);
    }

    private void OnDestroy()
    {
        spawner.Completed.RemoveListener(OnSpawnComplited);
        cart.CollisionStone.RemoveListener(OnCartCollisionStone);
    }

    public void CheckPassed()
    {
        checkPassed = false;
    }

    private void OnSpawnComplited()
    {
        checkPassed = true;
    }
    private void OnCartCollisionStone()
    {
        Defeat.Invoke();
        checkPassed = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2.5f)
        {
            if (checkPassed == true)
            {
                if (FindObjectsOfType<Stone>().Length == 0)
                {
                    Passed.Invoke();
                    level++;
                }
            }
            timer = 0;
        }
    }
}
