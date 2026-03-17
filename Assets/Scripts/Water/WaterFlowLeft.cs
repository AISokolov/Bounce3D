using UnityEngine;

public class WaterFlowLeft : MonoBehaviour
{
    [SerializeField] private float force = 4f;
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        rb.AddForce(Vector3.left * force, ForceMode.Acceleration);
    }
}
