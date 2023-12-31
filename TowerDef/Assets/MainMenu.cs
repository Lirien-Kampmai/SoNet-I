using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDef
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_ContinueButton;

        private void Start()
        {
            if (FileHandler.HasFile(MapCompletition.filename) == false)
                m_ContinueButton.SetActive(false);
            else
                m_ContinueButton.SetActive(true);
        }

        public void NewGame()
        {
            FileHandler.Reset(MapCompletition.filename);
            SceneManager.LoadScene(1);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }





    }
}