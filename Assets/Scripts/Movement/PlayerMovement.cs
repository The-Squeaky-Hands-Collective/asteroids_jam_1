using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : EntityMovement
{ 
    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void ComputeForces()
    {
        if (availablePlayerActions.forward.State)
        {
            rigidbody.AddForce(transform.forward * forwardAcceleration, ForceMode.Acceleration);
        }

        if (availablePlayerActions.rotateLeft.State)
        {
            rigidbody.AddTorque(-transform.up * leftTurnAcceleration, ForceMode.Acceleration);
        }

        if (availablePlayerActions.rotateRight.State)
        {
            rigidbody.AddTorque(transform.up * leftTurnAcceleration, ForceMode.Acceleration);
        }
    }
}
