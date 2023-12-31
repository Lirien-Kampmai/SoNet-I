using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneState : MonoBehaviour
{
    public void RestartLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void ResetLevel()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit ()
    {
        Application.Quit ();
    }
}
