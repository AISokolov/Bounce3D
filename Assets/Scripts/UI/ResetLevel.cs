using UnityEngine;

public class ResetLevel : MonoBehaviour
{
    public void OnResetPressed()
    {
        Time.timeScale = 1f;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("RESET: No object with tag 'Player' found!");
            return;
        }

        var cm = player.GetComponent<CheckpointManager>();
        if (cm == null) cm = player.GetComponentInParent<CheckpointManager>();
        if (cm == null) cm = player.GetComponentInChildren<CheckpointManager>();
        if (cm == null)
        {
            Debug.LogError("RESET: No CheckpointManager found on player.");
            return;
        }

        cm.RespawnPlayer();
    }
}