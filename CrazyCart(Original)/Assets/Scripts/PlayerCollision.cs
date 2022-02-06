using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Movement movement;
    public GameManager gameManager;
    public ObjectSpawner objectSpawner;
    public GameObject cartBody;
    public Rigidbody rigidbody;
    private GameObject childObject;

    
    private void OnCollisionEnter(Collision other) //works with everything 
    {
        if (other.collider.tag.Contains("Obstacle"))
        {
            movement.enabled = false;

            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            //Debug.Log("HIT");

            cartBody.GetComponent<Animator>().SetTrigger("ObsCollision");
            
            try
            {
                SoundManager.instance.PlayCollisionSound();
            }
            catch
            {
                Debug.Log("You are playing not from menu, cant detect sound player");
            }

            StartCoroutine(EndGameAfterTime(1f));

        }
        if (other.collider.tag.Contains("Finish"))
        {
            try
            {
                SoundManager.instance.PlayWinSound();
            }
            catch
            {
                Debug.Log("You are not playing from the menu");
            }

            movement.enabled = false;

            rigidbody.constraints = RigidbodyConstraints.FreezeAll;


            gameManager.CompleteLevel();

        }
        if (other.collider.tag.Contains("Puddle"))
        {

            movement.enabled = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            try
            {
                SoundManager.instance.PlaySpinSound();
            }
            catch
            {
                Debug.Log("you are playing not from the menu");
            }

            cartBody.GetComponent<Animator>().SetTrigger("SpinCollision");
            cartBody.GetComponent<Collider>().enabled=false;
            
            Debug.Log("Collide");
            
            StartCoroutine(EndGameAfterTime(1f));
        }


        if (other.collider.tag.Contains("Pickup"))
        {


            other.gameObject.GetComponentInChildren<Animator>().SetTrigger("PickUpTrigger");

            objectSpawner.PickUpDef(other.collider.tag);

            try
            {
                SoundManager.instance.PlayPickUpSound();
            }
            catch
            {
                Debug.Log("You are playing not from menu, cant detect sound player");
            }


            Destroy(other.gameObject, 0.00001f);

        }


    }
    IEnumerator EndGameAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        gameManager.EndGame();
    }

}
