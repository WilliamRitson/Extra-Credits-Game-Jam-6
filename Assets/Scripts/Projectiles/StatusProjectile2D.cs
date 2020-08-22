using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StatusProjectile2D : MonoBehaviour
{
    public int duration;
    public Cat.State type;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Cat cat = other.gameObject.GetComponent<Cat>();
        if (!cat) return;
        cat.ApplyStatusEffect(duration, type);
        Destroy(gameObject);
    }


}
