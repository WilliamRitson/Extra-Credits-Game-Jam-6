using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cat : MonoBehaviour
{
    public Transform firePoint;
    public Transform[] movementPoints;
    public float baseSpeed = 2;
    private float currentSpeed;
    private int nextMovementPoint = 0;
    private int moveDir = 1;
    private Vector3 movingTowards;
    private AudioSource audioSource;

    private CatAction[] actions;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        actions = GetComponents<CatAction>();
        currentSpeed = baseSpeed;
        movingTowards = movementPoints[0].position;
    }

    private void Update()
    {
        // Move towards the next location
        var position = transform.position;
        Vector3 towardsTarget = (movingTowards - position).normalized * (Time.deltaTime * currentSpeed);
        transform.position = position + towardsTarget;
        
        // When we reach a destination stop moving and take an action
        if (!(Vector3.Distance(transform.position, movingTowards) < 1)) return;
        movingTowards = GetNextMovementLoc();
        currentSpeed = 0;
        StartCoroutine(TakeAction());
    }

    private IEnumerator TakeAction()
    {
        CatAction toPerform = actions[Random.Range(0, actions.Length)];
        if (toPerform.soundEffect != null)
        {
            audioSource.clip = toPerform.soundEffect;
            audioSource.Play();
        }
        yield return toPerform.Perform(firePoint);
        currentSpeed = baseSpeed;
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
