using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float initialSpeed = 25.0f;
    public float maxSpeed = 40.0f;
    public float scale = 20.0f;
    public float gain = 20.0f;
    public float gravityModifier = 15f;

    public float Speed =>
        Mathf.Clamp(
        initialSpeed + Mathf.Log10(Time.timeSinceLevelLoad / scale + 1f) * gain,
        initialSpeed,
        maxSpeed
    );

    private void Start()
    {
        Instance = this;
        Physics.gravity *= gravityModifier;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        GameEvents.OnGameOver?.Invoke();
        StartCoroutine(DieCoroutine());

        IEnumerator DieCoroutine()
        {
            yield return new WaitForSeconds(2.0f);
            Restart();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Physics.gravity /= gravityModifier;
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