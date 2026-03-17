using UnityEngine;

[RequireComponent(typeof(Collider))]
public class JumpTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag))
            return;

        var controller = other.GetComponent<BallController>();
        if (controller != null)
        {
            controller.PressJump();
        }
    }
}
