using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cat : MonoBehaviour
{
    public Transform firePoint;
    public Transform[] movementPoints;
    private readonly Dictionary<Transform, int> movementPointAttraction = new Dictionary<Transform, int>();
    public float speed = 2;
    private int nextMovementPoint = 0;
    private int moveDir = 1;
    private Vector3 movingTowards;
    private AudioSource audioSource;

    private Damageable damageScript;

    private CatAction[] actions;
    private bool takeingAction = false;
    private float StunnedDuration = 0;
    private float HyperDuration = 0;
    public float hyperMultiplier = 2;

    public enum State {
        Neutral,
        Hyper,
        Stunned
        }

    private State catState = State.Neutral;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        actions = GetComponents<CatAction>();
        movingTowards = movementPoints[0].position;
        foreach (var point in movementPoints)
        {
            movementPointAttraction[point] = 1;
        }
    }

    private void Update()
    {
        ReduceDurations();

        // If we are doing something don't move
        if (takeingAction) return;

        switch (GetCurrentState())
        {
            case State.Neutral:
                Move(3);
                break;
            case State.Hyper:
                Move(hyperMultiplier);
                break;
        }

    }


    private State GetCurrentState()
    {
        if (StunnedDuration > 0)
        {
            return State.Stunned;
        } 
        if (HyperDuration > 0)
        {
            return State.Hyper;
        }
        return State.Neutral;
    }

    private void ReduceDurations()
    {

        if (StunnedDuration > 0)
        {
            StunnedDuration -= Time.deltaTime;
        }else
        {
            StunnedDuration = 0;
        }

        if (HyperDuration > 0)
        {
            HyperDuration -= Time.deltaTime;
        }
        else
        {
            HyperDuration = 0;
        }
    }


    private IEnumerator TakeAction(float CastRate)
    {
        CatAction toPerform = actions[Random.Range(0, actions.Length)];
        MovingTextManager.Instance.ShowMessage(toPerform.abilityTitle, transform.position, Color.white);
        if (toPerform.soundEffect != null)
        {
            audioSource.clip = toPerform.soundEffect;
            audioSource.Play();
        }
        takeingAction = true;
        yield return toPerform.Perform(firePoint, CastRate);
        takeingAction = false;
    }

    private Vector3 GetNextMovementLoc()
    {
        var tickets = new List<Vector3>();
        foreach (var point in movementPoints)
        {
            Debug.Log($"Attraction to point {point} is {movementPointAttraction[point]}.");
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
        Debug.Log($"Closest point to {positionY} is {best}.");
        return best;
    }

    public void ApplyStatusEffect(float duration, State type)
    {
        switch (type)
        {
            case State.Hyper:
                HyperDuration += duration;
                break;
            case State.Stunned:
                StunnedDuration += duration;
                break;
        }
    }

    private void Move(float multiplier)
    {
        // Move towards the next location
        var position = transform.position;
        Vector3 towardsTarget = (movingTowards - position).normalized * (Time.deltaTime * speed * multiplier);
        transform.position = position + towardsTarget;

        // Exit if we are not close enough to the destination
        if (Vector3.Distance(transform.position, movingTowards) > 0.1) return;

        // When we reach a destination stop moving and take an action
        movingTowards = GetNextMovementLoc();
        StartCoroutine(TakeAction(multiplier));
    }
}
