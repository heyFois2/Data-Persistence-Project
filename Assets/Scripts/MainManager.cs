using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    
    public int LineCount = 1;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text LvlText;
    public GameObject GameCompletedText;
    public GameObject GameOverText;
    public GameObject YouWinText;
    public Button MenuButton;
    public Text BestScoreText;
    
    private bool m_Started = false;
    public int m_Points;
    
    private bool m_GameOver = false;




    
    // Start is called before the first frame update
    void Start()
    {
        //If you pass to the next level... Score is saved for the current game and the game can restart in a new level with a line of bricks in plus 
        if(DataManager.Instance.LevelUp > 0)
        {
            YouWinText.SetActive(true);
            LineCount = DataManager.Instance.LevelUp;
            m_Points = DataManager.Instance.ScoreAfterLevelUp;
            ScoreText.text = "Score : " + m_Points;
        }
        if (DataManager.Instance != null)
        {
            BestScoreText.text  = ("Best Score : " + DataManager.Instance.PlayerName + " : " + DataManager.Instance.BestScore);
        }
        GameCompletedText.SetActive(false);
        LvlText.text = "Lvl : " + LineCount;
    

        MenuButton.gameObject.SetActive(false);
        
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        
  
            
                if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                YouWinText.SetActive(false);
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }   
        }
                         if (m_GameOver)
                {
                    LineCount = 1;
                    DataManager.Instance.LevelUp = 0;
                    MenuButton.gameObject.SetActive(true);
                    DataManager.Instance.UpdateBestScore(m_Points);
                    DataManager.Instance.LoadPlayerData();
                    BestScoreText.text = ("Best Score : " + DataManager.Instance.PlayerName + " : " + DataManager.Instance.BestScore);

                }
                else if (LineCount > 5)
                {

                     if (GameObject.FindGameObjectsWithTag("Brick").Length == 0)
                        {
                            m_GameOver = true;
                            GameCompletedText.SetActive(true);
                            Destroy(Ball.gameObject);
                        }
                }
                else if (LineCount <= 5)
                    {
                        if (GameObject.FindGameObjectsWithTag("Brick").Length == 0)
                        {
                        DataManager.Instance.UpdateBestScore(m_Points);
                        DataManager.Instance.ScoreAfterLevelUp = m_Points;
                        DataManager.Instance.LevelUp = LineCount + 1;
                                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }

                    }


            
        
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        Destroy(Ball.gameObject);
        m_GameOver = true;
        DataManager.Instance.UpdateBestScore(m_Points);
        
    }


}
