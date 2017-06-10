using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : EntityMovement
{
    // Input
    public KeybindingManager keybindingManager;

    // Projectiles
    public GameObject projectileTemplate;
    public GameObject projectileOriginPositionObject;

    // Input
    protected AvailablePlayerActions availablePlayerActions;

    protected override void Start()
    {
        transform.position = world.transform.position + new Vector3(0f, 0f, -world.transform.localScale.x * 0.5f);
        availablePlayerActions = keybindingManager.GetCurrentAvailablePlayerActions();
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(availablePlayerActions.shoot.WasPressed)
        {
            Vector3 projectilePosition = (projectileOriginPositionObject.transform.position + projectileOriginPositionObject.transform.forward * 1f);
            GameObject projectile = Instantiate(projectileTemplate, projectilePosition, projectileTemplate.transform.localRotation);
            projectile.transform.forward = projectileOriginPositionObject.transform.forward;
            projectile.SetActive(true);
        }
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

    protected override void LimitVelocity()
    {
        // TODO Might want to implement it's own version of it. It does not always make sense to limit velocity like this.
        base.LimitVelocity();
    }
}
