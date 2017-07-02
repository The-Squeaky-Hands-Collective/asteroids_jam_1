using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rasmus TODO, split into camera modules which allows easier change and/or modification without changing the other ones.
/// Camera volumes? Volumes that affect how the camera is used.
/// Camera powerups, which changes camera attributes and/or types.
/// Multiple focus points which is used to interpolate the camera's desired position and/or rotation.
/// Another camera idea is to follow vehicle's acceleration's direction.
/// Continue on version 2 and 4 primarly.
/// </summary>
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
    private Vector3 anotherUp = Vector3.zero;
    private Transform desiredTransform;

    private void Awake()
    {
        desiredTransform = new GameObject().transform;
        desiredTransform.transform.position = Vector3.zero;
        desiredTransform.localRotation = Quaternion.identity;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 20, 20), cameraType.ToString(), GUIStyle.none))
        {
            ++cameraType;
            if (cameraType == 5)
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

        if (Vector3.Dot(objectToFollow.transform.forward, Vector3.down) > 0)
        {
            anotherUp = Vector3.Lerp(anotherUp, Vector3.down, Time.deltaTime * 0.1f);
        }
        else
        {
            anotherUp = Vector3.Lerp(anotherUp, Vector3.up, Time.deltaTime * 0.1f);
        }

        desiredTransform.position = transform.position;
        desiredTransform.LookAt(objectToFollow.transform);

        if (cameraType == 0) transform.LookAt(objectToFollow.transform);
        if (cameraType == 1) transform.LookAt(objectToFollow.transform, objectToFollow.transform.forward);
        if (cameraType == 2) transform.LookAt(objectToFollow.transform, localUp);
        if (cameraType == 3) transform.LookAt(objectToFollow.transform, anotherUp);
        if (cameraType == 4)
        {
            // Debug.Log("Quaternion.Angle(transform.rotation, desiredTransform.rotation): " + Quaternion.Angle(transform.rotation, desiredTransform.rotation));

            float a = Quaternion.Angle(transform.rotation, desiredTransform.rotation);
            a = Mathf.Clamp(a, 1f, 180f);
            a *= 0.1f;
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredTransform.rotation, Time.deltaTime * 6.0f);
        }
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
