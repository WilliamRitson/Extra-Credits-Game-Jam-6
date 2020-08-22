using UnityEngine;
using System.Collections;

public abstract class CatAction : MonoBehaviour
{
    public AudioClip soundEffect;
    public abstract IEnumerator Perform(Transform target);
}