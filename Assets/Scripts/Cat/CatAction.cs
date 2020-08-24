using UnityEngine;
using System.Collections;

public enum ActionFrequency
{
    Common,
    Rare,
    Never
}

public abstract class CatAction : MonoBehaviour
{
    public ActionFrequency frequency = ActionFrequency.Common;
    public string abilityTitle;
    public AudioClip soundEffect;
    public Sprite intentionIcon;
    public abstract IEnumerator Perform(Transform target, float speed);
}