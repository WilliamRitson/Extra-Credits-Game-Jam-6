using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public float currentMana;
    public float manaCapacity = 10;
    public float manaRegeneration = 1;

    public void Start()
    {
        if (Options.difficulty == Difficulty.Easy )
        {
            manaRegeneration = 1.3f;
        }
        else if (Options.difficulty == Difficulty.Medium)
        {
            manaRegeneration = 1.3f;
        }
        else if (Options.difficulty == Difficulty.Hard)
        {
            manaRegeneration = 1;
        }
        else if (Options.difficulty == Difficulty.Hyper)
        {
            manaRegeneration = 3f;
        }
    }

    private void Update()
    {
        if (currentMana <= manaCapacity)
        {
            currentMana += Time.deltaTime * manaRegeneration;
        }
        else
        {
            currentMana = manaCapacity;
        }
        outputText.text = "Mana: " + (int)currentMana;
    }

}
