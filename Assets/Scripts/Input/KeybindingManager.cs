using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybindingManager : MonoBehaviour
{
    private AvailablePlayerActions currentKeyBindings;
    private AvailablePlayerActions defaultPlayerActions;

    private void Awake()
    {
        defaultPlayerActions = new AvailablePlayerActions();

        defaultPlayerActions.rotateLeft.AddDefaultBinding(Key.A);
        defaultPlayerActions.rotateLeft.AddDefaultBinding(Key.LeftArrow);
        defaultPlayerActions.rotateLeft.AddDefaultBinding(InputControlType.DPadLeft);

        defaultPlayerActions.rotateRight.AddDefaultBinding(Key.D);
        defaultPlayerActions.rotateRight.AddDefaultBinding(Key.RightArrow);
        defaultPlayerActions.rotateRight.AddDefaultBinding(InputControlType.DPadRight);

        defaultPlayerActions.forward.AddDefaultBinding(Key.W);
        defaultPlayerActions.forward.AddDefaultBinding(Key.UpArrow);
        defaultPlayerActions.forward.AddDefaultBinding(InputControlType.Action1);

        defaultPlayerActions.jump.AddDefaultBinding(Key.Space);
        defaultPlayerActions.jump.AddDefaultBinding(InputControlType.Action1);

        currentKeyBindings = defaultPlayerActions;
    }

    public AvailablePlayerActions GetCurrentAvailablePlayerActions()
    {
        return currentKeyBindings;
    }
}

