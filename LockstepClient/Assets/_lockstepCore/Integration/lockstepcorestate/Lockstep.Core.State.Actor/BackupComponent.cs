using Entitas;

namespace Lockstep.Core.State.Actor
{
    [Actor]
    public class BackupComponent : IComponent
    {
        public byte actorId;

        public uint tick;
    }
}


