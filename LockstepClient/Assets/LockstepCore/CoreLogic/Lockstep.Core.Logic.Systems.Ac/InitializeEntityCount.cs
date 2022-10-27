using Entitas;

namespace Lockstep.Core.Logic.Systems.Actor
{
    /// <summary>
    /// ？？初始化玩家
    /// </summary>
    public class InitializeEntityCount : IInitializeSystem, ISystem
    {
        private readonly ActorContext _actorContext;

        public InitializeEntityCount(Contexts contexts)
        {
            _actorContext = contexts.actor;
        }

        public void Initialize()
        {
            ActorEntity[] entities = (_actorContext).GetEntities();
            foreach (ActorEntity actorEntity in entities)
            {
                actorEntity.AddEntityCount(0u);
            }
        }
    }
}


