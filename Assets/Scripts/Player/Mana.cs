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
