using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseClass : MonoBehaviour
{
    public static World world;
    public static float gravityConstant = 0.001f;

    void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        if (world == null)
        {
            world = FindObjectOfType<World>();
        }
    }
}
