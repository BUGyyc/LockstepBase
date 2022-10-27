using Entitas;
using Lockstep.Game.Features.Cleanup;

namespace Lockstep.Game.Features
{
    internal sealed class CleanupFeature : Feature
    {
        public CleanupFeature(Contexts contexts, ServiceContainer services)
            : base("Cleanup")
        {
            ((Systems)this).Add((ISystem)(object)new RemoveDestroyedEntitiesFromView(contexts, services));
        }
    }
}


