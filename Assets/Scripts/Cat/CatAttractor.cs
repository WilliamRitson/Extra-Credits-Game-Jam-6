using System;
using UnityEngine;

public class CatAttractor : MonoBehaviour
{
    private Cat cat;
    public int strength = 1;

    private void Start()
    {
        cat = GameObject.FindWithTag("Cat").GetComponent<Cat>();
        cat.RegisterAttractor(transform.position.y, strength);
    }

    private void OnDestroy()
    {
        cat.DeregisterAttractor(transform.position.y, strength);
    }
}
