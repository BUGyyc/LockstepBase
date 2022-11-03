using Lockstep;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using System;

[Serializable]
public class CreateEntityCommand : ICommand, ISerializable
{
    /// <summary>
    /// 坐标
    /// </summary>
    public LVector3 position;

    /// <summary>
    /// 欧拉旋转角
    /// </summary>
    public LVector3 rotateEuler;

    /// <summary>
    /// 创建的 Entity 类型
    /// </summary>
    public uint type;


    public ushort Tag => CommandDefine.CreateEntity;

    public void Execute(InputEntity e)
    {
        // UnityEngine.Debug.Log($" CharacterInputCommand  Execute ");
        // e.AddCharacterInputSpeed(moveSpeed);
    }

    public void Serialize(Serializer writer)
    {
        // writer.Put(moveSpeed._x);
        // writer.Put(moveSpeed._y);
        // writer.Put(moveSpeed._z);

        // writer.Put(entityId);
    }

    public void Deserialize(Deserializer reader)
    {
        // int x = reader.GetInt();
        // int y = reader.GetInt();
        // int z = reader.GetInt();

        // moveSpeed = new LVector3(true, x, y, z);
        // entityId = reader.GetUInt();
    }
}

