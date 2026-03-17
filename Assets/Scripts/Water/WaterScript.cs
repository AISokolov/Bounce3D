using UnityEngine;

public class WaterScript : MonoBehaviour
{
    [Header("Water Physics (applied while inside)")]
    [SerializeField] private float waterDrag = 3f;
    [SerializeField] private float waterAngularDrag = 2f;

    [Header("Vertical Control (while inside water)")]
    [SerializeField] private float swimUpForce = 6f;   // hold big right button = up

    [Header("UI Hold Button (Big Right Button)")]
    [SerializeField] private HoldButton swimUpButton; 

    private Rigidbody ballRb;
    private float originalDrag;
    private float originalAngularDrag;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var bub = other.GetComponent<UnderwaterBubbles>();
        if (bub) bub.SetUnderwater(true);

        ballRb = other.GetComponent<Rigidbody>();
        if (!ballRb) return;

        originalDrag = ballRb.linearDamping;
        originalAngularDrag = ballRb.angularDamping;

        ballRb.linearDamping = waterDrag;
        ballRb.angularDamping = waterAngularDrag;

        var oxygen = FindFirstObjectByType<WaterOxygenUI>();
        if (oxygen != null)
            oxygen.StartOxygen(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!ballRb) return;

        ballRb.linearDamping = originalDrag;
        ballRb.angularDamping = originalAngularDrag;
        ballRb = null;

        var bub = other.GetComponent<UnderwaterBubbles>();
        if (bub) bub.SetUnderwater(false);

        var oxygen = FindFirstObjectByType<WaterOxygenUI>();
        if (oxygen != null)
            oxygen.StopOxygen();

    }

    private void FixedUpdate()
    {
        if (!ballRb) return;

        if (swimUpButton != null && swimUpButton.IsHeld)
        {
            ballRb.AddForce(Vector3.up * swimUpForce, ForceMode.VelocityChange);
        }
    }
}
