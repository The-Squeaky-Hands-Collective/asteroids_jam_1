using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityMovement : BaseClass
{
    // Forces
    public float forwardAcceleration;
    public float leftTurnAcceleration;
    public float rightTurnAcceleration;
    public float maxAllowedVelocity;

    // Object
    protected new Rigidbody rigidbody;

    // Gravity
    protected Vector3 gravityDirection = Vector3.zero;
    protected float gravityScale = 1.0f;

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false; // Disable engine gravity
    }

    protected virtual void FixedUpdate()
    {
        ComputeGravityDirection();
        ApplyGravity();
        ComputeTransformRelativeUp();
        ComputeForces();
        LimitVelocity();
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

    protected virtual void LimitVelocity()
    {
        Vector3 currentVelocity = rigidbody.velocity;
        float currentVelocityMagnitude = rigidbody.velocity.magnitude;
        if (currentVelocityMagnitude > maxAllowedVelocity)
        {
            rigidbody.velocity = currentVelocity.normalized * maxAllowedVelocity;
        }
    }
}
