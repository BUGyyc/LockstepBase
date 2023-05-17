// using BEPUutilities;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Lockstep;

namespace Lockstep.Core.State.Game
{

    [Game]
    //[Event(/*Could not decode attribute arguments.*/)]
    public class DestinationComponent : IComponent
    {
        //public Vector2 value;
        public LVector3 value;
    }

}
