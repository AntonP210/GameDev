using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool gameEnded = false;
    public GameObject winMenu, loseMenu;
    public string nextLevel = "level02";
    public int levelToUnlock;
    public SceneFader sceneFader;

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            return;
        }
        if (PlayerStats.Health == 0)
        {
            EndGame();
        }
    }
    private void Start()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }
    private void EndGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;
        Debug.Log("Game Over!");
        //sceneFader.FadeTo(nextLevel);
        loseMenu.SetActive(true);
    }

    public void WinLevel()
    {
        Debug.Log("level won");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        //sceneFader.FadeTo(nextLevel);
        winMenu.SetActive(true);
    }
}
