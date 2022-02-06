using UnityEngine.SceneManagement;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Aplication Closed.");
        Application.Quit();
    }
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
