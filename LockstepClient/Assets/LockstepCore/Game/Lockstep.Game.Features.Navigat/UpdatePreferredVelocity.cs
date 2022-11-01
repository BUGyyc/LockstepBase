using BEPUutilities;
using Entitas;
using FixMath.NET;

namespace Lockstep.Game.Features.Navigation.RVO
{

    internal class UpdatePreferredVelocity : IExecuteSystem, ISystem
    {
        private readonly Contexts _contexts;

        private readonly IGroup<GameEntity> movingEntities;

        public UpdatePreferredVelocity(Contexts contexts, ServiceContainer services)
        {
            _contexts = contexts;
            movingEntities = ((Context<GameEntity>)_contexts.game).GetGroup((IMatcher<GameEntity>)(object)GameMatcher.AllOf(GameMatcher.LocalId, GameMatcher.RvoAgentSettings));
        }

        public void Execute()
        {
            //TODO:
            //foreach (GameEntity movingEntity in movingEntities)
            //{
            //    Vector2 preferredVelocity = movingEntity.destination.value - movingEntity.position.value;
            //    if (preferredVelocity.LengthSquared() > Fix64.One)
            //    {
            //        preferredVelocity.Normalize();
            //    }
            //    movingEntity.rvoAgentSettings.preferredVelocity = preferredVelocity;
            //}
        }
    }
}