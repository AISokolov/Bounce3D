using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var cm = other.GetComponentInParent<CheckpointManager>();
        if (cm == null) return;

        cm.SetCheckpoint(transform);
    }
}