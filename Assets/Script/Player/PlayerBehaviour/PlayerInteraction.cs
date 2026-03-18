using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // 交互参数 暂用 之后将写入 SO
    [SerializeField] private float interactionRange; 
    [SerializeField] private float interactInterval; // 交互检测间隔
    [SerializeField] private LayerMask interactableLayerMask;

    // 运行时状态
    private float lastInteractTime; // 上次交互检测的时间
    private IInteractable previousTarget; // 上次交互对象
    private IInteractable currentTarget; // 当前交互对象

    [Tooltip("发现交互对象时广播")]
    [SerializeField] private InteractEventSO onTargetFoundEvent; // 目标发现事件

    [Tooltip("丢失交互对象时广播")]
    [SerializeField] private GameEventSO onTargetLostEvent; // 目标丢失事件 

    /// <summary>
    /// 下一步计划： 
    /// 内存收敛：应该提供一个全局唯一的 PhysicsQueryService，内部持有一个全局静态的 Collider2D[128] 缓存池供单线程轮流使用，避免即使是初始化时产生的冗余内存。
    /// 职责分离：业务逻辑层（如技能系统、AI视觉）只需要请求数据，不应该关心底层是如何 NonAlloc 的。
    /// 使用 ContactFilter2D：现代 Unity 推荐使用 ContactFilter2D 配合 List<Collider2D>，这是更新的封装，底层实现原理相似，但支持更多过滤条件。
    /// </summary>
    private Collider2D[] hitBuffer = new Collider2D[10]; // 用于存储检测结果的缓冲区
    private List<IInteractable> interactableObjects = new List<IInteractable>();


    private void Update()
    {
        lastInteractTime += Time.deltaTime;
        if (lastInteractTime >= interactInterval)
        {
            lastInteractTime = 0f;
            DetectInteractableObjects();
        }
    }

    private void DetectInteractableObjects()
    {
        int hitCount = Physics2D.OverlapCircleNonAlloc(
            transform.position, // 检测中心
            interactionRange, // 检测范围
            hitBuffer, // 存储检测结果的缓冲区
            interactableLayerMask // 只检测指定层的对象
        );

        for (int i = 0; i < hitCount; i++)
        {
            Collider2D collider = hitBuffer[i];
            if (collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if (interactable.CanInteract(gameObject))
                {
                    interactableObjects.Add(interactable);
                }
            }
        }

        previousTarget = currentTarget;

        if (interactableObjects.Count == 0)
        {
            if (currentTarget != null)
            {
                currentTarget.OnInteractEnd(gameObject);
                if (onTargetLostEvent != null) onTargetLostEvent?.Raise();
                currentTarget = null;
            }

            return;
        }

        interactableObjects.Sort((a, b) =>
        {
            int priorityComparison = b.InteractPriority.CompareTo(a.InteractPriority);
            if (priorityComparison != 0) return priorityComparison;

            float distanceA = Vector2.Distance(transform.position, a.InteractTransform.position);
            float distanceB = Vector2.Distance(transform.position, b.InteractTransform.position);
            return distanceB.CompareTo(distanceA);
        });

        currentTarget = interactableObjects[0];
        if (currentTarget != previousTarget)
        {
            if (previousTarget != null) previousTarget.OnInteractEnd(gameObject); // 通知旧目标交互结束
            if (onTargetFoundEvent != null) onTargetFoundEvent?.Raise(currentTarget); // 广播新目标发现事件
        }
    }

    private void HandleInteraction()
    {
        /// 下一步计划
        /// 绑定新版输入系统的交互按键事件，触发当前目标的交互逻辑。
    }

    private void OnDrawGizmosSelected()
    {
        // 在编辑器中绘制交互范围的圆形 gizmo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
