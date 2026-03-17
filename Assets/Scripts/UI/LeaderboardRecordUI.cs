using TMPro;
using UnityEngine;

public class LeaderboardRecordUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text starsText;

    public void Setup(string playerName, float timeValue, int starsValue)
    {
        nameText.text = playerName;

        int minutes = Mathf.FloorToInt(timeValue / 60f);
        int seconds = Mathf.FloorToInt(timeValue % 60f);
        timeText.text = $"{minutes:00}:{seconds:00}";

        starsText.text = $"{starsValue}/3";
    }
}