using System;
using System.Collections.Generic;
using UnityEngine;


public class Damageable : MonoBehaviour
{
    [Tooltip("The maximum health this entity can have.")]
    [SerializeField] private float maximumLife;
    public float MaximumLife { get => maximumLife; set => SetMaximumHealth(value); }

    [Tooltip("The initial amount of health this entity should start with.")]
    [SerializeField] private float currentLife;
    public float CurrentLife { get => currentLife; set => SetHealth(value); }

    [Tooltip("The amount of health regen this entity should receive.")]
    public float HealthRegen;

    /// <summary> An event fired when the entity hits 0 health. </summary>
    public event Action OnDeath;

    /// <summary> An event fired whenever the entity's health changes. </summary>
    public event Action<float> OnHealthChange;

    /// <summary> An event fired whenever the entity takes damage </summary>
    public event Action<float> OnDamaged;


    public delegate float DamageModifier(float damage, Element element);
    public readonly List<DamageModifier> Modifiers = new List<DamageModifier>();

    void Update()
    {
        if(HealthRegen != 0 && CurrentLife < MaximumLife)
        {
            Heal(HealthRegen * Time.deltaTime);
        }
    }

    /// <summary>Sets the health of this entity bounded between 0 and the maximumLife value.</summary>
    /// <returns>The change from the previous value.</returns>
    public float SetHealth(float newValue)
    {
        if (currentLife == newValue)
            return 0;
        var previous = currentLife;
        currentLife = Math.Min(Math.Max(0, newValue), maximumLife);
        OnHealthChange?.Invoke(currentLife);
        if (currentLife == 0)
        {
            OnDeath?.Invoke();
        }

        return Math.Abs(previous - currentLife);
    }

    /// <summary>Sets the health of this entity to a value greater than 0. 
    /// If the maximum health is less than the current health updates the current health to be the maximum health</summary>
    /// <returns>The change from the previous value.</returns>
    public float SetMaximumHealth(float newValue)
    {
        if (currentLife == newValue)
            return 0;
        var previous = currentLife;
        maximumLife = Math.Max(0, newValue);
        if (maximumLife < currentLife)
        {
            SetHealth(maximumLife);
        }
        return previous - currentLife;
    }

    private float GetEffectiveDamage(float damage, Element element)
    {
        var effective = damage;

        foreach (var modifier in Modifiers)
        {
            effective = modifier(effective, element);
        }
        
        return effective;
    }
    

    /// <summary>Reduces health by the given amount.</summary>
    /// <returns>The change from the previous value.</returns>
    public float TakeDamage(float damage, Element element)
    {
        var effectiveDamage = GetEffectiveDamage(damage, element);
        var resultantDamage = SetHealth(currentLife - effectiveDamage);
        if (resultantDamage > 0)
        {
            OnDamaged?.Invoke(resultantDamage);
        }
        return resultantDamage;
    }

    public float Heal(float healAmount)
    {
        var healed = SetHealth(currentLife + healAmount);
        return healed;
    }
}
