using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    public static World world;

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (world == null)
        {
            world = FindObjectOfType<World>();
        }
    }
}
