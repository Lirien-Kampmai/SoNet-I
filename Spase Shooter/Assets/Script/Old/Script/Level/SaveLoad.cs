using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] private Turret     turret;
    [SerializeField] private Bag        bag;
    [SerializeField] private LevelState levelState;

    private int gold;
    private int damage;
    private int shot;
    private int currentLevel;

    private float fireRate;

    private void Awake()
    {
        damage = turret.Damage;
        fireRate = turret.RateOfFire;
        shot = turret.AmountObject;
        gold = bag.GetAmountGold();
        currentLevel = levelState.Level;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Turret: Damage", damage);
        PlayerPrefs.SetFloat("Turret: RateOfFire", fireRate);
        PlayerPrefs.SetInt("Turret: AmountObject", shot);
        PlayerPrefs.SetInt("Bag: GetAmountGold", gold);
        PlayerPrefs.SetInt("levelState: Level", currentLevel);
    }

    public void Load()
    {
        damage = PlayerPrefs.GetInt("Turret: Damage", 0);
        fireRate = PlayerPrefs.GetFloat("Turret: RateOfFire", 0);
        shot = PlayerPrefs.GetInt("Turret: AmountObject", 0);
        gold = PlayerPrefs.GetInt("Bag: GetAmountGold", 0);
        currentLevel = PlayerPrefs.GetInt("levelState: Level", 0);
    }

}
