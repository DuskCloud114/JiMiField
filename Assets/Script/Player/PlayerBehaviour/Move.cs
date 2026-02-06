using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rigidbody;
    private IInputProvider inputProvider;
    private InputFrame inputFrame;

    void Awake()
    {
        // 获取输入层组件
        inputProvider = GetComponent<IInputProvider>();
        if (inputProvider != null) inputFrame = inputProvider.GetInputFrame();
        else Debug.LogError($"{gameObject.name}上未找到 IInputProvider 组件");

        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        PlayerMove(inputFrame.MoveInput);
    }

    public void PlayerMove(Vector2 moveInput)
    {
        Vector2 movement = moveInput.normalized * moveSpeed;
        rigidbody.velocity = new Vector2(movement.x, rigidbody.velocity.y);
    }

}
