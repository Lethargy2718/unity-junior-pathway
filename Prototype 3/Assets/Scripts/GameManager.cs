using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        GameEvents.OnGameOver?.Invoke();
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