using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private string sceneName = "Level_1";
    void Awake()
    {
        Application.targetFrameRate = 90;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}