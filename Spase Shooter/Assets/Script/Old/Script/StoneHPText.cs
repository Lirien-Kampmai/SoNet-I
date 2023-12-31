using UnityEngine;
using UnityEngine.UI;

public class StoneHPText : MonoBehaviour
{
    [SerializeField] private Text hpText;

    private Demolishing demolishing;

    private void Awake()
    {
        demolishing = GetComponent<Demolishing>();
        demolishing.ChangeHitPoint.AddListener(OnChangeHitPoint);
    }

    private void OnDestroy()
    {
        demolishing.ChangeHitPoint.RemoveListener(OnChangeHitPoint);
    }

    private void OnChangeHitPoint()
    {
        int hitPoints = demolishing.GetHitPoint();

        if (hitPoints >= 1000)
            hpText.text = hitPoints / 1000 + "K";
        else
            hpText.text = hitPoints.ToString();
    }
}
