using System;
using BEPUutilities;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;

namespace Lockstep.Game.Commands
{


    [Serializable]
    public class SpawnCommand : Lockstep.Core.Logic.Interfaces.ICommand, ISerializable
    {
        public int EntityConfigId;

        public Vector2 Position;

        public ushort Tag => 2;

        public void Execute(InputEntity e)
        {
            UnityEngine.Debug.Log($" Spawn  Position {Position}  ");
            e.AddCoordinate(Position);
            e.AddEntityConfigId(EntityConfigId);
        }

        public void Serialize(Serializer writer)
        {
            writer.Put(EntityConfigId);
            writer.Put(Position.X.RawValue);
            writer.Put(Position.Y.RawValue);
        }

        public void Deserialize(Deserializer reader)
        {
            EntityConfigId = reader.GetInt();
            Position.X.RawValue = reader.GetLong();
            Position.Y.RawValue = reader.GetLong();
        }
    }
}