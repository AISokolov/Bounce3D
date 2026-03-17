using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform visualChild;

    [Header("Animation")]
    [SerializeField] private string triggerName = "Activate";
    [SerializeField] private string idleStateName = "Idle";
    [SerializeField] private string activatingTag = "Player";

    private bool played;
    private float ignoreUntilTime;

    private Vector3 childStartLocalPos;
    private Quaternion childStartLocalRot;
    private Vector3 childStartLocalScale;

    private void Awake()
    {
        if (visualChild != null)
        {
            childStartLocalPos = visualChild.localPosition;
            childStartLocalRot = visualChild.localRotation;
            childStartLocalScale = visualChild.localScale;
        }

        ResetPlatform(0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < ignoreUntilTime) return;
        if (played) return;

        var rb = other.attachedRigidbody;
        if (rb == null) return;
        if (!rb.CompareTag(activatingTag)) return;

        played = true;

        if (animator != null)
            animator.SetTrigger(triggerName);
    }

    public void ResetPlatform(float ignoreSeconds = 0.2f)
    {
        played = false;
        ignoreUntilTime = Time.time + ignoreSeconds;

        if (visualChild != null)
        {
            visualChild.localPosition = childStartLocalPos;
            visualChild.localRotation = childStartLocalRot;
            visualChild.localScale = childStartLocalScale;
        }

        if (animator != null)
        {
            animator.ResetTrigger(triggerName);
            animator.Rebind();
            animator.Update(0f);
            animator.Play(idleStateName, 0, 0f);
            animator.Update(0f);
        }
    }
}