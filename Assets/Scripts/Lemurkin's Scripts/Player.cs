using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Transform position;
    public Mana mana;
    public Sprite playerSprite;
    public float speed = 1;
    public Spell[] spells = new Spell[6];
    public Button buttonPrefab;


    public void Update()
    {

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) )&& transform.position.y <=3.5)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up, speed/100);
        }

        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && transform.position.y >= -3.5)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down, speed/100);
        }
    }

    public bool canCastSpell(Spell spell)
    {
        return (mana.currentMana >= spell.manaCost);
    }

    public void AttemptToCastSpell(Spell spell)
    {

        if (mana.currentMana >= spell.manaCost)
        {
            mana.currentMana -= spell.manaCost;
            spell.Cast(position);
        }

    }

    public void Awake()
    {

        //Create existing spells
        for (int i = 0; i < spells.Length; i++)
        {
            createSpell(spells[i]);
        }
    }

    private void createSpell(Spell spell)
    {

    }

}
