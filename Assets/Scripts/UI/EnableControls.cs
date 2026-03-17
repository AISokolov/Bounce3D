using UnityEngine;

public class EnableControls : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BallController ball = other.GetComponent<BallController>();

        if (ball != null)
        {
            ball.controllerEnabled = true;
        }
    }
}
