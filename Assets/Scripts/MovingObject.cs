using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : BaseClass {

	// Update is called once per frame
	void Update () {
        transform.position = world.GetPointOn(transform.position); //placera mig på spheren
    }
}
