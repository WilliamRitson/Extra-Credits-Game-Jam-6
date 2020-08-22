using UnityEngine;

public class SpawnObjectOnDeath : OnDeathBehavior
{
    [SerializeField] GameObject toSpawn;

    protected override void OnDeath()
    {
        Instantiate(toSpawn, transform.position, transform.rotation);
    }
}
