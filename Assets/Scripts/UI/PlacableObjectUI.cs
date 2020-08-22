using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableObjectUI : MonoBehaviour
{
    public Meter meter;
    public Damageable damagable;

    private void Start()
    {
        meter.ConnectDamageable(damagable);
    }
}
