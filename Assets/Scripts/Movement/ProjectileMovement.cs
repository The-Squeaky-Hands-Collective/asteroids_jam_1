using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : EntityMovement
{
    protected override void Start()
    {
        base.Start();
        rigidbody.AddForce(transform.forward * forwardAcceleration, ForceMode.VelocityChange);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void ComputeForces()
    {
        rigidbody.AddForce(transform.forward * forwardAcceleration, ForceMode.Acceleration);
    }
}
