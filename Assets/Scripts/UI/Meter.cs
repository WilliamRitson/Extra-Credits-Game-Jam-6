﻿using System;
using System.Collections;
using System.Collections.Generic;
 using TMPro;
 using UnityEngine;
using UnityEngine.UI;

public class Meter : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private TextMeshProUGUI readout;
    private Damageable dmg;
    private float max;

    internal void ConnectDamageable(Damageable health)
    {
        if (dmg != null) 
            DisconnectDamageable(dmg);
        dmg = health;
        health.OnHealthChange += SetHealth;
        max = health.MaximumLife;
        SetHealth(health.CurrentLife);
    }

    internal void DisconnectDamageable(Damageable health)
    {
        health.OnHealthChange -= SetHealth;
    }

    private void OnDestroy()
    {
        if (dmg != null) 
            DisconnectDamageable(dmg);
    }


    protected virtual void SetHealth(int newValue)
    {
        readout.text = newValue + "/" + (int) max;
        fill.fillAmount = newValue / max ;
    }
}
