using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Lockstep.Core.Logic;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using Lockstep.Game;
using Lockstep.Network.Messages;

namespace Lockstep.Network.Client
{

    public class NetworkCommandQueue : CommandQueue
    {
        private readonly INetwork _network;

        private readonly Dictionary<ushort, Type> _commandFactories = new Dictionary<ushort, Type>();

        public byte LagCompensation { get; set; }

        public event EventHandler<Init> InitReceived;

        public NetworkCommandQueue(INetwork network)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type item in from type in assembly.GetTypes()
                                      where type.GetInterfaces().Any((Type intf) => intf.FullName != null && intf.FullName!.Equals(typeof(ICommand).FullName))
                                      select type)
                {
                    ushort tag = ((ICommand)Activator.CreateInstance(item)).Tag;
                    if (_commandFactories.ContainsKey(tag))
                    {
                        throw new InvalidDataException($"The command tag {tag} is already registered. Every command tag must be unique.");
                    }
                    _commandFactories.Add(tag, item);
                }
            }
            _network = network;
            _network.DataReceived += NetworkOnDataReceived;
        }

        public override void Enqueue(Input input)
        {
            base.Enqueue(input);
            Serializer serializer = new Serializer();
            serializer.Put((byte)2);
            serializer.Put(input.Tick);
            serializer.Put(LagCompensation);
            serializer.Put(input.Commands.Count());
            serializer.Put(input.ActorId);
            foreach (ICommand command in input.Commands)
            {
                serializer.Put(command.Tag);
                command.Serialize(serializer);
            }
            _network.Send(Compressor.Compress(serializer));
        }

        private void NetworkOnDataReceived(byte[] rawData)
        {
            byte[] source = Compressor.Decompress(rawData);
            Deserializer deserializer = new Deserializer(source);
            switch (deserializer.GetByte())
            {
                case 0:
                    {
                        Init init = new Init();
                        init.Deserialize(deserializer);
                        this.InitReceived?.Invoke(this, init);
                        break;
                    }
                case 2:
                    {
                        uint tick = deserializer.GetUInt() + deserializer.GetByte();
                        int @int = deserializer.GetInt();
                        byte @byte = deserializer.GetByte();
                        ICommand[] array = new ICommand[@int];
                        for (int i = 0; i < @int; i++)
                        {
                            ushort uShort = deserializer.GetUShort();
                            if (_commandFactories.ContainsKey(uShort))
                            {
                                ICommand command = (ICommand)Activator.CreateInstance(_commandFactories[uShort]);
                                command.Deserialize(deserializer);
                                array[i] = command;
                            }
                        }
                        base.Enqueue(new Input(tick, @byte, array));
                        break;
                    }
            }
        }
    }
}