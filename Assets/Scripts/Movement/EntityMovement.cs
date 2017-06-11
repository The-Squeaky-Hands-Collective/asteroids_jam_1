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
    protected float gravityScale = 1.0f;
    protected Vector3 gravityDirection = Vector3.zero;
    protected float gravityForce = 0.0f;
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
        ComputeGravityForce();
        ApplyGravity();
        ComputeTransformRelativeUp();
        ComputeForces();
        LimitVelocity();
        PreventOrbiting();
    }

    private void ComputeVectorToGravityCenter()
    {
        vectorToGravityCenter = world.transform.position - transform.position;
    }

    private void ComputeGravityDirection()
    {
        gravityDirection = vectorToGravityCenter.normalized;
    }

    protected virtual void ComputeGravityForce()
    {
        gravityForce = gravityConstant * gravityScale * (rigidbody.mass * world.GetMass()) / vectorToGravityCenter.sqrMagnitude;
        // Debug.Log("Force: " + gravityForce);
        // Debug.Log("Velocity: " + rigidbody.velocity.magnitude);
        // Debug.Log("Distance: " + vectorToGravityCenter.magnitude);

        float distancetoGravityCenterWithShipOffset = vectorToGravityCenter.magnitude + gameObject.transform.localScale.y;

        float offsetConstant = 1.9f;
        if (distancetoGravityCenterWithShipOffset > world.GetRadius() + gameObject.transform.localScale.y * offsetConstant)
        {
            float diff = Mathf.Abs(distancetoGravityCenterWithShipOffset - world.GetRadius() - (gameObject.transform.localScale.y * offsetConstant));
            gravityForce += distancetoGravityCenterWithShipOffset - world.GetRadius() * diff;
            gravityForce += Mathf.Pow(distancetoGravityCenterWithShipOffset - world.GetRadius(), diff);
            // Debug.Log("Force2: " + gravityForce);
        }
    }

    private void ApplyGravity()
    {
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
        transform.position = world.GetPointOn(transform.position, transform.localScale.y * 0.5f);
    }
}
