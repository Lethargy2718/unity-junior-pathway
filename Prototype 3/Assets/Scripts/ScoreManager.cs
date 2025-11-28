using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        scoreText.text = "0";
        StartCoroutine(IncrementScoreCoroutine());
    }

    private IEnumerator IncrementScoreCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            IncrementScore();
        }
    }

    private void IncrementScore()
    {
        scoreText.text = (int.Parse(scoreText.text) + 1).ToString();
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += StopAllCoroutines;   
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= StopAllCoroutines;
    }
}