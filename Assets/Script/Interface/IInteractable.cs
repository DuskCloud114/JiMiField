using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string InteractPrompt { get; } // 交互提示， 用于 UI 显示
    int InteractPriority { get; } // 交互优先级， 数值越大优先级越高
    Transform InteractTransform { get; } // 交互点位置， 默认为物体中心

    bool CanInteract(GameObject interactor); // 判断是否可以交互
    void OnInteract(GameObject interactor); // 交互逻辑
    void OnInteractEnd(GameObject interactor); // 交互结束逻辑

}
