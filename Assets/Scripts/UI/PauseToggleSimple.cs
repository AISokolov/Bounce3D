using UnityEngine;
using UnityEngine.UI;

public class PauseToggleSimple : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image image;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;

    [Header ("Overlay")]
    [SerializeField] private GameObject overlay;

    [Header ("Player ball")]
    [SerializeField] private GameObject ball;

    private bool isPaused;

    private void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();

        Apply();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;
        overlay.SetActive(isPaused);
        ball.GetComponent<BallController>().controllerEnabled = !isPaused;

        Apply();
    }

    private void Apply()
    {
        image.sprite = isPaused ? playSprite : pauseSprite;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f; // safety
    }
}
