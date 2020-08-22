using UnityEngine;
using System.Collections;

public abstract class CatAction : MonoBehaviour
{
    public abstract IEnumerator Perform(Transform target);
}