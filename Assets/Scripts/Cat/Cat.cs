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

    private CatAction[] actions;
    private bool takeingAction = false;
    private float StunnedDuration = 0;
    private float HyperDuration = 0;
    public float hyperMultiplier = 2;


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
        
        if (StunnedDuration <=0 && HyperDuration <=0)
        {
            // Move towards the next location
            var position = transform.position;
            Vector3 towardsTarget = (movingTowards - position).normalized * (Time.deltaTime * speed);
            transform.position = position + towardsTarget;

            // Exit if we are not close enough to the destination
            if (Vector3.Distance(transform.position, movingTowards) > 0.1) return;

            // When we reach a destination stop moving and take an action
            movingTowards = GetNextMovementLoc();
            StartCoroutine(TakeAction(1));
        }

        if (StunnedDuration > 0)
        {
            StunnedDuration -= Time.deltaTime;
        }

        if (HyperDuration > 0)
        {
            //TODO: Lose energy over time.
            HyperDuration -= Time.deltaTime;

            if (StunnedDuration < 0)
            {
                // Move towards the next location, affected by Hyper Speed
                var position = transform.position;
                Vector3 towardsTarget = (movingTowards - position).normalized * (Time.deltaTime * speed * hyperMultiplier);
                transform.position = position + towardsTarget;

                // Exit if we are not close enough to the destination
                if (Vector3.Distance(transform.position, movingTowards) > 0.1) return;

                // When we reach a destination stop moving and take an action
                movingTowards = GetNextMovementLoc();
                StartCoroutine(TakeAction(hyperMultiplier));
            }
            
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
        yield return toPerform.Perform(firePoint, 1);
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
}
