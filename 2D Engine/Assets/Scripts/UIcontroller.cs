using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIcontroller : MonoBehaviour
{

    public GameObject healthBar;
    public GameObject healthPoints;

    [SerializeField]public bool BarOrPoints;

    private void Start()
    {

        //Bar = True
        //Points = False
        if (!BarOrPoints)
        {
            healthBar.SetActive(false);
            healthPoints.SetActive(true);
        }
        else
        {
            healthBar.SetActive(true);
            healthPoints.SetActive(false);
        }
    }

    
}
