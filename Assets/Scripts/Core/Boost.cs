using UnityEngine;

public class Boost : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        rb.AddForce(Vector3.right * force, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        BallController ball = other.GetComponent<BallController>();
        
        if (ball != null)
        {
            ball.controllerEnabled = false;
        }
    }
}
