using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }

    public int maxLives = 3;

    private int lives;
    public int Lives
    {
        get => lives;
        set
        {
            lives = Math.Max(value, 0);
            livesText.text = "Lives: " + lives.ToString();
            if (lives == 0)
            {
                Die();
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        Lives = maxLives;
    }

    private void Die()
    {
        Time.timeScale = 0;
        StartCoroutine(DieCoroutine());

        IEnumerator DieCoroutine()
        {
            yield return new WaitForSecondsRealtime(2.0f);
            Restart();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}