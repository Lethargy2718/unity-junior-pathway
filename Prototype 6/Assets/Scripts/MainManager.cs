using System.Security.Policy;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    private string path;

    public Color color;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        path = Application.persistentDataPath + "/savefile.json";
        LoadColor();
    }

    [System.Serializable]
    private class SaveData
    {
        public Color color;
    }

    public void SaveColor()
    {
        SaveData saveData = new()
        {
            color = color
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);
    }

    public void LoadColor()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData loadData = JsonUtility.FromJson<SaveData>(json);
            color = loadData.color;
        }
    }
}