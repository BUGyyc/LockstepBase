using Entitas;
using Lockstep.Game.Features.Input;

namespace Lockstep.Game.Features
{
    internal sealed class InputFeature : Feature
    {
        public InputFeature(Contexts contexts, ServiceContainer services)
            : base("Input")
        {
            Add(new ExecuteSpawnInput(contexts, services));
            Add(new ExecuteNavigationInput(contexts, services));


            Add(new ExecuteCharacterInput(contexts, services));

        }
    }
}


