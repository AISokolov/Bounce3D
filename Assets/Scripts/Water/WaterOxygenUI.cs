using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaterOxygenUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image oxygenImage;
    [SerializeField] private Sprite[] stages;

    [Header("Timing")]
    [SerializeField] private float secondsPerStage = 3f;

    [Header("Kill")]
    [SerializeField] private KillPlayer killPlayer;

    private Coroutine routine;
    private int index;
    private GameObject player;

    private void Awake()
    {
        oxygenImage.enabled = false;
    }

    public void StartOxygen(GameObject playerObj)
    {
        player = playerObj;

        StopOxygen();

        index = 0;
        oxygenImage.enabled = true;
        oxygenImage.sprite = stages[index];

        routine = StartCoroutine(OxygenRoutine());
    }

    public void StopOxygen()
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }

        oxygenImage.enabled = false;
        index = 0;
    }

    private IEnumerator OxygenRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsPerStage);

            index++;

            if (index >= stages.Length)
            {
                oxygenImage.sprite = stages[stages.Length - 1];

                if (killPlayer != null && player != null)
                    killPlayer.Kill(player);

                yield break;
            }

            oxygenImage.sprite = stages[index];
        }
    }
}
