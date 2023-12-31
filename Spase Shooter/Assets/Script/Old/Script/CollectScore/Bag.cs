using UnityEngine;

public class Bag : MonoBehaviour
{
    private int amountGold;

    public int BagGold => amountGold;

    public void AddGold(int amount)
    {
        amountGold += amount;
    }

    public int GetAmountGold()
    {
        return amountGold;
    }

    public void DrawGold (int amount)
    {
        if (amountGold - amount > 0) amountGold -= amount;
    }
}
