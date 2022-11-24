// /*
//  * @Author: delevin.ying 
//  * @Date: 2022-11-17 16:09:10 
//  * @Last Modified by: delevin.ying
//  * @Last Modified time: 2022-11-17 16:09:50
//  */
// using Lockstep;
// using Lockstep.Core.Logic.Interfaces;
// using Lockstep.Core.Logic.Serialization;
// using Lockstep.Core.Logic.Serialization.Utils;
// using System;

// [Serializable]
// public class FireCommand : ICommand, ISerializable
// {
//     // public uint skillId;
//     public uint entityId;
//     // public uint moveDir;

//     public ushort Tag => CommandDefine.Fire;

//     public void Execute(InputEntity e)
//     {
//         // UnityEngine.Debug.Log($" System 响应键盘输入 {moveSpeed}");
//         // e.AddCharacterInputSpeed(moveSpeed, inputOriginData);
//         // e.AddSkillInput(skillId, entityId);
//     }

//     public void Serialize(Serializer writer)
//     {
//         writer.Put(skillId);
//         writer.Put(entityId);
//     }

//     public void Deserialize(Deserializer reader)
//     {
//         skillId = reader.GetUInt();
//         entityId = reader.GetUInt();
//     }
// }