using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : MonoBehaviour
{
    private RepeaterConfigSO config; // 中继器配置文件

    [SerializeField] private bool isPowered; // 是否供电 运行时状态


    [Tooltip("拖入中继器供电状态变化时事件配置文件")]
    private GameEventSO onPowerStateChangeEvent; // 中继器供电状态变化时事件配置文件

    // 视觉反馈
    // Renderer 组件是 Unity 渲染组件的基类， SpriteRenderer 是 Renderer 的一个具体实现， 用于渲染网格对象
    private SpriteRenderer spriteRenderer; // 中继器的 SpriteRenderer 组件 用于改变颜色

    // 字段
    public bool IsPowered => isPowered; // 供电状态的公共只读属性

    void Awake()
    {
        config = GetComponent<PublicRepeaterConfig>().Config; // 获取 RepeaterConfigSO 配置文件
        if (config == null) Debug.LogError("中继器" + name + "缺少配置文件");

        spriteRenderer = GetComponent<SpriteRenderer>(); // 获取 SpriteRenderer 组件
        if (spriteRenderer == null) Debug.LogError("中继器" + name + "缺少SpriteRenderer组件");

        UpdateVisual(); // 初始化视觉状态
    }

    void UpdateVisual()
    {
        if (spriteRenderer != null && config != null) spriteRenderer.color = isPowered ? config.PowerColor : config.UnpowerColor; // 根据供电状态更新颜色
        else Debug.LogError("中继器" + name + "缺少SpriteRenderer组件或配置文件");
        Debug.Log("中继器" + name + "当前供电状态: " + (isPowered ? "供电" : "未供电") + " 颜色: " + spriteRenderer.color);
    }

    void ChangePowerState(bool powered)
    {
        if (isPowered == powered) return; // 如果供电状态没有变化，直接返回

        isPowered = powered; // 更新供电状态
        UpdateVisual(); // 更新视觉状态

        // 通过事件通道通知其他系统
        if (onPowerStateChangeEvent != null) onPowerStateChangeEvent.Raise(); // 触发供电状态变化事件
        else Debug.LogError("中继器" + name + "缺少供电状态变化事件配置文件");
    }

    private void OnDrawGizmos()
    {
        if (config == null) return; // 如果没有配置文件， 不绘制 gizmo

        // 在编辑器中绘制供电范围的方形 gizmo
        Gizmos.color = isPowered ? config.PowerColor : config.UnpowerColor; // 根据供电状态设置 gizmo 颜色
        Gizmos.DrawWireCube(transform.position, new Vector3(config.PowerRange * 2, config.PowerRange * 2, 0)); // 绘制供电范围的方形 gizmo
    }
}
