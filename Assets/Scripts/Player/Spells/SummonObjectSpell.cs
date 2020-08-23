using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonObjectSpell : Spell
{
    public GameObject toSummon;
    public float castDelay = 0f;

    public override IEnumerator Cast(Transform target)
    {
        yield return new WaitForSeconds(castDelay);
        Instantiate(toSummon, target.position, target.rotation);
    }
}
