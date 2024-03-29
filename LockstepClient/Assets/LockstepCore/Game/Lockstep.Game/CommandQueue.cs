﻿using System.Collections.Generic;
using System.Linq;
using Lockstep.Core.Logic;
using Lockstep.Core.Logic.Interfaces;

namespace Lockstep.Game
{

    public class CommandQueue : ICommandQueue
    {
        //TODO:如果用了多线程，Buffer容易阻塞，应该用双Buffer交替，防止阻塞
        /// <summary>
        /// 存储每一帧的命令队列
        /// </summary>
        public Dictionary<uint, List<Input>> Buffer { get; } = new Dictionary<uint, List<Input>>(5000);


        public void Enqueue(uint tick, byte actorId, params ICommand[] commands)
        {
            Enqueue(new Input(tick, actorId, commands));
        }

        public virtual void Enqueue(Input input)
        {
            lock (Buffer)
            {
                if (!Buffer.ContainsKey(input.Tick))
                {
                    Buffer.Add(input.Tick, new List<Input>(10));
                }
                Buffer[input.Tick].Add(input);
            }
        }

        public virtual List<Input> Dequeue()
        {
            lock (Buffer)
            {
                List<Input> result = Buffer.SelectMany((KeyValuePair<uint, List<Input>> pair) => pair.Value).ToList();
                Buffer.Clear();
                return result;
            }
        }
    }
}
