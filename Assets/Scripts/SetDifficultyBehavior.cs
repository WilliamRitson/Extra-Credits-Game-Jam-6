using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SetDifficultyBehavior : MonoBehaviour
{
    private Image bg;
    public Text text;

    private void Start()
    {
        bg = GetComponent<Image>();
    }

    private void Update()
    {
        text.text = "Current Difficulty: " + Options.difficulty;
    }

    public void ChangeDifficulty()
    {


        switch (Options.difficulty)
        {
            case Difficulty.Easy:
                Options.difficulty = Difficulty.Medium;
                break;
            case Difficulty.Medium:
                Options.difficulty = Difficulty.Hard;
                break;
            case Difficulty.Hard:
                Options.difficulty = Difficulty.Hyper;
                break;
            case Difficulty.Hyper:
                Options.difficulty = Difficulty.Easy;
                break;
        }
        Debug.Log(Options.difficulty);
    }
    
}
