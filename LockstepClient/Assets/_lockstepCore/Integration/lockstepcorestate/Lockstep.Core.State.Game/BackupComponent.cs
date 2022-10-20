using Entitas;

namespace Lockstep.Core.State.Game
{
    [Game]
    public class BackupComponent : IComponent
    {
        public uint localEntityId;

        public uint tick;
    }
}


