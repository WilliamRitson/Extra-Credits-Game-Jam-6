using System.Collections;
using UnityEngine;

public class LaunchProjectileCatAction : CatAction
{
    public GameObject projectile;
    public float timeToPerform;

    public override IEnumerator Perform(Transform target)
    {
        yield return new WaitForSeconds(timeToPerform / 2);
        Instantiate(projectile, target.position, target.rotation);
        yield return new WaitForSeconds(timeToPerform / 2);
    }
}
