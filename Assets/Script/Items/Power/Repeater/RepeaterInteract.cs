using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterInteract : IInteractableBase
{
    private RepeaterConfigSO config; // 中继器配置文件

    [SerializeField] private float interactDistance; // 交互距离
    public float InteractDistance
    {
        get => interactDistance;
        private set => interactDistance = value;
    }

    [Tooltip("拖入交互事件配置文件")]
    [SerializeField] private InteractEventSO onInteractEvent; // 交互事件配置文件

    void Awake()
    {
        config = GetComponent<PublicRepeaterConfig>().Config; // 获取 RepeaterConfigSO 配置文件
        if (config == null) Debug.LogError("中继器" + name + "缺少配置文件");

        InteractDistance = config.InteractDistance; // 从配置文件中获取交互距离
        InteractPriority = config.InteractPriority; // 从配置文件中获取交互优先级
        InteractPrompt = config.InteractPrompt; // 从配置文件中获取交互提示

        if (onInteractEvent == null) Debug.LogError("中继器" + name + "缺少交互事件配置文件");
    }


    public override bool CanInteract(GameObject interactor)
    {
        if (config == null) return false; // 如果没有 config 组件，无法交互
        if (!base.CanInteract(interactor)) return false; // 如果父类判断不可交互，返回 false
        return true; // 始终可以交互
    }

    public override void OnInteract(GameObject interactor)
    {
        if (config == null) return; // 如果没有 config 组件，直接返回

        Debug.Log("中继器" + name + "被交互");

        // 通过事件通道通知其他系统
        if (onInteractEvent != null) onInteractEvent.Raise(this); // 触发交互事件
        else Debug.LogError("中继器" + name + "缺少交互事件配置文件");
    }

    public override void OnInteractEnd(GameObject interactor)
    {
        if (config == null) return; // 如果没有 config 组件，直接返回
        Debug.Log("中继器" + name + "交互结束");
    }
}
