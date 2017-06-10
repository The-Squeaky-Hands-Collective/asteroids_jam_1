using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
    public GameObject objectToFollow;

    public float heigthAboveObject = 20f;
    public float lowThresholdToResetSpeed = 0.5f;
    public float maxMovementSpeed = 4.7f;

    private float currentMovementSpeed = 0f;
    private Vector3 lastMovement = Vector3.zero;
    private Vector3 desiredPositionForCamera = Vector3.zero;
    private float movementIncrease = 0;

    void Start()
    {

    }

    void FixedUpdate()
    {
        ComputeDesiredPosition();
        float distanceToDesiredPosition = (desiredPositionForCamera - transform.position).magnitude;
        if(distanceToDesiredPosition < lowThresholdToResetSpeed)
        {
            currentMovementSpeed = 0f;
            movementIncrease = 0;
        }

        MoveTowardsDesiredPosition();
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

    private float ComputeSpeedIncrease(float speed)
    {
        movementIncrease += 0.009f;
        return speed + (0.01f * movementIncrease * movementIncrease);
    }
}
