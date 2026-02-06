using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, IInputProvider
{
    private InputFrame currentInputFrame;

    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        Vector2 moveInput = ctx.ReadValue<Vector2>();
        currentInputFrame.MoveInput = moveInput;
    }

    public void OnJumpInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            currentInputFrame.IsJumpInput = true;
        }
        else if (ctx.performed)
        {
            currentInputFrame.IsJumpInput = true;
            currentInputFrame.IsJumpHeld = true;
        }
        else if (ctx.canceled)
        {
            currentInputFrame.IsJumpInput = false;
            currentInputFrame.IsJumpHeld = false;
        }
        else
        {
            currentInputFrame.IsJumpInput = false;
            currentInputFrame.IsJumpHeld = false;
        }
    }

    public InputFrame GetInputFrame()
    {
        return currentInputFrame;
    }
}
