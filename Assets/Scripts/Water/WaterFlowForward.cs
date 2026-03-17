using UnityEngine;

public class WaterFlowForward : MonoBehaviour
{
    [SerializeField] private float force = 4f;
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        rb.AddForce(Vector3.forward * force, ForceMode.Acceleration);
    }
}
