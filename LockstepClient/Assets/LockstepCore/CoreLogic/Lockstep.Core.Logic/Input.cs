using System.Collections.Generic;
using Lockstep.Core.Logic.Interfaces;

namespace Lockstep.Core.Logic
{
    public class Input
    {
        /// <summary>
        /// 帧号
        /// </summary>
        public uint Tick { get; }

        /// <summary>
        /// 输入者
        /// </summary>
        public byte ActorId { get; }

        /// <summary>
        /// 输入指令集合
        /// </summary>
        public IEnumerable<ICommand> Commands { get; }

        public Input(uint tick, byte actorId, IEnumerable<ICommand> commands)
        {
            Tick = tick;
            ActorId = actorId;
            Commands = commands;
        }

        public override string ToString()
        {
            return ActorId + " >> " + Tick + ": " + Commands.GetType().FullName;
        }
    }
}

