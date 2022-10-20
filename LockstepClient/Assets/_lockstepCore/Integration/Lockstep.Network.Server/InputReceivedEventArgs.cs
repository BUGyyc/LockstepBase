using System;

namespace Lockstep.Network.Server
{
    public class InputReceivedEventArgs : EventArgs
    {
        public byte ActorId { get; set; }

        public uint Tick { get; set; }

        public InputReceivedEventArgs(byte actorId, uint tick)
        {
            ActorId = actorId;
            Tick = tick;
        }
    }
}

