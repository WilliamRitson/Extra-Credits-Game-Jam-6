﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InitialVelocity : MonoBehaviour
{
    public Vector2 initialVelocity;
    
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = initialVelocity;
    }


}
