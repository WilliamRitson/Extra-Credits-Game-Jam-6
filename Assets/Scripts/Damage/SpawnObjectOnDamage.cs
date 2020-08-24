using UnityEngine;
public class SpawnObjectOnDamage : OnDamageBehavior
{
    [SerializeField] GameObject toSpawn;

    protected override void OnDamaged(int damage)
    {
        if (damage == 0) return;;
        Instantiate(toSpawn, transform.position, transform.rotation);
    }
}