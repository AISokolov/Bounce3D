using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderboardManager : MonoBehaviour
{
    [System.Serializable]
    public class RecordData
    {
        public string playerName;
        public int starsValue;
        public float timeValue;
    }

    [System.Serializable]
    public class ScoreSubmission
    {
        public string player_name;
        public float time_seconds;
        public int stars;
    }

    [System.Serializable]
    public class LeaderboardResponse
    {
        public List<LeaderboardEntry> entries;
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string player_name;
        public float time_seconds;
        public int stars;
    }

    [Header("UI References")]
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Transform leaderboardContent;
    [SerializeField] private LeaderboardRecordUI recordPrefab;

    [Header("Gameplay References")]
    [SerializeField] private GameTimer timer;
    [SerializeField] private StarsManager currentStarsValue;

    [Header("Server")]
    [SerializeField] private string serverBaseUrl = "http://91.228.153.55:8000";

    private const int MaxRecords = 10;
    private readonly List<RecordData> records = new List<RecordData>();

    private void Start()
    {
        LoadLeaderboardFromServer();
    }

    public void SaveCurrentRecord()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
            playerName = "Player";

        float currentTime = timer != null ? timer.GetTime() : 0f;
        int stars = currentStarsValue != null ? currentStarsValue.GetStarsFromTime(currentTime) : 0;

        StartCoroutine(SubmitScoreCoroutine(playerName, currentTime, stars));
    }

    public void LoadLeaderboardFromServer()
    {
        StartCoroutine(GetLeaderboardCoroutine());
    }

    private IEnumerator SubmitScoreCoroutine(string playerName, float timeValue, int starsValue)
    {
        ScoreSubmission score = new ScoreSubmission
        {
            player_name = playerName,
            time_seconds = timeValue,
            stars = starsValue
        };

        string json = JsonUtility.ToJson(score);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(serverBaseUrl + "/submit-score", "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            Debug.Log("GET URL: " + serverBaseUrl + "/leaderboard");
            Debug.Log("POST URL: " + serverBaseUrl + "/submit-score");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Score submitted successfully.");
                nameInputField.text = "";
                LoadLeaderboardFromServer();
            }
            else
            {
                Debug.LogError("Submit score failed: " + request.error);
                Debug.LogError("Server response: " + request.downloadHandler.text);
            }
        }
    }

    private IEnumerator GetLeaderboardCoroutine()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(serverBaseUrl + "/leaderboard"))
        {
            yield return request.SendWebRequest();

            Debug.Log("GET URL: " + serverBaseUrl + "/leaderboard");
            Debug.Log("POST URL: " + serverBaseUrl + "/submit-score");

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;

                LeaderboardResponse response = JsonUtility.FromJson<LeaderboardResponse>(json);

                records.Clear();

                if (response != null && response.entries != null)
                {
                    for (int i = 0; i < response.entries.Count && i < MaxRecords; i++)
                    {
                        LeaderboardEntry entry = response.entries[i];
                        records.Add(new RecordData
                        {
                            playerName = entry.player_name,
                            timeValue = entry.time_seconds,
                            starsValue = entry.stars
                        });
                    }
                }

                RefreshUI();
            }
            else
            {
                Debug.LogError("Get leaderboard failed: " + request.error);
                Debug.LogError("Server response: " + request.downloadHandler.text);
            }
        }
    }

    private void RefreshUI()
    {
        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }

        foreach (RecordData record in records)
        {
            LeaderboardRecordUI row = Instantiate(recordPrefab, leaderboardContent);
            row.Setup(record.playerName, record.timeValue, record.starsValue);
        }
    }
}