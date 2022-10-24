using System;

namespace Lockstep.Network.Server
{
    public class StartedEventArgs : EventArgs
    {
        public int SimulationSpeed { get; set; }

        public byte[] ActorIds { get; set; }

        public StartedEventArgs(int simulationSpeed, byte[] actorIds)
        {
            SimulationSpeed = simulationSpeed;
            ActorIds = actorIds;
        }
    }
}

