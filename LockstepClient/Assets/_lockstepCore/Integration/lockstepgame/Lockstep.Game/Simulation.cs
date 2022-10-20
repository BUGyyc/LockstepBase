using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entitas;
using Lockstep.Common.Logging;
using Lockstep.Core.Logic;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization.Utils;
using Lockstep.Game.Features;

namespace Lockstep.Game
{

    public class Simulation
    {
        private float _tickDt;

        private float _accumulatedTime;

        private World _world;

        private readonly ICommandQueue _commandQueue;

        private readonly List<ICommand> _localCommandBuffer = new List<ICommand>();

        public Contexts Contexts { get; }

        public GameLog GameLog { get; } = new GameLog();


        public byte LocalActorId { get; private set; }

        public bool Running { get; private set; }

        public ServiceContainer Services { get; }

        public event EventHandler Started;

        public Simulation(Contexts contexts, ICommandQueue commandQueue, params IService[] services)
        {
            _commandQueue = commandQueue;
            Contexts = contexts;
            Services = new ServiceContainer();
            foreach (IService instance in services)
            {
                Services.Register(instance);
            }
        }

        public void Start(int targetFps, byte localActorId, byte[] allActors)
        {
            GameLog.LocalActorId = localActorId;
            GameLog.AllActorIds = allActors;
            LocalActorId = localActorId;
            _tickDt = 1000f / (float)targetFps;
            _world = new World(Contexts, allActors, new InputFeature(Contexts, Services), new RVONavigationFeature(Contexts, Services), new CleanupFeature(Contexts, Services));
            Running = true;
            this.Started?.Invoke(this, EventArgs.Empty);
        }

        public void Update(float elapsedMilliseconds)
        {
            if (!Running)
            {
                return;
            }
            _accumulatedTime += elapsedMilliseconds;
            while (_accumulatedTime >= _tickDt)
            {
                lock (_localCommandBuffer)
                {
                    _commandQueue.Enqueue(new Input(_world.Tick, LocalActorId, _localCommandBuffer.ToArray()));
                    _localCommandBuffer.Clear();
                    ProcessInputQueue();
                    _world.Predict();
                }
                _accumulatedTime -= _tickDt;
            }
        }

        public void Execute(ICommand command)
        {
            if (!Running)
            {
                return;
            }
            lock (_localCommandBuffer)
            {
                _localCommandBuffer.Add(command);
            }
        }

        public void DumpGameLog(Stream outputStream, bool closeStream = true)
        {
            Serializer serializer = new Serializer();
            serializer.Put(Contexts.gameState.hashCode.value);
            serializer.Put(Contexts.gameState.tick.value);
            outputStream.Write(serializer.Data, 0, serializer.Length);
            GameLog.WriteTo(outputStream);
            if (closeStream)
            {
                outputStream.Close();
            }
        }

        private void ProcessInputQueue()
        {
            List<Input> list = _commandQueue.Dequeue();
            if (!list.Any())
            {
                return;
            }
            foreach (Input item in list)
            {
                GameLog.Add(_world.Tick, item);
                foreach (ICommand command in item.Commands)
                {
                    Log.Trace(this, item.ActorId + " >> " + item.Tick + ": " + item.Commands.Count());
                    InputEntity inputEntity = ((Context<InputEntity>)Contexts.input).CreateEntity();
                    command.Execute(inputEntity);
                    inputEntity.AddTick(item.Tick);
                    inputEntity.AddActorId(item.ActorId);
                }
            }
            List<Input> source = list.Where((Input input) => input.ActorId != LocalActorId).ToList();
            if (!source.Any())
            {
                return;
            }
            uint num = source.Min((Input input) => input.Tick);
            uint num2 = source.Max((Input input) => input.Tick);
            Log.Trace(this, ">>>Input from " + num + " to " + num2);
            if (num < _world.Tick)
            {
                uint tick = _world.Tick;
                _world.RevertToTick(num);
                while (_world.Tick <= num2 && _world.Tick < tick)
                {
                    _world.Simulate();
                }
                while (_world.Tick < tick)
                {
                    _world.Predict();
                }
            }
        }
    }

}
