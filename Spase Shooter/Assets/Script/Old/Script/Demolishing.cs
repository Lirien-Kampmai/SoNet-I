using UnityEngine;
using UnityEngine.Events;

public class Demolishing : MonoBehaviour
{
    public  int maxHitPoint;
    private int hitPoint;

    [HideInInspector] public UnityEvent Die;
    [HideInInspector] public UnityEvent ChangeHitPoint;

    private bool isDie = false;

    private void Start()
    {
        hitPoint = maxHitPoint;
        ChangeHitPoint.Invoke();
    }

    public void ApplyHeal (int heal)
    {
        hitPoint += heal;
        ChangeHitPoint.Invoke();
    }

    public void ApplyDamage (int damage)
    {
        hitPoint -= damage;

        ChangeHitPoint.Invoke();

        if (hitPoint <= 0)
        {
            Kill ();
        }
    }

    public void Kill ()
    {
        
        hitPoint = 0;
        

        ChangeHitPoint.Invoke();
        Die.Invoke();

        isDie = true;
        if (isDie == true) return;
    }

    public int GetHitPoint ()
    {
        return hitPoint;
    }

}
