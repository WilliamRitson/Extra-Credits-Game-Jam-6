﻿using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile2D : MonoBehaviour
{
    public int damage;
    public Element damageType;
    public string friendTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (friendTag != null && other.CompareTag(friendTag)) return;
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (!damageable) return;
        damageable.TakeDamage(damage, damageType);
        Destroy(gameObject);
    }
    

}
