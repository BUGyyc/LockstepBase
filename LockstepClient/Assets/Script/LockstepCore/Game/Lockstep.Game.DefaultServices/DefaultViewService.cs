using Lockstep.Game.Interfaces;

namespace Lockstep.Game.DefaultServices
{
    public class DefaultViewService : Lockstep.Game.Interfaces.IViewService, IService
    {
        public void LoadView(GameEntity entity, int configId, bool isMaster = false)
        {
            entity.isNavigable = true;
        }

        public void DeleteView(uint entityId)
        {
        }

        public void LoadView(GameEntity entity, uint configId)
        {
            //throw new System.NotImplementedException();
        }
    }
}


