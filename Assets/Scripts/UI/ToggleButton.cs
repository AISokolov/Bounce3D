using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Image image;
    [SerializeField] private Sprite buttonOnSprite;
    [SerializeField] private Sprite buttonOffSprite;

    private bool isMuted;

    private void Awake()
    {
        if (image == null)
            image = GetComponent<Image>();

        isMuted = PlayerPrefs.GetInt("SOUND_MUTED", 0) == 1;
        Apply();
    }

    public void Toggle()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("SOUND_MUTED", isMuted ? 1 : 0);
        Apply();
    }

    private void Apply()
    {
        AudioListener.pause = isMuted;
        image.sprite = isMuted ? buttonOffSprite : buttonOnSprite;
    }
}
