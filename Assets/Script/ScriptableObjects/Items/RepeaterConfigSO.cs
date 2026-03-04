using UnityEngine;

[CreateAssetMenu(fileName = "NewRepeaterConfig", menuName = "Items/RepeaterConfig")]

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

    [Tooltip("中继器交互显示文字")]
    [SerializeField] private string interactPrompt; // 中继器交互显示文字

    [Tooltip("中继器交互距离")]
    [SerializeField] private float interactDistance; // 中继器交互距离

    [Tooltip("中继器交互优先级")]
    [SerializeField] private int interactPriority; // 中继器交互优先级

    // 公共字段
    public float MaxWireLength => maxWireLength; // 只读 公共属性，获取最大电线长度
    public float ConnectingLength => connectingLength; // 只读 公共属性，获取连接长度
    public float PowerRange => powerRange; // 只读 公共属性，获取供电范围

    public Color UnpowerColor => unpowerColor; // 只读 公共属性，获取未供电时中继器 ui 显示颜色
    public Color PowerColor => powerColor; // 只读 公共属性，获取供电时中继器 ui 显示颜色

    public float InteractDistance => interactDistance; // 只读 公共属性，获取中继器交互距离
    public int InteractPriority => interactPriority; // 只读 公共属性，获取中继器交互优先级
    public string InteractPrompt => interactPrompt; // 只读 公共属性，获取中继器交互显示文字

    private void OnValidate()
    {
        // 在编辑器中验证配置数据的合法性，确保数值合理，避免运行时错误

        // 电线距离验证
        if (maxWireLength < 0)
        {
            Debug.LogWarning("最大电线长度不能为负数，已自动设置为0");
            maxWireLength = 0; // 确保最大电线长度不为负数
        }
        if (connectingLength < 0)
        {
            Debug.LogWarning("连接长度不能为负数，已自动设置为0");
            connectingLength = 0; // 确保连接长度不为负数
        }
        if (powerRange < 0)
        {
            Debug.LogWarning("供电范围不能为负数，已自动设置为0");
            powerRange = 0; // 确保供电范围不为负数
        }
        if (connectingLength > maxWireLength)
        {
            Debug.LogWarning("连接长度不能大于最大电线长度，已自动调整连接长度为最大电线长度");
            connectingLength = maxWireLength; // 确保连接长度不大于最大电线长度
        }

        // 视觉配置验证
        if (unpowerColor == null)
        {
            Debug.LogWarning("未供电时中继器 ui 显示颜色不能为空，已自动设置为红色");
            unpowerColor = Color.red; // 确保未供电时中继器 ui 显示颜色不为 null
        }
        if (powerColor == null)
        {
            Debug.LogWarning("供电时中继器 ui 显示颜色不能为空，已自动设置为绿色");
            powerColor = Color.green; // 确保供电时中继器 ui 显示颜色不为 null
        }

        // 交互距离验证
        if (interactDistance < 0)
        {
            Debug.LogWarning("中继器交互距离不能为负数，已自动设置为0");
            interactDistance = 0; // 确保中继器交互距离不为负数
        }
        // 交互优先级验证
        if (interactPriority < 0)
        {
            Debug.LogWarning("中继器交互优先级不能为负数，已自动设置为0");
            interactPriority = 0; // 确保中继器交互优先级不为负数
        }
        if (interactPrompt == "" )
        {
            Debug.LogWarning("中继器交互显示文字不能为空，已自动设置为'交互'");
            interactPrompt = "交互"; // 确保中继器交互显示文字不为空
        }
    }
}