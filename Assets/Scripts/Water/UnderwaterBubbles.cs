using UnityEngine;

public class UnderwaterBubbles : MonoBehaviour
{
    [SerializeField] private ParticleSystem bubbles;

    void Awake()
    {
        if (bubbles != null)
            bubbles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void SetUnderwater(bool value)
    {
        if (bubbles == null) return;

        if (value) bubbles.Play();
        else bubbles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}
