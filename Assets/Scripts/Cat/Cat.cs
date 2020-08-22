using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cat : MonoBehaviour
{
    public Transform firePoint;
    public Transform[] movementPoints;
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

    private enum State {
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
    }

    private void Update()
    {
        // If we are doing somethign don't move
        if (takeingAction) return;

        if (HyperDuration <= 0 && StunnedDuration <= 0)
        {
            catState = State.Neutral;
        }

        if (StunnedDuration > 0)
        {
            catState = State.Stunned;
            StunnedDuration -= Time.deltaTime;
        }

        if (HyperDuration > 0)
        {
            if (catState != State.Stunned)
            {
                catState = State.Hyper;
            }
            HyperDuration -= Time.deltaTime;
        }



        if (catState == State.Neutral)
        {
            Move(1);
        }

        if (catState == State.Hyper)
        {
            Move(hyperMultiplier);   
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
        int nextMoveIndex = nextMovementPoint + moveDir;
        if (nextMoveIndex == movementPoints.Length  || nextMoveIndex == -1)
        {
            moveDir *= -1;
        }

        nextMovementPoint += moveDir;
        return movementPoints[nextMovementPoint].position;
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
