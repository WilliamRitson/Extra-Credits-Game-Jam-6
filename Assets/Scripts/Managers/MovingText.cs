using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingText : MonoBehaviour
{
    public string text;
    public float speed;
    public Text uiText;
    public Vector2 direction;
    public float fadeTime;
    private float startTime;

    private void Start()
    {
        startTime = fadeTime;    
    }

    void Update()
    {
        fadeTime -= Time.deltaTime;
        transform.Translate(direction * speed * Time.deltaTime);
        var faded = uiText.color;
        faded.a = fadeTime / startTime;
        uiText.color = faded;
        if (fadeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Initilize(string text, float speed, Vector2 direction, Color color, float fadeTime)
    {
        this.speed = speed;
        this.direction = direction;
        uiText.text = text;
        uiText.color = color;
        this.fadeTime = fadeTime;
        startTime = fadeTime;

    }
}
