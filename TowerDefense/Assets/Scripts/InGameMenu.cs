using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public GameObject pauseUI, winUI, loseUI, settingsUI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle(pauseUI);
        }
    }
    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        UpdateDb(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Restart()
    {
        //reset values of the game
        Debug.Log("Level Restarted");
        Toggle(pauseUI);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Settings()
    {
        Debug.Log("settings");
        Toggle(settingsUI);
    }
    public void Toggle(GameObject ui)
    {
        ui.SetActive(!ui.activeSelf);
        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void UnFreeze()
    {
        Time.timeScale = 1f;
    }
    public void Exit()
    {
       SceneManager.LoadScene("MainMenu");
    }

    public void UpdateDb(int level)
    {
        DataBaseManager.instance.PostProgressToDataBase(level);
        Debug.Log("Data Saved, level unlocked: " + level);
    }
}
