using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingObject : BaseClass
{
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

    void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, world.GetPointOn(transform.position), Time.deltaTime * 2); //placera mig på spheren

        if (Vector3.Distance(transform.position, world.GetPointOn(transform.position)) > (transform.localScale.x * 0.61f))
        {
            MoveToGround();
        }
    }

    void FixedUpdate()
    {
        CalculateVelocity();
    }

    void CalculateVelocity()
    {
        float length = o_rigidbody.velocity.magnitude;
        Vector3 cross1 = Vector3.Cross(o_rigidbody.velocity, world.GetUpVector(transform.position)).normalized;
        Vector3 cross2 = Vector3.Cross(world.GetUpVector(transform.position), cross1).normalized;

        o_rigidbody.velocity = cross2 * length;
    }

    void MoveToGround()
    {
        //Vector3 dir = (world.GetPointOn(transform.position) - transform.position).normalized;
        //transform.position += dir * 2 * Time.deltaTime;
        transform.position = world.GetPointOn(transform.position);
    }

    //void AddGravity()
    //{
    //    o_rigidbody.AddForce(gravityConstant * -world.GetUpVector(transform.position));
    //}
}
