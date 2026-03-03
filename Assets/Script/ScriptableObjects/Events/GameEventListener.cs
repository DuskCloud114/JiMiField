using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [Tooltip("拖入事件通道")]
    [SerializeField] private GameEventSO gameEvent;

    [Tooltip("事件触发时调用的响应")]
    [SerializeField] private UnityEvent onEventRaised;

    private void OnEnable()
    {
        if (gameEvent != null) gameEvent.RegisterListener(this); // 注册监听器
    }

    private void OnDisable()
    {
        if (gameEvent != null) gameEvent.UnregisterListener(this); // 注销监听器   
    }

    public void OnEventRaised()
    {
        onEventRaised.Invoke(); // 触发事件时调用响应方法
    }
}
