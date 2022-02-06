using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSpeed : MonoBehaviour
{
    [Range(20,40)]
    public float maxspeed = 30f;

        private Rigidbody localRgb;

    private void Start()
    {
        localRgb = GetComponent < Rigidbody>();

    }

    private void FixedUpdate()
    {

        if (localRgb.velocity.magnitude > maxspeed)

            localRgb.velocity = Vector3.ClampMagnitude(localRgb.velocity, maxspeed);
    }
}





