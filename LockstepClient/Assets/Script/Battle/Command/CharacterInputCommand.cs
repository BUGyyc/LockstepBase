using Lockstep;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using System;

[Serializable]
public class CharacterInputCommand : ICommand, ISerializable
{
    public uint entityId;

    /// <summary>
    /// 等同于摇杆、WASD
    /// </summary>
    public LVector2 moveDir;

    /// <summary>
    /// 摄像机朝向，这个不需要频繁传输
    /// </summary>
    public LVector3 viewDir;

    // public bool mouse

    // public uint moveDir;

    public ushort Tag => CommandDefine.CharacterInput;

    public void Execute(InputEntity e)
    {
        UnityEngine.Debug.Log($" System 响应键盘输入 {moveDir}");
        e.AddCharacterInput(entityId, moveDir, viewDir);

    }

    public void Serialize(Serializer writer)
    {
        writer.Put(viewDir._x);
        writer.Put(viewDir._y);
        writer.Put(viewDir._z);

        writer.Put(moveDir._x);
        writer.Put(moveDir._y);
        // writer.Put(inputOriginData._z);

        writer.Put(entityId);
    }

    public void Deserialize(Deserializer reader)
    {
        int x = reader.GetInt();
        int y = reader.GetInt();
        int z = reader.GetInt();
        viewDir = new LVector3(true, x, y, z);

        int a = reader.GetInt();
        int b = reader.GetInt();

        moveDir = new LVector2(true, a, b);

        entityId = reader.GetUInt();
    }
}