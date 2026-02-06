using UnityEngine;

/// <summary>
/// 输入数据的结构体， 用于 输入层 和 逻辑层 的数据传递
/// </summary>
[System.Serializable]
public struct InputFrame
{
    public Vector2 MoveInput; // 移动输入
    public bool IsJumpInput; // 跳跃短按
    public bool IsJumpHeld; // 跳跃长按
    public bool IsAttackTriggered; // 攻击触发
}