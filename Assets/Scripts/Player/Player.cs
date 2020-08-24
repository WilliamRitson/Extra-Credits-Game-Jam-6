using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Mana mana;
    public float speed = 1;
    public Transform castPoint;
    private AudioSource audioSource;
    public Transform[] targetPoints;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
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
        audioSource.clip = spell.soundEffect;
        audioSource.Play();
        mana.currentMana -= spell.manaCost;
        StartCoroutine(spell.Cast(GetClosestTargetPoint(castPoint.position.y)));
    }
    
    private Transform GetClosestTargetPoint(float positionY)
    {
        float lowestDistance = float.PositiveInfinity;
        Transform best = null;
        foreach (var point in targetPoints)
        {
            var dist = Math.Abs(point.position.y - positionY);
            if (dist >= lowestDistance) continue;
            lowestDistance = dist;
            best = point;
        }
        return best;
    }



}
