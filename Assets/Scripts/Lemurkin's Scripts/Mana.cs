using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public float currentMana = 0;
    public float manaCapacity = 10;
    public float manaRegenration = 1;


    // Start is called before the first frame update
    void Start()
    {
        currentMana = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMana <= manaCapacity)
        {
            currentMana += Time.deltaTime * manaRegenration;
        }
        else
        {
            currentMana = manaCapacity;
        }
        outputText.text = "Mana: " + (int)currentMana;
    }

}
