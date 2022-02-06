using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : MonoBehaviour
{
    public GameObject face, hpBar;

    // Face  is the gameobject in the scene that the hpBar object will always face 
    //there is not special settings about it , its in the prefabs/characters.
    void Update()
    {
        hpBar.transform.LookAt(face.transform);
        hpBar.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
