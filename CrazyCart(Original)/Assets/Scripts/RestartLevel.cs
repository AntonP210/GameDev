
using System.Configuration;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    

    public void RestartCurrentLevel() {
        GameManager.Restart();
    }
    public void Quit() {
        Debug.Log("Aplication Closed.");
        Application.Quit();
    }


   
    public void MainMenu()
    {
        Debug.Log("MainMenu");
        GameManager.Restart();
        SceneManager.LoadScene("Menu");

    }





}
