using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventWithParam<T> : ScriptableObject
{
    private readonly HashSet<System.Action<T>> listeners = new HashSet<System.Action<T>>();

    public void Raise(T param)
    {
        foreach (var listener in listeners)
        {
            listener?.Invoke(param); // 触发事件时调用所有监听器
        }
    }

    public void RegisterListener(System.Action<T> listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(System.Action<T> listener)
    {
        listeners.Remove(listener);
    }
}
