using UnityEngine;
using UnityEngine.UI;

public class ControlModeSwitcher : MonoBehaviour
{
    public Image icon;
    public Sprite joystickSprite;
    public Sprite buttonsSprite;

    public int controlMode = 0; // 0 = joystick, 1 = buttons

    private void Start()
    {
        controlMode = PlayerPrefs.GetInt("ControlMode", 0);
        UpdateVisual();
    }

    public void ToggleMode()
    {
        controlMode = 1 - controlMode;

        PlayerPrefs.SetInt("ControlMode", controlMode);
        PlayerPrefs.Save();

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (icon == null) return;
        icon.sprite = (controlMode == 0) ? joystickSprite : buttonsSprite;
    }
}