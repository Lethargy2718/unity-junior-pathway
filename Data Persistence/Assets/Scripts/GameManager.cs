using System.Security.Policy;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private string path;

    // Data
    public string playerName = "";
    public int bestScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/save.json";
        LoadBestScore();
    }

    [System.Serializable]
    private class SaveData
    {
        public int bestScore;
    }

    public void SaveBestScore()
    {
        SaveData saveData = new()
        {
            bestScore = bestScore,
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);
    }

    public void LoadBestScore()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData loadData = JsonUtility.FromJson<SaveData>(json);
            bestScore = loadData.bestScore;
        }
    }
}