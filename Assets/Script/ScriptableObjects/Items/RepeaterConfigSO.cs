using UnityEngine;

[CreateAssetMenu(fileName = "RepeaterConfig", menuName = "Items/RepeaterConfig")]

// 中继器配置文件 继承了 ScriptableObject 可以在Unity编辑器中创建实例并配置数据
public class RepeaterConfigSO : ScriptableObject
{

    // 电线相关配置
    [Tooltip("最大电线长度, 超过此长度将断开连接")]
    [SerializeField] private float maxWireLength; // 最大电线长度

    [Tooltip("连接长度， 在此范围内可以连接其他设备， 超出此范围将无法连接并弹出警告")]
    [SerializeField] private float connectingLength; // 连接长度 


    // 供电相关配置
    [Tooltip("探测范围， 在此范围内可以为其他设备供电")]
    [SerializeField] private float powerRange; // 供电范围

    // 视觉相关配置
    [Tooltip("未供电时中继器 ui 显示颜色")]
    [SerializeField] private Color unpowerColor; // 未供电时中继器 ui 显示颜色

    [Tooltip("供电时中继器 ui 显示颜色")]
    [SerializeField] private Color powerColor; // 供电时中继器 ui 显示颜色

    // 公共字段
    public float MaxWireLength => maxWireLength; // 只读 公共属性，获取最大电线长度
    public float ConnectingLength => connectingLength; // 只读 公共属性，获取连接长度

    public float PowerRange => powerRange; // 只读 公共属性，获取供电范围


    public Color UnpowerColor => unpowerColor; // 只读 公共属性，获取未供电时中继器 ui 显示颜色
    public Color PowerColor => powerColor; // 只读 公共属性，获取供电时中继器 ui 显示颜色
}