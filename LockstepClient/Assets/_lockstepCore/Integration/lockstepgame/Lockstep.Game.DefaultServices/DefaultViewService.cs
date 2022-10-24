using Lockstep.Game.Interfaces;

namespace Lockstep.Game.DefaultServices
{
    public class DefaultViewService : Lockstep.Game.Interfaces.IViewService, IService
    {
        public void LoadView(GameEntity entity, int configId)
        {
            entity.isNavigable = true;
        }

        public void DeleteView(uint entityId)
        {
        }
    }
}


