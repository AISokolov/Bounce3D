using UnityEngine;
using System.Collections;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private GameObject endGameOverlay;
    [SerializeField] private float delay = 2f;

    [Header("Overlay")]
    [SerializeField] private GameObject overlay;

    [Header("Timer")]
    [SerializeField] private GameTimer timer;

    [Header("Stars")]
    [SerializeField] private StarsManager starsUI;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        BallController ball = other.GetComponent<BallController>();
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (ball != null)
                ball.controllerEnabled = false;

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            timer.StopTimer();

            StartCoroutine(ShowEndGame());
        }
    }

    public void ResetTrigger()
    {
        triggered = false;
    }

    private IEnumerator ShowEndGame()
    {
        float finishTime = timer.GetTime();

        yield return new WaitForSeconds(delay);

        endGameOverlay.SetActive(true);
        overlay.SetActive(true);

        starsUI.ShowStarsForTime(finishTime);

        Time.timeScale = 0f;
    }
}