using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public int manaCost;
    public string spellName;
    public Sprite spellIcon;
    abstract public IEnumerator Cast(Transform target);
}
