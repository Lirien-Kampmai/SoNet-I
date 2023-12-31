using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public static LevelBoundary Instance;

    [SerializeField] private Vector2 screenResolution;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftBorder, -10, 0), new Vector3(leftBorder, 10, 0));
        Gizmos.DrawLine(new Vector3(rightBorder, -10, 0), new Vector3(rightBorder, 10, 0));
    }
#endif
    public float leftBorder
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        }
    }

    public float rightBorder
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(screenResolution.x, 0, 0)).x;
        }
    }
}
