namespace Lockstep.Game.Interfaces
{
    public interface IViewService : IService
    {
        void LoadView(GameEntity entity, uint configId);

        void LoadView(GameEntity entity, int configId, bool isMaster = false);

        void DeleteView(uint entityId);
    }
}


