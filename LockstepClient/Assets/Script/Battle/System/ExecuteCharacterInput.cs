using System.Collections.Generic;
using System.Linq;
using BEPUutilities;
using Entitas;
using FixMath.NET;
using Lockstep.Common.Logging;
using Lockstep.Game.Interfaces;
using Lockstep.Game;



public class ExecuteCharacterInput : IExecuteSystem, ISystem
{
    private readonly IViewService _viewService;

    private readonly GameContext _gameContext;

    private readonly GameStateContext _gameStateContext;

    private readonly IGroup<InputEntity> _characterInputs;

    //private uint _localIdCounter;

    //private readonly ActorContext _actorContext;

    public ExecuteCharacterInput(Contexts contexts, ServiceContainer serviceContainer)
    {
        _viewService = serviceContainer.Get<IViewService>();
        _gameContext = contexts.game;
        _gameStateContext = contexts.gameState;
        //_actorContext = contexts.actor;
        _characterInputs = contexts.input.GetGroup(InputMatcher.AllOf(InputMatcher.ActorId, InputMatcher.Tick, InputMatcher.CharacterInput));
    }

    public void Execute()
    {
        //晒选指定帧号，生成对应的Entity
        foreach (InputEntity item in from entity in _characterInputs.GetEntities()
                                     where entity.tick.value == _gameStateContext.tick.value
                                     select entity)
        {
            var speed = item.characterInput.moveSpeed;

            var entityId = item.characterInput.entityId;

            var actor = item.actorId.value;

            UnityEngine.Debug.Log($"<color=yellow>玩家输入 actor {actor}  speed {speed}  entityId {entityId}  </color>");

            //var gameEntity = _gameContext.GetEntityWithLocalId(entityId);

            //gameEntity.position.value += speed;
        }
    }
}

