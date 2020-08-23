using System;
using System.Collections;
using UnityEngine;

public class LaunchProjectileInAllLanesCatAction : CatAction
{
    public GameObject projectile;
    public float timeToPerform;

    private Cat cat;
    private SpriteRenderer render;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        cat = GetComponent<Cat>();
    }

    public override IEnumerator Perform(Transform target, float speed)
    {
        yield return new WaitForSeconds(timeToPerform / 3 / speed);
        render.flipX = true;
        yield return new WaitForSeconds(timeToPerform / 3 / speed);
        foreach (var lane in cat.movementPoints)
        {
            var targetPos = new Vector3(target.position.x, lane.position.y, lane.position.z);
            Instantiate(projectile, targetPos, target.rotation);
        }
        yield return new WaitForSeconds(timeToPerform / 3 / speed);
        render.flipX = false;
    }
}
