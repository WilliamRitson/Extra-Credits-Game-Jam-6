using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Transform position;
    public Mana mana;
    public float speed = 1;
    public Transform castPoint;


    public void Update()
    {

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) )&& transform.position.y <= 2.5)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up, speed * Time.deltaTime);
        }

        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && transform.position.y >= -3.5)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down, speed * Time.deltaTime);
        }
    }

    public bool CanCastSpell(Spell spell)
    {
        return (mana.currentMana >= spell.manaCost && Time.timeScale > 0);
    }

    public void AttemptToCastSpell(Spell spell)
    {
        if (!CanCastSpell(spell)) return;
        MovingTextManager.Instance.ShowMessage(spell.spellName, transform.position, Color.white);
        mana.currentMana -= spell.manaCost;
        StartCoroutine(spell.Cast(castPoint));
    }


}
