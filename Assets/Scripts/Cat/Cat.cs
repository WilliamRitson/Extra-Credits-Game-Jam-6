using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Cat : MonoBehaviour
{

    public float movementSpeed = 2;
    public float hyperDps = 1.0f;
    public float baseSpeed = 2;

    public Transform firePoint;
    public Transform[] movementPoints;
    public SpriteRenderer[] intentionIndicators;
    public GameObject aura;
    public GameObject kitten;
    
    private readonly Dictionary<Transform, int> movementPointAttraction = new Dictionary<Transform, int>();
    private Vector3 movingTowards;
    private AudioSource audioSource;
    private readonly List<CatAction> intentions = new List<CatAction>();
    private readonly List<Vector3> moveIntentions = new List<Vector3>();
    private SpriteRenderer spriteRenderer;
    private Damageable damageable;
    private CatAction[] actions;
    private Animator animator;
    
    private float hyperDamage = 0;
    private float catSpeed;
    private float timeElapsed = 0;
    private bool takingAction = false;
    private float stunnedDuration = 0;
    private float hyperDuration = 0;
    public float hyperMultiplier = 2;
    private static readonly int Attack = Animator.StringToHash("Attack");

    public enum State {
        Neutral,
        Hyper,
        Stunned
    }
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        damageable.MaximumLife = Options.GetCatHealth();
        damageable.CurrentLife = Options.GetCatHealth();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        actions = GetComponents<CatAction>();
        foreach (var point in movementPoints)
        {
            movementPointAttraction[point] = 1;
        }

        foreach (var _ in intentionIndicators)
        {
            AddIntention();
        }
        DrawIntentions();
        movingTowards = moveIntentions[0];

    }

    private void DrawIntentions()
    {
        for (int i = 0; i < intentions.Count; i++)
        {
            intentionIndicators[i].sprite = intentions[i].intentionIcon;
            var pos = intentionIndicators[i].gameObject.transform.position;
            intentionIndicators[i].gameObject.transform.position = new Vector3(pos.x, moveIntentions[i].y, pos.z);
        }
    }

    private void Update()
    {
        //Difficulty scaling functionality
        timeElapsed += Time.deltaTime;
        catSpeed = (timeElapsed / 300) + baseSpeed;

        var state = GetCurrentState();
        TickStatusEffects(state);

        // If we are doing something or stunned  don't move
        if (takingAction || state == State.Stunned) return;
        
        var multiplier = state == State.Hyper ? hyperMultiplier : 1;
        Move(catSpeed * multiplier);
    }

    void TickStatusEffects(State state)
    {
        ReduceDurations();
        ApplyStatusVisualEffects(state);
        if (state != State.Hyper) return;
        hyperDamage += Time.deltaTime * hyperDps;
        if (!(hyperDamage >= 1)) return;
        hyperDamage -= 1;
        damageable.TakeDamage(1, Element.Distraction);
    }

    private void ApplyStatusVisualEffects(State state)
    {
        aura.SetActive(state == State.Hyper);
        kitten.SetActive(state == State.Stunned);
        spriteRenderer.flipY = state == State.Stunned;
        switch (state)
        {
            case State.Hyper:
                spriteRenderer.color = Color.red;
                break;
            default:
                spriteRenderer.color = Color.white;
                break;
        }
    }


    private State GetCurrentState()
    {
        if (stunnedDuration > 0)
        {
            return State.Stunned;
        } 
        if (hyperDuration > 0)
        {
            return State.Hyper;
        }
        return State.Neutral;
    }

    private void ReduceDurations()
    {

        if (stunnedDuration > 0)
        {
            stunnedDuration -= Time.deltaTime;
        }
        else
        {
            stunnedDuration = 0;
        }

        if (hyperDuration > 0)
        {
            hyperDuration -= Time.deltaTime;
        }
        else
        {
            hyperDuration = 0;
        }
    }


    private IEnumerator TakeAction(float castRate)
    {
        CatAction toPerform = intentions[0];

        
        MovingTextManager.Instance.ShowMessage(toPerform.abilityTitle, transform.position, Color.white);
        if (toPerform.soundEffect != null)
        {
            audioSource.clip = toPerform.soundEffect;
            audioSource.Play();
        }
        takingAction = true;
        yield return toPerform.Perform(firePoint, castRate, this);
        takingAction = false;
        
        intentions.RemoveAt(0);
        moveIntentions.RemoveAt(0);
        AddIntention();
        DrawIntentions();
        movingTowards = moveIntentions[0];
    }

    private void AddIntention()
    {
        intentions.Add(GenerateIntention());
        moveIntentions.Add(GetNextMovementLoc());
    }
    
    private CatAction GenerateIntention()
    {
        var tickets = new List<CatAction>();
        foreach (var action in actions)
        {
            for (var i = 0; i < GetTicketsForAction(action); i++)
            {
                tickets.Add(action);
            }
        }
        return tickets[Random.Range(0, tickets.Count)];
    }

    private int GetTicketsForAction(CatAction action)
    {
        switch (action.frequency)
        {
            case ActionFrequency.Common:
                return 3;
            case ActionFrequency.Rare:
                return 1;
        }
        return 0;
    }


    private Vector3 GetNextMovementLoc()
    {
        var tickets = new List<Vector3>();
        foreach (var point in movementPoints)
        {
            for (var i = 0; i < movementPointAttraction[point]; i++)
            {
                tickets.Add(point.position);
            }
        }
        return tickets[Random.Range(0, tickets.Count)];
    }

    public void RegisterAttractor(float positionY, int strength)
    {
        movementPointAttraction[GetClosestMovementPoint(positionY)] += strength;
        
        // Recompute cats intentions
        for (int i = 0; i < moveIntentions.Count; i++)
        {
            moveIntentions[i] = GetNextMovementLoc();
        }
        DrawIntentions();

        if (takingAction) return;
        movingTowards = moveIntentions[0];
    }

    public void DeregisterAttractor(float positionY, int strength)
    {
        movementPointAttraction[GetClosestMovementPoint(positionY)] -= strength;
    }

    private Transform GetClosestMovementPoint(float positionY)
    {
        float lowestDistance = float.PositiveInfinity;
        Transform best = null;
        foreach (var point in movementPoints)
        {
            var dist = Math.Abs(point.position.y - positionY);
            if (dist >= lowestDistance) continue;
            lowestDistance = dist;
            best = point;
        }
        return best;
    }

    public void ApplyStatusEffect(float duration, State type)
    {
        switch (type)
        {
            case State.Hyper:
                hyperDuration += duration;
                break;
            case State.Stunned:
                stunnedDuration += duration;
                break;
        }
    }

    private void Move(float multiplier)
    {
        // Move towards the next location
        var position = transform.position;
        Vector3 towardsTarget = (movingTowards - position).normalized * (Time.deltaTime * movementSpeed * multiplier);
        transform.position = position + towardsTarget;

        // Exit if we are not close enough to the destination
        if (Vector3.Distance(transform.position, movingTowards) > 0.1) return;

        // When we reach a destination stop moving and take an action
        StartCoroutine(TakeAction(multiplier));
    }

    public void AnimateAttack()
    {
        animator.SetTrigger(Attack);

    }

    public void SetNextIntention(CatAction nextAction)
    {
        intentions[1] = nextAction;
        DrawIntentions();
    }
}
