using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] pickups;
    public Vector3[] rowOflocations;
    //public Vector3 location = new Vector3(0f, 0.8f, 20f);
    public GameObject parentObject;

    // public int pos1State = 0; //1=obj ,0=empty
    // public int pos2State = 0; //1=obj ,0=empty
    // public int pos3State = 0; //1=obj ,0=empty

    void Start()
    {
        PickUpSpawner();
    }

    public void PickUpSpawner()
    {
        foreach (Vector3 location in rowOflocations){
            PickUpCreator(location);
        }
        
    }


    public void PickUpCreator(Vector3 location)
    {

        GameObject spawnedPickup = Instantiate(pickups[Random.Range(0, pickups.Length)], location, Quaternion.identity);

        spawnedPickup.transform.parent = parentObject.transform;

        spawnedPickup.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); //size of the object.
    }
    public void PickUpDef(string tagOfGameObject)
    {

        switch (tagOfGameObject)
        {
            case "PickupPineApple":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupBanana":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupBaguette":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupBaguetteDouble":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupBread":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupCake":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupCarrot":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupCereal":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupCheese":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupChips":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupChocolate":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupCleaner":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupCoke":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupCokeCan":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupCroissant":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupDonut":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupEggs":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupEnergyBar":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupFanta":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupFish":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupHamburger":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupKetchup":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupMilk":
                ScoreScript.scoreValue += 1;

                break;
            case "PickupPizza":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupSausage":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupSpritePack":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupSteak":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupToiletPaper":
                ScoreScript.scoreValue += 1;
                break;
            case "PickupWaterMelon":
                ScoreScript.scoreValue += 1;
                break;
            default:
                break;
        }
    }

    // IEnumerator ExecuteAfterTime(int posNumber)
    // {
    //     Vector3 pos1 = new Vector3(-0.039f, 3.134f, 0.0f);
    //     Vector3 pos2 = new Vector3(-2.129f, -0.344f, 0.0f);
    //     Vector3 pos3 = new Vector3(1.936f, 0.727f, 0.0f);


    //     yield return new WaitForSeconds(1.2f);

    //     //Code to execute after the delay


    //     if (posNumber == 1)
    //     {
    //         VeggieCreator(pos1, parentObject1);
    //         pos1State = 1;
    //     }
    //     if (posNumber == 2)
    //     {
    //         VeggieCreator(pos2, parentObject2);
    //         pos2State = 1;
    //     }
    //     if (posNumber == 3)
    //     {
    //         VeggieCreator(pos3, parentObject3);
    //         pos3State = 1;
    //     }

    // }
}
