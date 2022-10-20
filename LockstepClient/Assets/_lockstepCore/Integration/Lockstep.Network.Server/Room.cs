using System;
using System.Collections.Generic;
using System.Linq;
using Lockstep.Core.Logic.Serialization;
using Lockstep.Core.Logic.Serialization.Utils;
using Lockstep.Network.Messages;
using Lockstep.Network.Server.Interfaces;
using UnityEngine;

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

        public void Open(int port)
        {
            _server.ClientConnected += OnClientConnected;
            _server.ClientDisconnected += OnClientDisconnected;
            _server.DataReceived += OnDataReceived;
            _server.Run(port);
            Debug.Log("Server started. Waiting for " + _size + " players...");
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
                case 2:
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
                case 3:
                    {
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
            int seed = new System.Random().Next(int.MinValue, int.MaxValue);
            this.Starting?.Invoke(this, new StartedEventArgs(20, _actorIds.Values.ToArray()));
            foreach (KeyValuePair<int, byte> actorId in _actorIds)
            {
                serializer.Reset();
                serializer.Put((byte)0);
                Init init = new Init();
                init.Seed = seed;
                init.ActorID = actorId.Value;
                init.AllActors = _actorIds.Values.ToArray();
                init.SimulationSpeed = 20;
                init.Serialize(serializer);
                _server.Send(actorId.Key, Compressor.Compress(serializer));
            }
            this.Started?.Invoke(this, new StartedEventArgs(20, _actorIds.Values.ToArray()));
        }
    }
}