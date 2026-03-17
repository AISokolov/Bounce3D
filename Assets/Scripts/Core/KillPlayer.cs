using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public void Kill(GameObject player)
    {
        var cm = player.GetComponent<CheckpointManager>();
        if (cm == null) cm = player.GetComponentInParent<CheckpointManager>();
        if (cm == null) cm = player.GetComponentInChildren<CheckpointManager>();

        if (cm != null)
        {
            cm.RespawnPlayer();
        }
        else
        {
            Debug.LogError("KillPlayer: No CheckpointManager found on player.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Kill(other.gameObject);
    }
}