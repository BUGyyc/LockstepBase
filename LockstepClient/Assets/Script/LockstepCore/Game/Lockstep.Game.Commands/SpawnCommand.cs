using System;
//using BEPUutilities;
using Lockstep;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;

namespace Lockstep.Game.Commands
{
    /// <summary>
    /// 创建 GameEntity 的指令
    /// </summary>
    [Serializable]
    public class SpawnCommand : ICommand, ISerializable
    {
        public int EntityConfigId;

        public LVector3 Position;

        // public EntityType type;


        /// <summary>
        /// 存储 GameEntity 的额外数据
        /// </summary>
        // public byte[] entityData;

        public ushort Tag => 2;

        public void Execute(InputEntity e)
        {
            //LogMaster.L($"Spawn Pos:{Position}");
            e.AddCoordinate(Position);
            e.AddEntityConfigId(EntityConfigId);
            // e.AddExtraEntityData(entityData);
            //e.A
        }

        public void Serialize(Serializer writer)
        {
            // writer.Put((uint)type);
            writer.Put(EntityConfigId);
            writer.Put(Position._x);
            writer.Put(Position._y);
            writer.Put(Position._z);
            // writer.PutBytesWithLength(entityData);

            //writer.Put(speed._x);
            //writer.Put(speed._y);
            //writer.Put(speed._z);


            //writer.Put(Position.X.RawValue);
            //writer.Put(Position.Y.RawValue);
        }

        public void Deserialize(Deserializer reader)
        {
            // type = (EntityType)reader.GetUInt();
            EntityConfigId = reader.GetInt();
            Position._x = reader.GetInt();
            Position._y = reader.GetInt();
            Position._z = reader.GetInt();
            // entityData = reader.GetBytesWithLength();


            //speed._x = reader.GetInt();
            //speed._y = reader.GetInt();
            //speed._z = reader.GetInt();
            //

            //Position.X.RawValue = reader.GetLong();
            //Position.Y.RawValue = reader.GetLong();
        }
    }
}
