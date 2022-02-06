using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;



    void Update()
    {
        Vector3 position = transform.position;
        position.z = (player.position + offset).z;
        transform.position = position;
    }


}
