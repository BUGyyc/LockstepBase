using Entitas;
// using Lockstep.Game.Features.Navigation.RVO;

namespace Lockstep.Game.Features
{
    internal sealed class RVONavigationFeature : Feature
    {
        public RVONavigationFeature(Contexts contexts, ServiceContainer services)
            : base("RVONavigation")
        {
            // ((Systems)this).Add((ISystem)(object)new NavigationTick(contexts, services));
        }
    }
}


