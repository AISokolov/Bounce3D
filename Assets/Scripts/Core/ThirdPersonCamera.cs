using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private BallController controller; // drag PlayerBall here too

    [Header("Distance")]
    [SerializeField] private float height = 5f;
    [SerializeField] private float distance = 8f;

    [Header("Smoothing")]
    [SerializeField] private float posSmooth = 10f;
    [SerializeField] private float rotSmooth = 10f;

    private Vector3 currentForward = Vector3.forward;

    private void LateUpdate()
    {
        if (target == null) return;

        // Decide which direction camera should be behind
        if (controller != null)
        {
            Vector3 dir = controller.LastMoveDirection;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
                currentForward = Vector3.Slerp(currentForward, dir.normalized, rotSmooth * Time.deltaTime);
        }

        // Camera position behind target
        Vector3 desiredPos = target.position - currentForward * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPos, posSmooth * Time.deltaTime);

        // Look at target
        Quaternion desiredRot = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, rotSmooth * Time.deltaTime);
    }
}
