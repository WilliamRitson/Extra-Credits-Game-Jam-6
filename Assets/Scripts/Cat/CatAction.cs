using UnityEngine;
using System.Collections;

public abstract class CatAction : MonoBehaviour
{
    public string abilityTitle;
    public AudioClip soundEffect;
    public abstract IEnumerator Perform(Transform target);
}