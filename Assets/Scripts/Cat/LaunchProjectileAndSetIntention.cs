using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LaunchProjectileAndSetIntention : CatAction
{
    public GameObject projectile;
    public float timeToPerform;
    public string nextIntention;

    private Cat cat;
    private CatAction nextAction;

    public void Start()
    {
        cat = GetComponent<Cat>();
        nextAction = GetComponents<CatAction>().First(action => action.abilityTitle == nextIntention);
    }

    public override IEnumerator Perform(Transform target, float speed)
    {
        yield return new WaitForSeconds(timeToPerform / 2 /speed);
        Instantiate(projectile, target.position, target.rotation);
        cat.SetNextIntention(nextAction);
        yield return new WaitForSeconds(timeToPerform / 2 /speed);
    }
}
