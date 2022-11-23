using CustomDataStruct;
using ProtoBuf.Serializers;
using Protocol;

public sealed class ABCDecorator : ICustomProtoSerializer
{
    public void SetValue(object target, object value, int fieldNumber)
    {
        ABC data = target as ABC;
        if (data == null)
        {
            return;
        }

        switch (fieldNumber)
        {
            case 1:
                data.UVal = ValueObject.Value<uint>(value);
                break;
            case 3:
                data.FVal = ValueObject.Value<float>(value);
                break;
            case 2:
                data.IVal = ValueObject.Value<int>(value);
                break;
            default:
                break;
        }
    }

    public object GetValue(object target, int fieldNumber)
    {
        ABC data = target as ABC;
        if (data == null)
        {
            return null;
        }

        switch (fieldNumber)
        {
            case 1:
                return ValueObject.Get(data.UVal);
            case 3:
                return ValueObject.Get(data.FVal);
            case 2:
                return ValueObject.Get(data.IVal);
        }

        return null;
    }
}
