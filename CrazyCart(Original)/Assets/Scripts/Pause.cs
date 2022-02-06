using UnityEngine;
using UnityEngine.SceneManagement;



public class Pause : MonoBehaviour
{
    public GameManager gameManager;

    public void PauseLevel()
    {
        gameManager.PauseGame();
    }
    public void UnPauseGame()
    {

        gameManager.UnFreezeGame();
    }
    public void Quit()
    {
        Debug.Log("Aplication Closed.");
        Application.Quit();
    }
    public void Restart(){
        GameManager.Restart();
        UnPauseGame();
    }

    public void Settings(){
        Debug.Log("Settings menu");

    }
    public void MainMenu(){
        SceneManager.LoadScene("Menu");

    }
}