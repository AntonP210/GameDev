using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Range(-1, 1)] public float stepSize;
    public Rigidbody rigidBody;

    [Range(100, 1000)]
    public float force = 500f;

    public bool isMovementEnabled = true;
    public GameObject cartBody, leftHolder, rightHolder;
    public Swipe swipeControls;




    void Update()
    {
        transform.position = new Vector3(stepSize, transform.position.y, transform.position.z);

        AddForce(isMovementEnabled);

        if (swipeControls.SwipeRight || Input.GetKeyDown("d"))
        {

            if (stepSize == 1)
            {
                return;
            }
            else
            {
                stepSize += 1;



                rightHolder.GetComponent<Animator>().SetTrigger("Trigger");

                try
                {
                    SoundManager.instance.PlaySteerSound();
                }
                catch
                {
                    Debug.Log("You are playing not from the menu");
                }

                cartBody.GetComponent<Animator>().SetTrigger("SteerRight");
            }

        }
        if (swipeControls.SwipeLeft || Input.GetKeyDown("a"))
        {


            //Debug.Log("move");

            if (stepSize == -1)
            {
                return;
            }
            else
            {
                stepSize += -1;

                leftHolder.GetComponent<Animator>().SetTrigger("Trigger");

                try
                {
                    SoundManager.instance.PlaySteerSound();
                }
                catch
                {
                    Debug.Log("You are playing not from the menu");
                }

                cartBody.GetComponent<Animator>().SetTrigger("SteerLeft");
            }
        }
    }

    public void AddForce(bool state)
    {

        if (state)
        {
            rigidBody.AddForce(0, 0, force * Time.deltaTime);
        }
        else
        {
            return;
        }
    }

}
