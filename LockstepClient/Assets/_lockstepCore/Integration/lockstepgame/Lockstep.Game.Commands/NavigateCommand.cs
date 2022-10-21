﻿using System;
using BEPUutilities;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;

namespace Lockstep.Game.Commands
{

    [Serializable]
    public class NavigateCommand : ICommand, ISerializable
    {
        public uint[] Selection;

        public Vector2 Destination;

        public ushort Tag => 1;

        //TODO:InputEntity
        public void Execute(InputEntity e)
        {
            e.AddSelection(Selection);
            e.AddCoordinate(Destination);
        }

        public void Serialize(Serializer writer)
        {
            writer.PutArray(Selection);
            writer.Put(Destination.X.RawValue);
            writer.Put(Destination.Y.RawValue);
        }

        public void Deserialize(Deserializer reader)
        {
            Selection = reader.GetUIntArray();
            Destination.X.RawValue = reader.GetLong();
            Destination.Y.RawValue = reader.GetLong();
        }
    }
}
