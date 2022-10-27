using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Utils;
using Entitas;
using Lockstep.Common.Logging;

namespace Lockstep.Core.Logic.Systems.GameState
{

    public class OnNewPredictionCreateSnapshot : ReactiveSystem<GameStateEntity>
    {
        private readonly GameContext _gameContext;

        private readonly ActorContext _actorContext;

        private readonly SnapshotContext _snapshotContext;

        private readonly GameStateContext _gameStateContext;

        private readonly IGroup<ActorEntity> _activeActors;

        private readonly IGroup<GameEntity> _activeEntities;

        public OnNewPredictionCreateSnapshot(Contexts contexts)
            : base((IContext<GameStateEntity>)(object)contexts.gameState)
        {
            _gameContext = contexts.game;
            _actorContext = contexts.actor;
            _snapshotContext = contexts.snapshot;
            _gameStateContext = contexts.gameState;
            _activeActors = (contexts.actor).GetGroup(ActorMatcher.Id);
            _activeEntities = (contexts.game).GetGroup(GameMatcher.LocalId);
        }

        protected override ICollector<GameStateEntity> GetTrigger(IContext<GameStateEntity> context)
        {
            //IL_000f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0014: Unknown result type (might be due to invalid IL or missing references)
            return CollectorContextExtension.CreateCollector<GameStateEntity>(context, new TriggerOnEvent<GameStateEntity>[1] { TriggerOnEventMatcherExtension.Added<GameStateEntity>(GameStateMatcher.Predicting) });
        }

        protected override bool Filter(GameStateEntity gameState)
        {
            return gameState.isPredicting;
        }

        protected override void Execute(List<GameStateEntity> entities)
        {
            uint value = _gameStateContext.tick.value;
            (_snapshotContext).CreateEntity().AddTick(value);
            foreach (GameEntity activeEntity in _activeEntities)
            {
                GameEntity gameEntity = (_gameContext).CreateEntity();
                foreach (int item in (activeEntity).GetComponentIndices().Except(new int[1] { 10 }))
                {
                    IComponent component = (activeEntity).GetComponent(item);
                    IComponent val = (gameEntity).CreateComponent(item, ((object)component).GetType());
                    PublicMemberInfoExtension.CopyPublicMemberValues((object)component, (object)val);
                    (gameEntity).AddComponent(item, val);
                }
                gameEntity.AddBackup(activeEntity.localId.value, value);
            }
            foreach (ActorEntity activeActor in _activeActors)
            {
                ActorEntity actorEntity = ((Context<ActorEntity>)_actorContext).CreateEntity();
                foreach (int item2 in (activeActor).GetComponentIndices().Except(new int[1] { 2 }))
                {
                    IComponent component2 = (activeActor).GetComponent(item2);
                    IComponent val2 = (actorEntity).CreateComponent(item2, ((object)component2).GetType());
                    PublicMemberInfoExtension.CopyPublicMemberValues((object)component2, (object)val2);
                    (actorEntity).AddComponent(item2, val2);
                }
                actorEntity.AddBackup(activeActor.id.value, value);
            }
            Log.Trace(this, "New snapshot for " + value + "(" + _activeActors.count + " actors, " + ((IGroup)_activeEntities).count + " entities)");
        }
    }
}
