using UnityEngine;

public class PlayOneTimeAnimationOnTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName = "Activate";
    [SerializeField] private string idleStateName = "Idle";
    [SerializeField] private string activatingTag = "Player";

    private bool played;
    private float ignoreUntilTime;

    private void Awake()
    {
        ResetTriggerState(0f);
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

    public void ResetTriggerState(float ignoreSeconds = 0.25f)
    {
        played = false;
        ignoreUntilTime = Time.time + ignoreSeconds;

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