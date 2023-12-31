using UnityEngine;

public class CartInputControl : MonoBehaviour
{
    [Header("Control object")]
    [SerializeField] CartModel cartMovement;
    [Header("Fire control object")]
    [SerializeField] Turret turret;

    private void Update()
    {
        cartMovement.SetMovementTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButtonDown(0) == true) turret.Fire();
    }
}
