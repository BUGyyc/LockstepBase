using System.Collections.Generic;
using System.Linq;
using Entitas;
using Lockstep.Common.Logging;

namespace Lockstep.Game.Features.Input
{

    public class VerifySelectionIdExists : IExecuteSystem, ISystem
    {
        private readonly GameContext _gameContext;

        private readonly InputContext _inputContext;

        private readonly GameStateContext _gameStateContext;

        public VerifySelectionIdExists(Contexts contexts)
        {
            _gameContext = contexts.game;
            _inputContext = contexts.input;
            _gameStateContext = contexts.gameState;
        }

        public void Execute()
        {
            foreach (InputEntity item in from entity in ContextExtension.GetEntities<InputEntity>((IContext<InputEntity>)(object)_inputContext, (IMatcher<InputEntity>)(object)InputMatcher.AllOf(InputMatcher.Tick, InputMatcher.Coordinate, InputMatcher.Selection, InputMatcher.ActorId))
                                         where entity.tick.value < _gameStateContext.tick.value
                                         select entity)
            {
                byte targetActorId = (item.hasTargetActorId ? item.targetActorId.value : item.actorId.value);
                IEnumerable<uint> ents = from entity in ContextExtension.GetEntities<GameEntity>((IContext<GameEntity>)(object)_gameContext, GameMatcher.LocalId)
                                         where entity.actorId.value == targetActorId
                                         select entity.id.value;
                List<uint> list = item.selection.entityIds.Where((uint u) => !ents.Contains(u)).ToList();
                if (list.Any())
                {
                    Log.Warn(this, list.Count + " missing for actor: " + targetActorId + " (command from " + item.actorId.value + ")");
                }
            }
        }
    }
}
