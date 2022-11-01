using System.Collections.Generic;
using System.Linq;
using BEPUutilities;
using Entitas;
using Lockstep.Common.Logging;

namespace Lockstep.Game.Features.Input
{

    public class ExecuteNavigationInput : IExecuteSystem, ISystem
    {
        private readonly GameContext _gameContext;

        private readonly IGroup<InputEntity> _navigationInput;

        private readonly GameStateContext _gameStateContext;

        public ExecuteNavigationInput(Contexts contexts, ServiceContainer serviceContainer)
        {
            _gameContext = contexts.game;
            _gameStateContext = contexts.gameState;
            _navigationInput = ((Context<InputEntity>)contexts.input).GetGroup((IMatcher<InputEntity>)(object)InputMatcher.AllOf(InputMatcher.Coordinate, InputMatcher.Selection, InputMatcher.ActorId, InputMatcher.Tick));
        }

        public void Execute()
        {
            //foreach (InputEntity input in from entity in _navigationInput.GetEntities()
            //                              where entity.tick.value == _gameStateContext.tick.value
            //                              select entity)
            //{
            //    Vector2 value = input.coordinate.value;
            //    byte targetActorId = (input.hasTargetActorId ? input.targetActorId.value : input.actorId.value);
            //    IEnumerable<GameEntity> enumerable = from entity in ContextExtension.GetEntities<GameEntity>((IContext<GameEntity>)(object)_gameContext, GameMatcher.LocalId)
            //                                         where input.selection.entityIds.Contains(entity.id.value) && entity.actorId.value == targetActorId
            //                                         select entity;
            //    Log.Trace(this, targetActorId + " moving " + string.Join(", ", enumerable.Select((GameEntity entity) => entity.id.value)));
            //    foreach (GameEntity item in enumerable)
            //    {
            //        item.ReplaceDestination(value);
            //    }
            //}
        }
    }
}
