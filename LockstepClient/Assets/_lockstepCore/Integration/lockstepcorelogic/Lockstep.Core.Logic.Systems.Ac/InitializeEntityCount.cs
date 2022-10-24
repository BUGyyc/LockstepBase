using Entitas;

namespace Lockstep.Core.Logic.Systems.Actor
{
    public class InitializeEntityCount : IInitializeSystem, ISystem
    {
        private readonly ActorContext _actorContext;

        public InitializeEntityCount(Contexts contexts)
        {
            _actorContext = contexts.actor;
        }

        public void Initialize()
        {
            ActorEntity[] entities = ((Context<ActorEntity>)_actorContext).GetEntities();
            foreach (ActorEntity actorEntity in entities)
            {
                actorEntity.AddEntityCount(0u);
            }
        }
    }
}


