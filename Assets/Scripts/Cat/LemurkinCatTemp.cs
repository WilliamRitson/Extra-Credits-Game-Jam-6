using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LemurkinCatTemp : MonoBehaviour
{
    public Transform firePoint;
    public Transform[] movementPoints;
    public float speed = 2;
    private int nextMovementPoint = 0;
    private int moveDir = 1;
    private Vector3 movingTowards;
    private AudioSource audioSource;


    //
    private String state;

    private CatAction[] actions;
    private bool takeingAction = false;


    //Which state the cat is in. 1 is the default state, 2 is distracted, 3 is 
    private int catState = 1;

    private void Start()
    {
        state = "default";
        audioSource = GetComponent<AudioSource>();
        actions = GetComponents<CatAction>();
        movingTowards = movementPoints[0].position;
    }

    private void Update()
    {
        // If we are doing somethign don't move
        if (takeingAction) return;

        if (state == "default")
        {

            // Move towards the next location
            var position = transform.position;
            Vector3 towardsTarget = (movingTowards - position).normalized * (Time.deltaTime * speed);
            transform.position = position + towardsTarget;

            // Exit if we are not close enough to the destination
            if (Vector3.Distance(transform.position, movingTowards) > 0.1) return;

            // When we reach a destination stop moving and take an action
            movingTowards = GetNextMovementLoc();
            StartCoroutine(TakeAction());
        }
    }

    private IEnumerator TakeAction()
    {
        CatAction toPerform = actions[Random.Range(0, actions.Length)];
        Debug.Log("Use action: " + toPerform.abilityTitle);
        MovingTextManager.Instance.ShowMessage(toPerform.abilityTitle, transform.position, Color.white);
        if (toPerform.soundEffect != null)
        {
            audioSource.clip = toPerform.soundEffect;
            audioSource.Play();
        }
        takeingAction = true;
        yield return toPerform.Perform(firePoint);
        takeingAction = false;
    }

    private Vector3 GetNextMovementLoc()
    {
        int nextMoveIndex = nextMovementPoint + moveDir;
        if (nextMoveIndex == movementPoints.Length || nextMoveIndex == -1)
        {
            moveDir *= -1;
        }

        nextMovementPoint += moveDir;
        return movementPoints[nextMovementPoint].position;
    }
}
