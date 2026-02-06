
/// <summary>
/// 中间层， 将硬件输入转换为数据
/// </summary>
public interface IInputProvider
{
    // 获取当前帧的输入数据
    public InputFrame GetInputFrame();
}