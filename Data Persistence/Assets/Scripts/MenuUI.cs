using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI playerNameText;

    private void Start()
    {
        // render the best score in save file. get from singleton which gets from file. (session persistence)
        bestScoreText.text = "Best Score: " + GameManager.Instance.bestScore;
        GameManager.Instance.playerName = playerNameText.text;
    }

    public void SetName(string Name)
    {
        GameManager.Instance.playerName = Name;
    }

    public void StartGame()
    {
        if (string.IsNullOrWhiteSpace(GameManager.Instance.playerName)) return;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        GameManager.Instance.SaveBestScore();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


}
