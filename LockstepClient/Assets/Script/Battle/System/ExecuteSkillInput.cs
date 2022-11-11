/*
 * @Author: delevin.ying 
 * @Date: 2022-11-03 17:37:09 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-11 18:11:50
 */
using System.Collections.Generic;
using System.Linq;
using BEPUutilities;
using Entitas;
using FixMath.NET;
using Lockstep.Common.Logging;
using Lockstep.Game.Interfaces;
using Lockstep.Game;


/// <summary>
/// 处理玩家输入
/// </summary>
public class ExecuteSkillInput : IExecuteSystem, ISystem
{
    private readonly IViewService _viewService;

    private readonly GameContext _gameContext;

    private readonly GameStateContext _gameStateContext;



    private readonly IGroup<InputEntity> _skillInputs;

    //private uint _localIdCounter;

    //private readonly ActorContext _actorContext;

    public ExecuteSkillInput(Contexts contexts, ServiceContainer serviceContainer)
    {
        _viewService = serviceContainer.Get<IViewService>();
        _gameContext = contexts.game;
        _gameStateContext = contexts.gameState;
        //_actorContext = contexts.actor;
        _skillInputs = contexts.input.GetGroup(InputMatcher.AllOf(InputMatcher.ActorId, InputMatcher.Tick, InputMatcher.SkillInput));
    }

    public void Execute()
    {
        //晒选指定帧号，生成对应的Entity
        foreach (InputEntity item in from entity in _skillInputs.GetEntities()
                                     where entity.tick.value == _gameStateContext.tick.value
                                     select entity)
        {
            var skillID = item.SkillInput.skillId;
            var entityId = item.SkillInput.entityId;
            UnityEngine.Debug.LogError($"Entity {entityId} 释放技能 {skillID} ");

            var gameEntity = _gameContext.GetEntityWithLocalId(entityId + EntityUtil.BaseCharacterEntityID);

            if (gameEntity == null)
            {
                UnityEngine.Debug.LogError($"Entity 不存在  {entityId + EntityUtil.BaseCharacterEntityID}  ");
                return;
            }

            if (gameEntity.hasAnimation)
            {
                // gameEntity.Animation.
                gameEntity.Animation.state = AnimationMainState.Skill;
                gameEntity.PlayAnimation(gameEntity.Animation, "Attack_Hand_1R_Root");

            }

            // var speed = item.characterInput.moveSpeed;

            // var entityId = item.characterInput.entityId;

            // var actor = item.actorId.value;

            // // UnityEngine.Debug.Log($"<color=yellow>玩家输入 actor {actor}  speed {speed}  entityId {entityId}  </color>");

            // var gameEntity = _gameContext.GetEntityWithLocalId(entityId + EntityUtil.BaseCharacterEntityID);

            // if (gameEntity == null)
            // {
            //     UnityEngine.Debug.LogError($"Entity 不存在  {entityId + EntityUtil.BaseCharacterEntityID}  ");
            //     return;
            // }

            // // gameEntity.position.value += speed;

            // gameEntity.Animation.inputParams = speed;
            // gameEntity.Animation.originInput = item.characterInput.inputOriginData;
        }
    }
}

