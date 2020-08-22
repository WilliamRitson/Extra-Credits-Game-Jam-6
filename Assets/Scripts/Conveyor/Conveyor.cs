using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField]
    private Vector2 velocity = new Vector2(2f,0f);
    
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        rb.velocity = velocity;
        
    }

    //Stops conjured item from moving
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.transform.name == "CatZone")
        {
            rb.velocity = new Vector2(0f,0f);
        }
    }
}
