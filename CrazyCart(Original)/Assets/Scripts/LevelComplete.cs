using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public void LoadNextLevel()
    {
        Debug.Log("Level Loaded");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ScoreScript.scoreValue = 0;
    }
    public void ReplayLevel()
    {
        GameManager.Restart();
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        ScoreScript.scoreValue = 0;
    }
}
