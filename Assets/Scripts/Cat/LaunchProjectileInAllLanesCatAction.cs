using System;
using System.Collections;
using UnityEngine;

public class LaunchProjectileInAllLanesCatAction : CatAction
{
    public GameObject projectile;
    public float timeToPerform;

    private Cat cat;

    private void Start()
    {
        cat = GetComponent<Cat>();
    }

    public override IEnumerator Perform(Transform target, float speed)
    {
        yield return new WaitForSeconds(timeToPerform / 2 /speed);
        foreach (var lane in cat.movementPoints)
        {
            var targetPos = new Vector3(target.position.x, lane.position.y, lane.position.z);
            Instantiate(projectile, targetPos, target.rotation);
        }
        yield return new WaitForSeconds(timeToPerform / 2 /speed);
    }
}
