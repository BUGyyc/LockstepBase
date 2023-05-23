﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entitas;
using Lockstep.Common.Logging;
using Lockstep.Core.Logic;
using Lockstep.Core.Logic.Interfaces;
using Lockstep.Core.Logic.Serialization.Utils;
using Lockstep.Game.Features;
using Lockstep.Network.Client;
//using UnityEngine;

namespace Lockstep.Game
{
    /// <summary>
    /// 模拟器，驱动帧执行
    /// </summary>
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

        //#if UNITY_EDITOR
        public World GetWorld()
        {
            return _world;
        }
        //#endif

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

        /// <summary>
        ///
        /// </summary>
        /// <param name="targetFps">指定帧号</param>
        /// <param name="localActorId">本地玩家 ID</param>
        /// <param name="allActors">房间内所有玩家ID</param>
        public void Start(int targetFps, byte localActorId, byte[] allActors)
        {
            GameLog.LocalActorId = localActorId;
            GameLog.AllActorIds = allActors;
            LocalActorId = localActorId;
            _tickDt = 1000f / (float)targetFps;
            _world = new World(
                Contexts,
                allActors,
                new InputFeature(Contexts, Services),
                new BattleLogicFeature(Contexts, Services),
                new RVONavigationFeature(Contexts, Services),
                new CleanupFeature(Contexts, Services)
            );
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
                    var cmdArr = _localCommandBuffer.ToArray();

                    //即使是空的 Cmd , 依然会发送
                    //NOTE: 本地命令立即入队，并且发送给服务器
                    _commandQueue.Enqueue(
                        new Input(_world.Tick, LocalActorId, cmdArr)
                    );

                    //NOTE: 待验证的 CheckHashCode
                    //var hashCode = Contexts.sharedInstance.gameState.hashCode.value;
                    //(_commandQueue as NetworkCommandQueue).SendHashCode(_world.Tick, hashCode);

                    //LogMaster.L($"Update {_world.Tick}   {UnityEngine.Time.realtimeSinceStartup} ");

                    _localCommandBuffer.Clear();
                    //NOTE: 处理队列中的输入，只处理非本地输入
                    ProcessInputQueue();
                    //NOTE: 预测？？ 只执行本地输入
                    _world.Predict();
                }
                _accumulatedTime -= _tickDt;
            }
        }

        /// <summary>
        /// 添加指令到执行队列
        /// </summary>
        /// <param name="command"></param>
        public void Execute(ICommand command)
        {
            if (!Running)
            {
                return;
            }

            lock (_localCommandBuffer)
            {
                if (RecordSetting.HasRecord)
                {
                    RecordManager.Instance.AddInputCommand(command);
                }

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
                    //把Command 创建为 InputEntity

                    InputEntity inputEntity = ((Context<InputEntity>)Contexts.input).CreateEntity();
                    command.Execute(inputEntity);
                    inputEntity.AddTick(item.Tick);
                    inputEntity.AddActorId(item.ActorId);
                }
            }
            //本地存储中不属于自己的输入？？？
            List<Input> source = list.Where((Input input) => input.ActorId != LocalActorId)
                .ToList();
            if (!source.Any())
            {
                return;
            }

            //NOTE: 以下都是模拟对方的指令，不执行本地输入
            uint num = source.Min((Input input) => input.Tick);
            uint num2 = source.Max((Input input) => input.Tick);

            if (num < _world.Tick)
            {
                LogMaster.L(
                    "Simulator  netInput  RollBack frame: "
                        + num
                        + "    Input minTick: "
                        + num
                        + " maxTick: "
                        + num2
                );

                uint tick = _world.Tick;
                _world.RevertToTick(num);
                while (_world.Tick <= num2 && _world.Tick < tick)
                {
                    LogMaster.L("加速追----》" + _world.Tick);
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