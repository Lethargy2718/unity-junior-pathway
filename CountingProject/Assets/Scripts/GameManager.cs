using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Elements")]
    public Text ScoreText;
    public Text SuccessfulShotsText;
    public Text MissedShotsText;
    public Text MissedTargetsText;
    public Text AccuracyText;
    public Text AvgLifeTimeText;
    public Text SuccessRateText;
    public GameObject PauseScreen;

    [Header("Game Settings")]
    public int penaltyPerMissedTarget = -5;

    public int successfulShots;
    public int missedShots;
    public int missedTargets;
    public float avgLifeTime;

    public int Score => successfulShots + penaltyPerMissedTarget * missedTargets;

    private int TotalShots => successfulShots + missedShots;
    private int TargetCount => successfulShots + missedTargets;
    public float SuccessRate => TargetCount > 0 ? (float)successfulShots / TargetCount : 0f;
    public float Accuracy => TotalShots > 0 ? (float)successfulShots / TotalShots : 0f;

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ResetStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TogglePause();
        }
    }

    private void TogglePause(bool? state = null)
    {
        state ??= !isPaused;

        isPaused = (bool)state;
        Time.timeScale = isPaused ? 0 : 1;
        //pauseScreen.SetActive(isPaused);
    }

    public void ResetStats()
    {
        successfulShots = 0;
        missedShots = 0;
        missedTargets = 0;
        UpdateUI();
    }

    public void UpdateUI()
    {
        ScoreText.text = "Score: " + Score;
        SuccessfulShotsText.text = "Successful Shots: " + successfulShots;
        MissedShotsText.text = "Missed Shots: " + missedShots;
        MissedTargetsText.text = "Missed Targets: " + missedTargets;
        AccuracyText.text = "Accuracy: " + (Accuracy * 100f).ToString("F1") + "%";
        AvgLifeTimeText.text = "Average Target Life Time: " + avgLifeTime.ToString("F2");
        SuccessRateText.text = "Success Rate: " + (SuccessRate * 100f).ToString("F1") + "%";
    }

    public void UpdateAvgLifeTime(float time)
    {
        avgLifeTime += (time - avgLifeTime) / TargetCount;
    }
}

