using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingObject : BaseClass {
    Rigidbody o_rigidbody;

    void Start()
    {
        Initialize();
    }

    public override void Initialize()
    {
        base.Initialize();
        o_rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate () {
        //transform.position = world.GetPointOn(transform.position); //placera mig på spheren

        CalculateVelocity();
    }

    void CalculateVelocity()
    {
        float length = o_rigidbody.velocity.magnitude;
        Vector3 cross1 = Vector3.Cross(o_rigidbody.velocity, world.GetUpVector(transform.position)).normalized;
        Vector3 cross2 = Vector3.Cross(world.GetUpVector(transform.position), cross1).normalized;

        o_rigidbody.velocity = cross2 * length;
    }

    void AddGravity()
    {

    }
}
