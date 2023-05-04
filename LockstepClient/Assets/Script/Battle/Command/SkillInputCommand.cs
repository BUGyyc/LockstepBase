using Lockstep;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using System;

[Serializable]
public class SkillInputCommand : ICommand, ISerializable
{
    public uint skillId;
    public uint entityId;
    // public uint moveDir;


    public bool leftMousePressed;
    public bool rightMousePressed;

    public LVector3 shootDir;

    public ushort Tag => CommandDefine.SkillInput;

    public void Execute(InputEntity e)
    {
         UnityEngine.Debug.Log($" System 响应键盘输入 {shootDir}");
        // e.AddCharacterInputSpeed(moveSpeed, inputOriginData);
        e.AddSkillInput(skillId, entityId, leftMousePressed, rightMousePressed, shootDir);
    }

    public void Serialize(Serializer writer)
    {
        writer.Put(skillId);
        writer.Put(entityId);

        writer.Put(leftMousePressed);
        writer.Put(rightMousePressed);
    }

    public void Deserialize(Deserializer reader)
    {
        skillId = reader.GetUInt();
        entityId = reader.GetUInt();

        leftMousePressed = reader.GetBool();
        rightMousePressed = reader.GetBool();
    }
}