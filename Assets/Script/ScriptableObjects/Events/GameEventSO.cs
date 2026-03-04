using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameEventSO", menuName = "Events/GameEventSO")]

// 游戏事件配置文件 继承了 ScriptableObject 可以在Unity编辑器中创建实例并配置数据
public class GameEventSO : ScriptableObject
{
    private HashSet<GameEventListener> listeners = new HashSet<GameEventListener>(); // 监听器集合 用于存储所有注册的监听器

    // 注册监听器
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener); // 将监听器添加到集合中 哈希表的添加操作平均时间复杂度为 O(1)， 且不允许重复添加
    }

    // 注销监听器
    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener); // 将监听器从集合中移除 哈希表的删除操作平均时间复杂度为 O(1)
    }

    // 触发事件
    public void Raise()
    {
        foreach (var listener in listeners) // 遍历所有注册的监听器
        {
            listener.OnEventRaised(); // 调用监听器的响应方法 触发事件时调用所有监听器的响应方法
        }
    }

}