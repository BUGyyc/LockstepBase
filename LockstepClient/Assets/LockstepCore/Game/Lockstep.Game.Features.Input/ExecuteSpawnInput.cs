using System.Collections.Generic;
using System.Linq;
using BEPUutilities;
using Entitas;
using FixMath.NET;
using Lockstep.Common.Logging;
using Lockstep.Game.Interfaces;

namespace Lockstep.Game.Features.Input
{

    public class ExecuteSpawnInput : IExecuteSystem, ISystem
    {
        private readonly IViewService _viewService;

        private readonly GameContext _gameContext;

        private readonly GameStateContext _gameStateContext;

        private readonly IGroup<InputEntity> _spawnInputs;

        private uint _localIdCounter;

        private readonly ActorContext _actorContext;

        public ExecuteSpawnInput(Contexts contexts, ServiceContainer serviceContainer)
        {
            _viewService = serviceContainer.Get<IViewService>();
            _gameContext = contexts.game;
            _gameStateContext = contexts.gameState;
            _actorContext = contexts.actor;
            _spawnInputs = ((Context<InputEntity>)contexts.input).GetGroup((IMatcher<InputEntity>)(object)InputMatcher.AllOf(InputMatcher.EntityConfigId, InputMatcher.ActorId, InputMatcher.Coordinate, InputMatcher.Tick));
        }

        public void Execute()
        {
            //晒选指定帧号，生成对应的Entity
            foreach (InputEntity item in from entity in _spawnInputs.GetEntities()
                                         where entity.tick.value == _gameStateContext.tick.value
                                         select entity)
            {
                //本地 Actor 
                ActorEntity entityWithId = _actorContext.GetEntityWithId(item.actorId.value);
                //本地 Actor 所关联的 Entity 数量
                uint value = entityWithId.entityCount.value;
                GameEntity gameEntity = _gameContext.CreateEntity();

                UnityEngine.Debug.Log($"<color=yellow> [ExecuteSpawnInput]   Input ActorId {item.actorId.value}  _localIdCounter {_localIdCounter}     </color> ");


                //UnityEngine.Debug.Log("[ExecuteSpawnInput]    " + entityWithId.id.value + " -> " + value);
                gameEntity.AddId(value);
                gameEntity.AddActorId(item.actorId.value);
                gameEntity.AddLocalId(_localIdCounter);
                gameEntity.AddVelocity(Vector2.Zero);
                gameEntity.AddPosition(item.coordinate.value);

                //初始动画
                AnimationComponent animation = new AnimationComponent()
                {
                    readyPlay = true,
                    animationName = "Idle_Wait_C"
                };

                gameEntity.AddComponent(GameComponentsLookup.Animation, animation);


                _viewService.LoadView(gameEntity, item.entityConfigId.value);
                if (gameEntity.isNavigable)
                {
                    gameEntity.AddRadius(F64.C1);
                    gameEntity.AddMaxSpeed(F64.C2);
                    gameEntity.AddRvoAgentSettings(Vector2.Zero, 5, new List<KeyValuePair<Fix64, uint>>());
                }
                //记录Entity数量
                entityWithId.ReplaceEntityCount(value + 1);

                _localIdCounter++;
            }
        }
    }
}
