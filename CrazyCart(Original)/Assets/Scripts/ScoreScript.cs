using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    public static int scoreValue = 0;

    public TextMeshProUGUI score,gameOverScore,FinishScore;
    private int HighScore;





    void Start()
    {
        UpdateUi(PlayerPrefs.GetInt("High Score"));

    }

    // Update is called once per frame
    void Update()
    {
        score.SetText("" + scoreValue);
        
    }
    public void UpdateHighScore()
    {
        if (scoreValue > PlayerPrefs.GetInt("High Score"))
        {

            SaveScore();

            Debug.Log("Score Updated,new score" + scoreValue);

        }

    }
    
    void SaveScore()
    {
        PlayerPrefs.SetInt("High Score", scoreValue);
    }
    void UpdateUi(int value){
        gameOverScore.SetText("High Score: "+value);
        FinishScore.SetText("High Score: "+value);
    }
    public static void LoadScore()
    {
        int hScore=PlayerPrefs.GetInt("High Score");
        Debug.Log("High Score "+hScore);
        
        
    }
}
