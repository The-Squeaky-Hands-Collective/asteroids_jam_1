using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityMovement : BaseClass
{
    // Input
    public KeybindingManager keybindingManager;

    // Forces
    public float forwardAcceleration;
    public float leftTurnAcceleration;
    public float rightTurnAcceleration;

    // Object
    protected new Rigidbody rigidbody;

    // Gravity
    protected Vector3 gravityDirection = Vector3.zero;
    protected float gravityScale = 1.0f;

    // Input
    protected AvailablePlayerActions availablePlayerActions;

    protected virtual void Start()
    {
        transform.position = world.transform.position + new Vector3(0f, 0f, -world.transform.localScale.x * 0.5f);
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false; // Disable engine gravity

        ComputeGravityDirection();
        transform.up = -gravityDirection;

        // Rotate the object to the world so that Z is rotated AWAY from the center of the world.
        Quaternion objectRotation = Quaternion.FromToRotation(transform.forward, -gravityDirection);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, objectRotation, 1f); // TODO Might be able to simplify this

        availablePlayerActions = keybindingManager.GetCurrentAvailablePlayerActions();
    }

    protected virtual void FixedUpdate()
    {
        ComputeGravityDirection();
        ApplyGravity();
        ComputeTransformRelativeUp();
        ComputeForces();
    }

    private void ComputeGravityDirection()
    {
        Vector3 relativeVetor = world.transform.position - transform.position;
        gravityDirection = (relativeVetor).normalized;
    }

    private void ApplyGravity()
    {
        Vector3 gravity = gravityConstant * gravityScale * gravityDirection;
        rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }

    private void ComputeTransformRelativeUp()
    {
        transform.rotation = Quaternion.LookRotation(-gravityDirection, -transform.forward);
        transform.Rotate(Vector3.right, 90f);
    }

    protected abstract void ComputeForces();

    private void LimitVelocity()
    {
        // TODO
    }
}
