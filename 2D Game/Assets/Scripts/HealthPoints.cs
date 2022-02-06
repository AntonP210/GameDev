using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPoints : MonoBehaviour
{

    public Image[] Images;
    private int counter = 1;
    public int currentHealthPoints = 4;

    public void UpdateHealth(bool isDamaged) {

        
        if (isDamaged)
        Images[Images.Length - counter].enabled = false;
        currentHealthPoints--;
        counter++;
    }
}
