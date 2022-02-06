using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }



}
