using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Rand = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public List<GameObject> targets;

    private int score;
    public int Score
    {
        get => score;
        set {
            score = value;
            scoreText.text = "Score: " + score;
        }
    }

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject gameScreen;
    public GameObject gameoverScreen;
    public GameObject pauseScreen;

    [Header("Gameplay")]
    public float spawnRate = 1.0f;
    public int startLives = 3;

    private int lives;
    public int Lives
    {
        get => lives;
        set
        {
            lives = Math.Max(0, value);
            livesText.text = "Lives: " + Lives;
            if (lives == 0)
            {
                GameOver();
            }
        }
    }

    private Coroutine spawnCoroutine;

    [HideInInspector] public bool isGameActive = false;
    [HideInInspector] public bool isPaused = false;
    [HideInInspector] public bool CanClick => Instance.isGameActive && !Instance.isPaused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame(int difficulty)
    {
        Score = 0;
        Lives = startLives;

        spawnCoroutine = StartCoroutine(SpawnCoroutine(difficulty));

        titleScreen.SetActive(false);
        gameScreen.SetActive(true);

        isGameActive = true;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameoverScreen.SetActive(true);


        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseScreen.SetActive(isPaused);
    }

    private void Update()
    {
        if (isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }

    private IEnumerator SpawnCoroutine(int difficulty)
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRate / difficulty);
            int randIdx = Rand.Range(0, targets.Count);
            Instantiate(targets[randIdx]);
        }
    }
}