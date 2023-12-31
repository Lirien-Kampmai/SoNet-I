using UnityEngine;
using UnityEngine.UI;

public class HUDPanel : MonoBehaviour
{
    [SerializeField] private Text goldText;
    [SerializeField] private Text damageText;
    [SerializeField] private Text fireRateText;
    [SerializeField] private Text shotText;
    [SerializeField] private Text stoneText;
    [SerializeField] private Text currentLevelText;
    [SerializeField] private Text nextLevelText;

    [SerializeField] private Turret turret;
    [SerializeField] private Bag bag;
    [SerializeField] private StoneSpawner stoneSpawner;
    [SerializeField] private LevelState levelState;

    private void Update()
    {
        fireRateText.text = "Fire Rate: " + turret.RateOfFire.ToString();
        damageText.text = "Damage: " + turret.Damage.ToString();
        shotText.text = "Amount Shot: " + turret.AmountObject.ToString();

        int length = FindObjectsOfType<Stone>().Length;
        stoneText.text = "Left Stone: " + length.ToString();

        goldText.text = "Gold: " + bag.GetAmountGold().ToString();

        currentLevelText.text = "Level: " + levelState.Level.ToString();
        int next = levelState.Level + 1;
        nextLevelText.text = "Next Level: " + next.ToString();
    }
}
