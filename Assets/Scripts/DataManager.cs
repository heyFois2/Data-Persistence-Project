using UnityEngine;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    public string PlayerName;
    public string NewPlayerName;
    public int BestScore;
    public static DataManager Instance;
    public TextMeshProUGUI BestScoreText;
    public int ScoreAfterLevelUp;
    public int LevelUp;

    //private int bestScore = 0;
   

     private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }


     // Ajoute cette ligne pour utiliser Text

[System.Serializable]
class SaveData
{    
    public string PlayerName;
    public int BestScore;

}


public void SavePlayerData()
{
    SaveData data = new SaveData();
      
   data.PlayerName = PlayerName;
   data.BestScore = BestScore;

    string json = JsonUtility.ToJson(data);
    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        
}

    public void LoadPlayerData()
    {
        
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json); 
            PlayerName = data.PlayerName;
            BestScore = data.BestScore;
        }
    }
             public void UpdateBestScore(int Score)
    {
        if (Score > BestScore)
        {
            PlayerName = NewPlayerName;
            BestScore = Score;
            SavePlayerData();
        }
    }
}