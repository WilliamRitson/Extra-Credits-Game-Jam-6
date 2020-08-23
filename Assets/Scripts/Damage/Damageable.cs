using System;
using System.Collections.Generic;
using UnityEngine;


public class Damageable : MonoBehaviour
{
    [Tooltip("The maximum health this entity can have.")]
    [SerializeField] private int maximumLife;
    public int MaximumLife { get => maximumLife; set => SetMaximumHealth(value); }

    [Tooltip("The initial amount of health this entity should start with.")]
    [SerializeField] private int currentLife;
    public int CurrentLife { get => currentLife; set => SetHealth(value); }

    [Tooltip("The time in seconds the entity is invincible after taking damage")]
    public float BaseInvincibilityTime = 0;
    private float InvincibilityTime;
    
    /// <summary> An event fired when the entity hits 0 health. </summary>
    public event Action OnDeath;

    /// <summary> An event fired whenever the entity's health changes. </summary>
    public event Action<int> OnHealthChange;

    /// <summary> An event fired whenever the entity takes damage </summary>
    public event Action<int> OnDamaged;


    public delegate int DamageModifier(int damage, Element element);
    public readonly List<DamageModifier> Modifiers = new List<DamageModifier>();


    public void Update()
    {
        if (isInvincible())
        {
            InvincibilityTime -= Time.deltaTime;
        }
    }

    /// <summary>Sets the health of this entity bounded between 0 and the maximumLife value.</summary>
    /// <returns>The change from the previous value.</returns>
    public int SetHealth(int newValue)
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
    public int SetMaximumHealth(int newValue)
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

    private int GetEffectiveDamage(int damage, Element element)
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
    public int TakeDamage(int damage, Element element)
    {
        if (!isInvincible())
        {
            var effectiveDamage = GetEffectiveDamage(damage, element);
            var resultantDamage = SetHealth(currentLife - effectiveDamage);
            if (resultantDamage > 0)
            {
                InvincibilityTime = BaseInvincibilityTime;
                OnDamaged?.Invoke(resultantDamage);
            }
            return resultantDamage;
        } else
        {
            return 0;
        }
    }

    public int Heal(int healAmount)
    {
        var healed = SetHealth(currentLife + healAmount);
        return healed;
    }

    public bool isInvincible()
    {
        return (InvincibilityTime > 0);
    }
}
