using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class IInteractableBase : MonoBehaviour, IInteractable
{

    [Tooltip("是否启用交互")]
    [SerializeField] private bool isInteractable = true; // 是否启用交互
    
    [Tooltip("交互提示， 用于 UI 显示")]
    [SerializeField] private string interactPrompt; // 交互提示， 用于 UI 显示

    [Tooltip("交互优先级， 数值越小优先级越高")]
    [SerializeField] private int interactPriority; // 交互优先级， 数值越小优先级越高

    public virtual bool IsInteractable => isInteractable; // 是否可交互
    public virtual string InteractPrompt => interactPrompt; // 交互提示
    public virtual int InteractPriority => interactPriority; // 交互优先级
    public virtual Transform InteractTransform => transform; // 交互点位置， 默认为物体中心


    public virtual bool CanInteract(GameObject interactor) => isInteractable;

    // 抽象方法， 子类必须实现交互逻辑
    // 每种被交互对象的交互逻辑都不同， 因此定义为抽象方法
    public abstract void OnInteract(GameObject interactor);

    // 虚方法， 子类可以选择性重写交互结束逻辑
    // 不是所有被交互对象都需要交互结束逻辑， 因此定义为虚方法， 默认实现为空
    public virtual void OnInteractEnd(GameObject interactor) { }

    public virtual void SetInteractable(bool value) => isInteractable = value; // 设置是否可交互

}
