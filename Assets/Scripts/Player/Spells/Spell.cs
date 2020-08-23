using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public int manaCost;
    public string spellName;
    [TextArea]
    public string tooltip;
    public Sprite spellIcon;
    public AudioClip soundEffect;
    abstract public IEnumerator Cast(Transform target);
}
