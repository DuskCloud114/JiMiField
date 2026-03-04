using UnityEngine;

//配置文件的公共字段，避免脚本直接引用配置文件
public class PublicRepeaterConfig : MonoBehaviour
{
    [Tooltip("拖入中继器配置文件, 其他脚本将通过此组件获取中继器配置数据 而不是直接引用配置文件")]
    [SerializeField] private RepeaterConfigSO config; // 中继器配置文件

    public RepeaterConfigSO Config => config; // 公共只读属性，获取中继器配置文件

}