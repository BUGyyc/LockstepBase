using Lockstep;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using System;

[Serializable]
public class CharacterInputCommand : ICommand, ISerializable
{
    //public int EntityConfigId;

    public LVector3 moveSpeed;

    public uint entityId;

    public LVector2 inputOriginData;

    // public uint moveDir;

    public ushort Tag => CommandDefine.CharacterInput;

    public void Execute(InputEntity e)
    {
        UnityEngine.Debug.Log($" System 响应键盘输入 {moveSpeed}");
        e.AddCharacterInputSpeed(moveSpeed, inputOriginData);

    }

    public void Serialize(Serializer writer)
    {
        writer.Put(moveSpeed._x);
        writer.Put(moveSpeed._y);
        writer.Put(moveSpeed._z);

        writer.Put(inputOriginData._x);
        writer.Put(inputOriginData._y);
        // writer.Put(inputOriginData._z);

        writer.Put(entityId);
    }

    public void Deserialize(Deserializer reader)
    {
        int x = reader.GetInt();
        int y = reader.GetInt();
        int z = reader.GetInt();
        moveSpeed = new LVector3(true, x, y, z);

        int a = reader.GetInt();
        int b = reader.GetInt();

        inputOriginData = new LVector2(true, a, b);

        entityId = reader.GetUInt();
    }
}