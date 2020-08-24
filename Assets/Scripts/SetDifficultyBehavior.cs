using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SetDifficultyBehavior : MonoBehaviour
{
    private Image bg;
    public Button button;
    public Sprite[] difficultySprites;



    private void Start()
    {
        bg = GetComponent<Image>();
        switch (Options.difficulty)
        {
            case Difficulty.Easy:
                button.GetComponent<Image>().sprite = difficultySprites[0];
                break;
            case Difficulty.Medium:
                button.GetComponent<Image>().sprite = difficultySprites[1];
                break;
            case Difficulty.Hard:
                button.GetComponent<Image>().sprite = difficultySprites[2];
                break;
            case Difficulty.Hyper:
                button.GetComponent<Image>().sprite = difficultySprites[3];
                break;
        }
    }

    public void ChangeDifficulty()
    {


        switch (Options.difficulty)
        {
            case Difficulty.Easy:
                button.GetComponent<Image>().sprite = difficultySprites[1];
                Options.difficulty = Difficulty.Medium;
                break;
            case Difficulty.Medium:
                button.GetComponent<Image>().sprite = difficultySprites[2];
                Options.difficulty = Difficulty.Hard;
                break;
            case Difficulty.Hard:
                button.GetComponent<Image>().sprite = difficultySprites[3];
                Options.difficulty = Difficulty.Hyper;
                break;
            case Difficulty.Hyper:
                button.GetComponent<Image>().sprite = difficultySprites[0];
                Options.difficulty = Difficulty.Easy;
                break;
        }
        Debug.Log(Options.difficulty);
    }
    
}
