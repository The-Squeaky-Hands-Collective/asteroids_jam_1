using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : BaseClass
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = world.GetRandomPoint();
        }
    }
}
