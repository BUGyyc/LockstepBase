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

        private readonly Dictionary<ushort, Type> _commandFactories =
            new Dictionary<ushort, Type>();

        /// <summary>
        /// 延迟补偿，允许有一定帧数的延迟
        /// </summary>
        /// <value></value>
        public byte LagCompensation { get; set; }

        public event EventHandler<Init> InitReceived;

        public NetworkCommandQueue(INetwork network)
        {
            //NOTE: 这里注册已经存在的 Command
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                foreach (
                    Type item in from type in assembly.GetTypes()
                                 where
                                     type.GetInterfaces()
                                         .Any(
                                             (Type intf) =>
                                                 intf.FullName != null
                                                 && intf.FullName!.Equals(
                                                     typeof(Lockstep.Core.Logic.Interfaces.ICommand).FullName
                                                 )
                                         )
                                 select type
                )
                {
                    ushort tag = (
                        (Lockstep.Core.Logic.Interfaces.ICommand)Activator.CreateInstance(item)
                    ).Tag;
                    if (_commandFactories.ContainsKey(tag))
                    {
                        throw new InvalidDataException(
                            $"The command tag {tag} is already registered. Every command tag must be unique."
                        );
                    }
                    _commandFactories.Add(tag, item);
                }
            }
            _network = network;
            _network.DataReceived += NetworkOnDataReceived;
        }

        /// <summary>
        /// 客户端塞包数据，然后发给服务器
        /// </summary>
        /// <param name="input"></param>
        public override void Enqueue(Input input)
        {
            base.Enqueue(input);
            Serializer serializer = new Serializer();
            serializer.Put(NetProtocolDefine.Input);
            serializer.Put(input.Tick);
            //塞入了 延迟补偿
            serializer.Put(LagCompensation);
            serializer.Put(input.Commands.Count());
            serializer.Put(input.ActorId);
            foreach (Lockstep.Core.Logic.Interfaces.ICommand command in input.Commands)
            {
                serializer.Put(command.Tag);
                command.Serialize(serializer);
            }
            _network.Send(Compressor.Compress(serializer));
        }

        /// <summary>
        /// 客户端收到服务器的包
        /// </summary>
        /// <param name="rawData"></param>
        private void NetworkOnDataReceived(byte[] rawData)
        {
            byte[] source = Compressor.Decompress(rawData);
            Deserializer deserializer = new Deserializer(source);
            switch (deserializer.GetByte())
            {
                case NetProtocolDefine.Init:
                    {
                        Init init = new Init();
                        init.Deserialize(deserializer);
                        this.InitReceived?.Invoke(this, init);
                        break;
                    }
                case NetProtocolDefine.Input:
                    {
                        //TODO: 这里的TICK是加上延迟补偿后的
                        // uint tick = deserializer.GetUInt() + deserializer.GetByte();

                        uint u1 = deserializer.GetUInt();
                        uint u2 = deserializer.GetByte();

                        uint tick = u1 + u2;

                        int @int = deserializer.GetInt();
                        byte @byte = deserializer.GetByte();

                        //LogMaster.L($"[Client] 接收网络包     NetPackage.InputCommand.Tick:{tick}    actorId:{@byte} ");

                        Lockstep.Core.Logic.Interfaces.ICommand[] array =
                            new Lockstep.Core.Logic.Interfaces.ICommand[@int];
                        for (int i = 0; i < @int; i++)
                        {
                            ushort uShort = deserializer.GetUShort();
                            if (_commandFactories.ContainsKey(uShort))
                            {
                                Lockstep.Core.Logic.Interfaces.ICommand command =
                                    (Lockstep.Core.Logic.Interfaces.ICommand)
                                        Activator.CreateInstance(_commandFactories[uShort]);
                                command.Deserialize(deserializer);
                                array[i] = command;
                            }
                        }

                        if (array.Length > 0)
                            LogMaster.L($"客户端收到网络包指令    tick:{tick}   actorId:{@byte} ");

                        base.Enqueue(new Input(tick, @byte, array));
                        break;
                    }
            }
        }
    }
}
