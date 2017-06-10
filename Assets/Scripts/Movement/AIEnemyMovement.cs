using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyMovement : EntityMovement
{
    public bool targetIsPlayer = true;

    public GameObject target;

    private bool goingRandom = false;

    protected override void Start()
    {
        base.Start();
        if(targetIsPlayer)
        {
            GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
            if (player.Length != 1)
            {
                throw new UnityException("Incorrect playercount.");
            }
            target = player[0];
        }
        transform.position = world.GetRandomPoint();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void ComputeForces()
    {
        Vector3 directionToTarget = target.transform.position - transform.position;
        float distanceToPlayer = directionToTarget.magnitude;
        if (world.GetRadius() > distanceToPlayer)
        {
            rigidbody.AddForce(directionToTarget * distanceToPlayer, ForceMode.Acceleration);
            goingRandom = false;
        }
        else
        {
            if(!goingRandom)
            {
                goingRandom = true;
                transform.Rotate(transform.up, Random.Range(0f, 360f));
            }
            rigidbody.AddForce(transform.forward * distanceToPlayer, ForceMode.Acceleration);
            // Go a random direction until you're closer to the player
        }
    }
}
