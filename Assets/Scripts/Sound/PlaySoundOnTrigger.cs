using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip checkpointSound;

    private bool hasPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasPlayed) return;

        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(checkpointSound);

            hasPlayed = true;
        }
    }
    public void ResetSound()
    {
        hasPlayed = false;
    }
}