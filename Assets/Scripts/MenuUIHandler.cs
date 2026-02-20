using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField PlayerNameField;
    public TextMeshProUGUI ScoreText;

    public void Start()
    {
        
        DataManager.Instance.LoadPlayerData();
        ScoreText.text = (" Best Score : " + DataManager.Instance.PlayerName + " : " + DataManager.Instance.BestScore);
    }

    public void StarNew()
    {
        DataManager.Instance.NewPlayerName = PlayerNameField.text;
       // DataManager.Instance.PlayerName = PlayerNameField.text;
        
            DataManager.Instance.SavePlayerData();
        
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void ExitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
