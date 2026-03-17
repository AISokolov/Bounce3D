using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuExit : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}