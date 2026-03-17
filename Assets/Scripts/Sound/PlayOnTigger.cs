using UnityEngine;

public class PlayOnTrigger : MonoBehaviour
{
    public AudioSource source;
    public AudioClip sound;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        source.PlayOneShot(sound);
    }
}