using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
    public GameObject objectToFollow;

    public float heigthAboveObject = 20f;

    public float thresholdStartFollow = 4;
    public float thresholdStopFollow = 3;

    // Movement
    public float maxMovementSpeed = 1f;

    private float currentMovementSpeed = 0f;

    private bool activeFollow = true;

    private Vector3 lastMovement = Vector3.zero;
    private Vector3 desiredPositionForCamera = Vector3.zero;

    void Start()
    {

    }

    void FixedUpdate()
    {
        ComputeDesiredPosition();
        float distanceToDesiredPosition = (desiredPositionForCamera - transform.position).magnitude;

        if (!activeFollow && distanceToDesiredPosition > thresholdStartFollow)
        {
            activeFollow = true;
        }
        else if(activeFollow && distanceToDesiredPosition < thresholdStopFollow)
        {
            activeFollow = false;
            currentMovementSpeed = 0;
        }

        if (activeFollow)
        {
            MoveTowardsDesiredPosition();
        }
        else
        {
            ExtrapolateMovement();
        }

        transform.LookAt(objectToFollow.transform.position);
    }

    private void ComputeDesiredPosition()
    {
        desiredPositionForCamera = objectToFollow.transform.position + (objectToFollow.transform.up * heigthAboveObject);
    }

    private void MoveTowardsDesiredPosition()
    {
        currentMovementSpeed = ComputeSpeedIncrease(currentMovementSpeed);
        currentMovementSpeed = Mathf.Clamp(currentMovementSpeed, 0, maxMovementSpeed);

        Vector3 direction = (desiredPositionForCamera - transform.position).normalized;
        Vector3 lastPosition = transform.position;
        transform.position = Vector3.Lerp(transform.position, desiredPositionForCamera, Time.deltaTime * currentMovementSpeed);
        lastMovement = transform.position - lastPosition;
    }

    private void ExtrapolateMovement()
    {
        transform.position += lastMovement;
    }

    private float ComputeSpeedIncrease(float speed)
    {
        return speed + 0.05f;
    }
}
