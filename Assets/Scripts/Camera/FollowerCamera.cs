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
    private Vector3 desiredPositionForCamera = Vector3.zero;
    private float movementIncrease = 0;

    private int cameraType = 0;
    private Vector3 localUp = Vector3.zero;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 20, 20), cameraType.ToString(), GUIStyle.none))
        {
            ++cameraType;
            if (cameraType == 3)
            {
                cameraType = 0;
            }
        }
    }

    void FixedUpdate()
    {
        ComputeDesiredPosition();
        float distanceToDesiredPosition = (desiredPositionForCamera - transform.position).magnitude;
        if (distanceToDesiredPosition < lowThresholdToResetSpeed)
        {
            currentMovementSpeed = 0f;
            movementIncrease = 0;
        }

        MoveTowardsDesiredPosition();

        localUp = Vector3.Lerp(localUp, objectToFollow.transform.forward, Time.deltaTime);

        if (cameraType == 0) transform.LookAt(objectToFollow.transform.position);
        if (cameraType == 1) transform.LookAt(objectToFollow.transform.position, objectToFollow.transform.forward);
        if (cameraType == 2) transform.LookAt(objectToFollow.transform.position, localUp);
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
        transform.position = Vector3.Lerp(transform.position, desiredPositionForCamera, Time.deltaTime * currentMovementSpeed);
    }

    private float ComputeSpeedIncrease(float speed)
    {
        movementIncrease += 0.009f;
        return speed + (0.01f * movementIncrease * movementIncrease);
    }
}
