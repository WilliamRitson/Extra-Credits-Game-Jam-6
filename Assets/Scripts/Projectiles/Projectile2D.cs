using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile2D : MonoBehaviour
{
    public int damage;
    public Element damageType;
    public int stunDuration;
    public int hyperDuration;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (!damageable) return;
        damageable.TakeDamage(damage, damageType);
        Destroy(gameObject);
    }
    

}
