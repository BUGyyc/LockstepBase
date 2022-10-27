using Entitas;
using Lockstep.Game.Features.Navigation.Simple;

namespace Lockstep.Game.Features
{
    internal sealed class SimpleNavigationFeature : Feature
    {
        public SimpleNavigationFeature(Contexts contexts, ServiceContainer services)
            : base("SimpleNavigation")
        {
            ((Systems)this).Add((ISystem)(object)new NavigationTick(contexts, services));
        }
    }
}


