using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class EntityMovement : BaseClass
{
    // Forces
    public float forwardAcceleration;
    public float maxAllowedVelocity;

    // Object
    protected new Rigidbody rigidbody;

    // Gravity
    protected Vector3 gravityDirection = Vector3.zero;
    protected float gravityScale = 1.0f;
    protected Vector3 vectorToGravityCenter = Vector3.zero;

    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false; // Disable engine gravity
    }

    protected virtual void FixedUpdate()
    {
        ComputeVectorToGravityCenter();
        ComputeGravityDirection();
        PreventOrbiting();
        ApplyGravity();
        ComputeTransformRelativeUp();
        ComputeForces();
        LimitVelocity();
    }

    private void ComputeVectorToGravityCenter()
    {
        vectorToGravityCenter = world.transform.position - transform.position;
    }

    private void ComputeGravityDirection()
    {
        gravityDirection = vectorToGravityCenter.normalized;
    }

    private void ApplyGravity()
    {
        float gravityForce = gravityConstant * gravityScale * (rigidbody.mass * world.GetMass()) / vectorToGravityCenter.magnitude;
        //Debug.Log("Distance: " + vectorToGravityCenter.magnitude + " Force: " + gravityForce);
      //  GUI.Button(Rect);

    //    rigidbody.AddForce(gravityDirection * world.GetDiameter() * 0.2f, ForceMode.Acceleration);
        rigidbody.AddForce(gravityDirection * gravityForce, ForceMode.Acceleration);
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

    protected virtual void PreventOrbiting()
    {
       // transform.position = world.GetPointOn(transform.position, transform.localScale.y * 0.55f);
    }
}
