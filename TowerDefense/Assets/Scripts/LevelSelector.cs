using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    //public SceneFader fader;
    private int levelReached;
    public Button[] levelButtons;
    public DataBaseManager dataBaseManager;


    private void Start()
    {
        levelReached = dataBaseManager.GetPlayerProgress();



        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }

        }
    }
    public void Select(string levelName)
    {
       // fader.FadeTo(levelName);
       SceneManager.LoadScene(levelName);
    }
}
