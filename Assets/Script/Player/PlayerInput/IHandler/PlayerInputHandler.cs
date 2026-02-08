using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, IInputProvider
{
    private InputFrame currentInputFrame;

    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        // 查看输入系统传入的 Vector2 的值
        Debug.Log($"Move Input Received: {ctx.ReadValue<Vector2>()}");

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

    // 获取当前输入帧
    public InputFrame GetInputFrame()
    {
        return currentInputFrame;
    }
}
