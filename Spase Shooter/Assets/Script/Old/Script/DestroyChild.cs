using UnityEngine;

public class DestroyChild : MonoBehaviour
{
    [SerializeField] Transform Parrent;
    public void DeletChildren()
    {
        int n = Parrent.childCount;
        for (int i = 0; i < n; i++)
        {
            Destroy(Parrent.GetChild(i).gameObject);
        }
    }
}
