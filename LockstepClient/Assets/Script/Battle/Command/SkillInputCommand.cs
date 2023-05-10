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

    //public LVector3 shootDir;

    public int shootX;
    public int shootY;
    public int shootZ;


    public bool leftMousePressed;
    public bool rightMousePressed;

    public ushort Tag => CommandDefine.SkillInput;

    public void Execute(InputEntity e)
    {
        // UnityEngine.Debug.Log($" System 响应键盘输入 {moveSpeed}");
        // e.AddCharacterInputSpeed(moveSpeed, inputOriginData);

        var lv3 = new UnityEngine.Vector3(shootX, shootY, shootZ).ToLVector3();  //new Lockstep.LVector3(false, shootX, shootY, shootZ);

        e.AddSkillInput(skillId, entityId, lv3, leftMousePressed, rightMousePressed);
    }

    public void Serialize(Serializer writer)
    {
        writer.Put(skillId);
        writer.Put(entityId);
        writer.Put(shootX);
        writer.Put(shootY);
        writer.Put(shootZ);
        writer.Put(leftMousePressed);
        writer.Put(rightMousePressed);
    }

    public void Deserialize(Deserializer reader)
    {
        skillId = reader.GetUInt();
        entityId = reader.GetUInt();

        shootX = reader.GetInt();
        shootY = reader.GetInt();
        shootZ = reader.GetInt();


        leftMousePressed = reader.GetBool();
        rightMousePressed = reader.GetBool();
    }
}