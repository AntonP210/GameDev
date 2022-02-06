using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    private ScoreScript scoreScript;
    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public GameObject pauseUI;
    public GameObject PlayerInstance;
    private PlayerCollision playerCollision; 
    private bool freezed = false;


    private void Start()
    {
        scoreScript = FindObjectOfType<ScoreScript>();
        ScoreScript.LoadScore(); ///TEST PURPOSE
        EnableCollider();
    }
    public void CompleteLevel()
    {
        Debug.Log("Level WON!");
        scoreScript.UpdateHighScore();
        UIControl(completeLevelUI, true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            try
            {
                SoundManager.instance.PlayGameOverSound();
            }
            catch
            {
                Debug.Log("You are playing not from the menu");
            }

            UIControl(gameOverUI, true);
            scoreScript.UpdateHighScore();
            Debug.Log("GAME OVER");
        }
    }
    public void FreezeGame()
    {

        Time.timeScale = 0;
        PlayerInstance.GetComponent<Movement>().enabled=false; 
        //Disable scripts that still work while timescale is set to 
    }
    public void UnFreezeGame()
    {
        UIControl(pauseUI, false);
        if (freezed)
        {
            freezed = !freezed;
        }
        Time.timeScale = 1;
        PlayerInstance.GetComponent<Movement>().enabled=true; 
    }
    public void PauseGame()
    {
        UIControl(pauseUI, true);
        if (!freezed)
        {
            freezed = !freezed;
            FreezeGame();
        }
        Debug.Log("Game Paused");
    }
    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        ScoreScript.scoreValue = 0;
    }
    public void EnableCollider(){
    PlayerInstance.GetComponent<Collider>().enabled = false;
    }
    public void UIControl(GameObject desiredUi, bool desiredState)
    {
        if (desiredState)
        {
            desiredUi.SetActive(desiredState);
        }
        else
        {
            desiredUi.SetActive(desiredState);
        }
    }
}
