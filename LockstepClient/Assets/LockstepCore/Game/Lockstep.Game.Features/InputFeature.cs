using Entitas;
using Lockstep.Game.Features.Input;

namespace Lockstep.Game.Features
{
    internal sealed class InputFeature : Feature
    {
        public InputFeature(Contexts contexts, ServiceContainer services)
            : base("Input")
        {
            ((Systems)this).Add((ISystem)(object)new ExecuteSpawnInput(contexts, services));
            ((Systems)this).Add((ISystem)(object)new ExecuteNavigationInput(contexts, services));
        }
    }
}


