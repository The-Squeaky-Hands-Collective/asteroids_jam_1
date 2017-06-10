using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailablePlayerActions : PlayerActionSet
{
    public PlayerAction rotateLeft;
    public PlayerAction rotateRight;
    public PlayerAction forward;
    public PlayerAction jump;
    public PlayerAction shoot;

    public AvailablePlayerActions()
    {
        rotateLeft = CreatePlayerAction("Rotate Left");
        rotateRight = CreatePlayerAction("Rotate Right");
        forward = CreatePlayerAction("Move forward");
        jump = CreatePlayerAction("Jump");
        shoot = CreatePlayerAction("Basic Shoot");
    }
}