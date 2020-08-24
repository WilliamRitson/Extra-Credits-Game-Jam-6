using System;
using UnityEngine;
using UnityEngine.UI;


public class SetDifficultyBehavior : MonoBehaviour
{
    public Difficulty difficulty;
    private Image bg;

    private void Start()
    {
        bg = GetComponent<Image>();
    }

    private void Update()
    {
        bg.color = (Options.difficulty == difficulty) ? Color.grey : Color.white;
    }

    public void OnClick()
    {
        Debug.Log(difficulty);
        Options.difficulty = difficulty;
    }
    
}
