using UnityEngine;

public class PlayAnimationOnTrigger : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName = "Activate";

    [Header("Filter")]
    [SerializeField] private string activatingTag = "Player";

    private void Reset()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(activatingTag))
            return;

        if (animator != null)
        {
            animator.SetTrigger(triggerName);
            print("Animation played");
        }
    }
}
