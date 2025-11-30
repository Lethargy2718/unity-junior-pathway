using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public List<GameObject> targets;
    public float spawnRate = 1.0f;

    private int score;
    public int Score
    {
        get => score;
        set {
            score = value;
            scoreText.text = "Score: " + score;
        }
    }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject gameScreen;
    public GameObject gameoverScreen;

    private Coroutine spawnCoroutine;

    [HideInInspector] public bool isGameActive = false;

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
        spawnCoroutine = StartCoroutine(SpawnCoroutine(difficulty));

        titleScreen.SetActive(false);
        gameoverScreen.SetActive(false);
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

    private IEnumerator SpawnCoroutine(int difficulty)
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRate / difficulty);
            int randIdx = Random.Range(0, targets.Count);
            Instantiate(targets[randIdx]);
        }
    }
}