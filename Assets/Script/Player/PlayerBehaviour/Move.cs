using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rigidbody;
    private IInputProvider playerInputProvider; // 玩家输入层接口
    private InputFrame inputFrame;

    void Awake()
    {
        // 获取输入层组件
        playerInputProvider = GetComponent<IInputProvider>();
        if (playerInputProvider != null) inputFrame = playerInputProvider.GetInputFrame();
        else Debug.LogError($"{gameObject.name}上未找到 IInputProvider 组件");

        rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody == null) Debug.LogError($"{gameObject.name}上未找到 Rigidbody2D 组件");

    }

    void Update()
    {
        if (playerInputProvider != null) inputFrame = playerInputProvider.GetInputFrame();
    }

    void FixedUpdate()
    {
        PlayerMove(inputFrame.MoveInput);
    }

    public void PlayerMove(Vector2 moveInput)
    {
        // 调试输出移动输入
        Debug.Log($"MoveInput: {moveInput}");

        Vector2 movement = moveInput.normalized * moveSpeed;

        // 如果是 Top-Down (俯视) 游戏，直接应用 XY 速度
        // 记得把 Rigidbody2D 的 Gravity Scale 设为 0
        rigidbody.velocity = movement;
    }

}
