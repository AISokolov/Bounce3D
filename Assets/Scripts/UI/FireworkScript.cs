using UnityEngine;

public class FireworkScript : MonoBehaviour
{
    private ParticleSystem firework;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasPlayed)
                return;
            if (firework == null)
                firework = GetComponentInChildren<ParticleSystem>();
            if (firework != null)
            {
                hasPlayed = true;
                firework.Play();
            }
        }
    }

    public void ResetFirework()
    {
        hasPlayed = false;

        if (firework == null)
            firework = GetComponentInChildren<ParticleSystem>();

        if (firework != null)
            firework.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

}
