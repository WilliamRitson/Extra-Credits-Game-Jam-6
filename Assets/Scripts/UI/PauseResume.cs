using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseResume : MonoBehaviour
{
    public Image display;
    public Sprite unpausedImage;
    public Sprite pausedImage;
    
    public void OnClick()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        if (Time.timeScale != 0)
        {
            display.sprite = pausedImage;
            Time.timeScale = 0;
        }
        else
        {
            display.sprite = unpausedImage;
            Time.timeScale = 1;
        }
    }
    

}
