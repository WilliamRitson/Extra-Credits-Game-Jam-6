using UnityEngine;


[RequireComponent(typeof(Damageable))]
public abstract class OnDeathBehavior : MonoBehaviour
{
    protected Damageable Damageable;

    private void Start()
    {
        Damageable = GetComponent<Damageable>();
        Damageable.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        if (Damageable != null)
            Damageable.OnDeath -= OnDeath;
    }

    protected abstract void OnDeath();
}

[RequireComponent(typeof(Damageable))]
public abstract class OnDamageBehavior : MonoBehaviour
{
    private Damageable damageable;

    private void Start()
    {
        damageable = GetComponent<Damageable>();
        damageable.OnDamaged += OnDamaged;
    }

    private void OnDestroy()
    {
        if (damageable != null)
            damageable.OnDamaged -= OnDamaged;
    }

    protected abstract void OnDamaged(float damage);
}

[RequireComponent(typeof(Damageable))]
public abstract class OnHealthChangeBehavior : MonoBehaviour
{
    private Damageable damageable;

    private void Start()
    {
        damageable = GetComponent<Damageable>();
        damageable.OnHealthChange += OnHealthChange;
    }

    private void OnDestroy()
    {
        damageable.OnHealthChange -= OnHealthChange;
    }

    protected abstract void OnHealthChange(float health);
}