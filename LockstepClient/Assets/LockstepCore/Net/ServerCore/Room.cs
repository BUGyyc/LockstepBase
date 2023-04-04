using System;
using System.Collections.Generic;
using System.Linq;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using Lockstep.Network.Messages;
using Lockstep.Network.Server.Interfaces;
using UnityEngine;
using System.IO;

using Protocol;

namespace Lockstep.Network.Server
{

    public class Room
    {
        private const int SimulationSpeed = 20;

        private readonly Dictionary<int, byte> _actorIds = new Dictionary<int, byte>();

        private readonly Dictionary<ulong, long> _hashCodes = new Dictionary<ulong, long>();

        private uint inputMessageCounter = 0u;

        private byte _nextPlayerId;

        private readonly int _size;

        private readonly IServer _server;

        public bool Running { get; private set; }

        public event EventHandler<StartedEventArgs> Starting;

        public event EventHandler<StartedEventArgs> Started;

        public event EventHandler<InputReceivedEventArgs> InputReceived;

        public Room(IServer server, int size)
        {
            _server = server;
            _size = size;
        }

        public int OnLivePlayerCount()
        {
            if (_actorIds == null) return 0;

            return _actorIds.Count > _size ? _size : _actorIds.Count;
        }

        public void Open(int port)
        {
            _server.ClientConnected += OnClientConnected;
            _server.ClientDisconnected += OnClientDisconnected;
            _server.DataReceived += OnDataReceived;
            _server.Run(port);
            Debug.Log("[服务器] 启动， Waiting for " + _size + " players...");
        }

        private void OnClientConnected(int clientId)
        {
            _actorIds.Add(clientId, _nextPlayerId++);
            if (_actorIds.Count == _size)
            {
                Debug.Log("Room is full, starting new simulation...");
                StartSimulationOnConnectedPeers();
                return;
            }
            Debug.Log(_actorIds.Count + " / " + _size + " players have connected.");
        }

        private void OnDataReceived(int clientId, byte[] data)
        {
            Deserializer deserializer = new Deserializer(Compressor.Decompress(data));
            switch (deserializer.GetByte())
            {
                case NetProtocolDefine.Input:
                    {
                        inputMessageCounter++;
                        uint uInt = deserializer.GetUInt();
                        deserializer.GetByte();
                        int @int = deserializer.GetInt();
                        if (@int > 0 || inputMessageCounter % 8u == 0)
                        {
                            _server.Distribute(clientId, data);
                        }
                        this.InputReceived?.Invoke(this, new InputReceivedEventArgs(_actorIds[clientId], uInt));
                        break;
                    }
                case NetProtocolDefine.CheckSync:
                    {
                        // HashCode 验证是否同步
                        Lockstep.Network.Messages.HashCode hashCode = new Lockstep.Network.Messages.HashCode();
                        hashCode.Deserialize(deserializer);
                        if (!_hashCodes.ContainsKey(hashCode.FrameNumber))
                        {
                            _hashCodes[hashCode.FrameNumber] = hashCode.Value;
                        }
                        else
                        {
                            Debug.Log(((_hashCodes[hashCode.FrameNumber] == hashCode.Value) ? "HashCode valid" : "Desync") + ": " + hashCode.Value);
                        }
                        break;
                    }
                default:
                    _server.Distribute(data);
                    break;
            }
        }

        private void OnClientDisconnected(int clientId)
        {
            _actorIds.Remove(clientId);
            if (_actorIds.Count == 0)
            {
                Debug.Log("All players left, stopping current simulation...");
                Running = false;
            }
            else
            {
                Debug.Log(_actorIds.Count + " players remaining.");
            }
        }

        private void StartSimulationOnConnectedPeers()
        {
            Serializer serializer = new Serializer();
            InitMsg msg = new InitMsg();

            int seed = new System.Random().Next(int.MinValue, int.MaxValue);

            const int SimulationFPS = 20;

            this.Starting?.Invoke(this, new StartedEventArgs(SimulationFPS, _actorIds.Values.ToArray()));
            foreach (KeyValuePair<int, byte> actorId in _actorIds)
            {
                serializer.Reset();
                serializer.Put(NetProtocolDefine.Init);

                // msg.type = NetProtocolDefine.Init;

                // Init init = new Init();
                // init.Seed = seed;
                // init.ActorID = actorId.Value;
                // init.AllActors = _actorIds.Values.ToArray();
                // init.SimulationSpeed = SimulationFPS;
                // init.Serialize(serializer);

                msg.seed = seed;
                msg.actorId = actorId.Value;
                foreach (var item in _actorIds.Values)
                {
                    msg.actorList.Add(item);
                }
                msg.simulationSpeed = SimulationFPS;

                // byte[] bs = msg.

                MemoryStream data = new MemoryStream();
                ProtoBufSerializer.Serialize(data, msg);
                byte[] bs = data.ToArray();

                serializer.Put(bs);
                // Buffer.BlockCopy();
                _server.Send(actorId.Key, serializer.Data);


                //Debug.Log($"<color=yellow> 服务器发起，启动游戏  msg {msg.actorId}  {msg.seed}  {msg.simulationSpeed} {msg.type} </color>");


                //var testNetPack = serializer.Data;
                //Deserializer deserializer = new Deserializer(testNetPack);
                //uint pId = deserializer.GetByte();

                //MemoryStream stream = new MemoryStream(deserializer.RawData);

                //UnityEngine.Debug.Log($"<color=red> stream  {stream.Length}  </color>");

                //InitMsg test = ProtoBufSerializer.Deserialize(stream, typeof(InitMsg), (int)stream.Length) as InitMsg;

                //UnityEngine.Debug.Log($"<color=red>  Client get Msg  net  pId  {pId}  msg:  {test.actorId}  {test.seed}  {test.simulationSpeed}  {test.type}   </color>");


                // _server.Send(actorId.Key, Compressor.Compress(serializer));
            }
            this.Started?.Invoke(this, new StartedEventArgs(SimulationFPS, _actorIds.Values.ToArray()));
        }
    }
}