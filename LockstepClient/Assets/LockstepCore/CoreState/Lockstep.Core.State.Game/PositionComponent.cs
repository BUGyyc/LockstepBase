using BEPUutilities;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Lockstep.Core.State.Game
{
    [Game]
    //[Event(/*Could not decode attribute arguments.*/)]
    public class PositionComponent : IComponent
    {
        public Vector2 value;
    }
}


