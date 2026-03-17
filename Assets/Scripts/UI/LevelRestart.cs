using UnityEngine;

public class LevelRestart : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject endGameOverlay;
    [SerializeField] private GameObject darkOverlay;

    [Header("Gameplay")]
    [SerializeField] private CheckpointManager checkpointManager;
    [SerializeField] private GameTimer timer;
    [SerializeField] private StarsManager starsManager;
    [SerializeField] private EndGameTrigger endGameTrigger;
    [SerializeField] private FireworkScript fireworkScript;

    [Header("Resettable Animation Triggers")]
    [SerializeField] private PlayOneTimeAnimationOnTrigger[] animationsToReset;
    [Header("Resettable Sound Triggers")]
    [SerializeField] private PlaySoundOnTrigger[] soundsToReset;

    public void RestartLevel()
    {
        Time.timeScale = 1f;

        if (endGameOverlay != null)
            endGameOverlay.SetActive(false);

        if (darkOverlay != null)
            darkOverlay.SetActive(false);

        if (starsManager != null)
            starsManager.HideAllStars();

        if (timer != null)
        {
            timer.ResetTimer();
            timer.StartTimer();
        }

        if (endGameTrigger != null)
            endGameTrigger.ResetTrigger();

        if (fireworkScript != null)
            fireworkScript.ResetFirework();

        if (checkpointManager != null)
            checkpointManager.RestartCheckppoints();

        if (soundsToReset != null)
        {
            foreach (var sound in soundsToReset)
            {
                if (sound != null)
                    sound.ResetSound();
            }
        }

        if (animationsToReset != null)
        {
            foreach (var anim in animationsToReset)
            {
                if (anim != null)
                    anim.ResetTriggerState(1f);
            }
        }
    }
}
