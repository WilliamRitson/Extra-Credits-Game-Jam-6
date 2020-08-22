using System;
using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public Transform firePoint;
    public Transform[] movementPoints;
    public float baseSpeed = 2;
    private float currentSpeed;
    private int nextMovementPoint = 0;
    private int moveDir = 1;
    private Vector3 movingTowards;

    private CatAction[] actions;

    private void Start()
    {
        actions = GetComponents<CatAction>();
        currentSpeed = baseSpeed;
        movingTowards = movementPoints[0].position;
    }

    private void Update()
    {
        Debug.Log($"Moving towards {movingTowards} currently at distance {Vector3.Distance(transform.position, movingTowards)}");
        
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
        CatAction toPerform = actions[0];
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
