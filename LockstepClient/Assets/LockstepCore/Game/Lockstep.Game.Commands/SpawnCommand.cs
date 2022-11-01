using System;
//using BEPUutilities;
using Lockstep;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;

namespace Lockstep.Game.Commands
{


    [Serializable]
    public class SpawnCommand : ICommand, ISerializable
    {
        public int EntityConfigId;

        public LVector3 Position;

        public ushort Tag => 2;

        public void Execute(InputEntity e)
        {
            //UnityEngine.Debug.Log($" Spawn  Position {Position}  ");
            e.AddCoordinate(Position);
            e.AddEntityConfigId(EntityConfigId);
        }

        public void Serialize(Serializer writer)
        {
            writer.Put(EntityConfigId);
            writer.Put(Position.x);
            writer.Put(Position.y);
            writer.Put(Position.z);

            //writer.Put(Position.X.RawValue);
            //writer.Put(Position.Y.RawValue);

        }

        public void Deserialize(Deserializer reader)
        {
            EntityConfigId = reader.GetInt();
            Position.x = reader.GetInt();
            Position.y = reader.GetInt();
            Position.z = reader.GetInt();

            //Position.X.RawValue = reader.GetLong();
            //Position.Y.RawValue = reader.GetLong();
        }
    }
}