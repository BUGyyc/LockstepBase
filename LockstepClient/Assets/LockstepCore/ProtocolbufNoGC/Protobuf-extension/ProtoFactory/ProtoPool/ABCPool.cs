using Protocol;

public sealed class ABCPool : ProtoPoolBase<ABC>
{
    protected override void ClearNetData(ABC netData)
    {
        netData.IVal = 0;
        netData.FVal = 0f;
        netData.UVal = 0;
    }

    protected override void RecycleChildren(ABC data)
    {
        // TODO:不含引用就不需要写
    }
}