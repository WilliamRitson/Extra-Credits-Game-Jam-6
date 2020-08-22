using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTextManager : MonoBehaviour
{
    private static MovingTextManager instance = null;

    public static MovingTextManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public GameObject movingTextPrefab;
    public float textSpeed = 1.0f;
    public float fadeTime = 1.25f;
    public void ShowMessage(string msg, Vector2 location, Color color)
    {
        location = new Vector2(
            location.x + Random.Range(-0.5f, 0.5f),
            location.y + Random.Range(-0.5f, 0.5f)
        );
        MovingText movingText = Instantiate(movingTextPrefab, location, Quaternion.identity).GetComponent<MovingText>();
        movingText.Initilize(msg, textSpeed, new Vector2(Random.Range(-0.2f, 0.2f), 1.0f), color, fadeTime);
    }

}
