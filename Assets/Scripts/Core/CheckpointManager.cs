using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private BallController ball;
    public static CheckpointManager Instance { get; private set; }
    public bool IsRespawning { get; private set; }

    [Header("Respawn")]
    public Vector3 respawnOffset = new Vector3(0f, 1f, 0f);

    private Rigidbody rb;

    private Vector3 startPos;
    private Quaternion startRot;

    private bool hasCheckpoint;
    private Vector3 checkpointPos;
    private Quaternion checkpointRot;

    [Header("Respawn Sound")]
    public AudioClip teleportSound;
    private AudioSource audioSource;


    public bool HasCheckpoint => hasCheckpoint;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();

        startPos = transform.position;
        startRot = transform.rotation;
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        hasCheckpoint = true;
        checkpointPos = checkpoint.position;
        checkpointRot = checkpoint.rotation;

        Debug.Log($"Checkpoint saved: {checkpointPos}");
    }

    public void RespawnPlayer()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (audioSource != null && teleportSound != null)
            audioSource.PlayOneShot(teleportSound);

        if (hasCheckpoint)
            transform.SetPositionAndRotation(checkpointPos + respawnOffset, checkpointRot);
        else
            transform.SetPositionAndRotation(startPos, startRot);

        Physics.SyncTransforms();

        ResetLevelObjects();
        ResetControlls();
    }

    private void ResetLevelObjects()
    {
        FallingPlatform[] platforms = FindObjectsByType<FallingPlatform>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (FallingPlatform platform in platforms)
        {
            if (platform != null)
                platform.ResetPlatform();
        }
    }

    private void ResetControlls()
    {
        if (ball != null)
        {
            ball.controllerEnabled = true;
        }
    }

    public void ClearCheckpoint()
    {
        hasCheckpoint = false;
    }

    public void RestartCheckppoints()
    {
        ClearCheckpoint();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        transform.SetPositionAndRotation(startPos, startRot);

        Physics.SyncTransforms();

        ResetLevelObjects();
        ResetControlls();
    }

}