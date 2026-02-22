using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeaterInteract : IInteractableBase
{
    private Repeater repeater; // 中继器组件
    public override string InteractPrompt => "开始传输电线"; // 交互提示
    public override int InteractPriority => 1; // 交互优先级， 数值越小优先级越高 之后可能写入配置文件中， 待考虑


    void Awake()
    {
        repeater = GetComponent<Repeater>(); // 获取 Repeater 组件
        if (repeater == null) Debug.LogError("中继器" + name + "缺少Repeater组件");
    }


    public override bool CanInteract(GameObject interactor)
    {
        return true; // 始终可以交互
    }

    public override void OnInteract(GameObject interactor)
    {
        if (repeater == null) return; // 如果没有 Repeater 组件，直接返回
    }
}
