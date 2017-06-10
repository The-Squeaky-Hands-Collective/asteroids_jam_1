using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour {
    public static World world;
	// Use this for initialization
	void Start () {
        Init();
	}

    public void Init()
    {
        if (world == null)
        {
            world = GameObject.FindObjectOfType(typeof(World)) as World;
        }
    }

}
