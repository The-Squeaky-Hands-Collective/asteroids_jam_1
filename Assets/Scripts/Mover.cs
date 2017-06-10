using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : BaseClass {

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = world.GetRandomPoint();
        }
	}
}
