using UnityEngine;

public class ControlModeApplier : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public GameObject up;
    public GameObject down;
    public GameObject joystick;

    private void Start()
    {
        int mode = PlayerPrefs.GetInt("ControlMode", 0);

        bool useJoystick = (mode == 0);
        bool useButtons = (mode == 1);

        if (joystick != null) joystick.SetActive(useJoystick);

        if (left != null) left.SetActive(useButtons);
        if (right != null) right.SetActive(useButtons);
        if (up != null) up.SetActive(useButtons);
        if (down != null) down.SetActive(useButtons);
    }
}