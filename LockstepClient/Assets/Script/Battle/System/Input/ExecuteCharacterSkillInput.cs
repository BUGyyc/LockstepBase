/*
 * @Author: delevin.ying
 * @Date: 2022-11-17 16:35:17
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-17 16:41:23
 */


using System.Collections.Generic;
using System.Linq;
// using BEPUutilities;
using Entitas;
// using FixMath.NET;
// using Lockstep.Common.Logging;
using Lockstep.Game.Interfaces;
using Lockstep.Game;
//using static UnityEngine.EventSystems.EventTrigger;
using Lockstep.Game.Commands;

// using Protocol;
// using Unity.VisualScripting;
// using System;

public class ExecuteCharacterSkillInput : IExecuteSystem, ISystem
{
    private readonly IViewService _viewService;

    private readonly GameContext _gameContext;

    private readonly GameStateContext _gameStateContext;

    private readonly IGroup<InputEntity> _skillInputs;

    public ExecuteCharacterSkillInput(Contexts contexts, ServiceContainer serviceContainer)
    {
        _viewService = serviceContainer.Get<IViewService>();
        _gameContext = contexts.game;
        _gameStateContext = contexts.gameState;
        _skillInputs = contexts.input.GetGroup(
            InputMatcher.AllOf(InputMatcher.ActorId, InputMatcher.Tick, InputMatcher.SkillInput)
        );
    }

    public void Execute()
    {
        //晒选指定帧号，生成对应的Entity
        foreach (
            InputEntity item in from entity in _skillInputs.GetEntities()
                                where entity.tick.value == _gameStateContext.tick.value
                                select entity
        )
        {
            var leftMousePressed = item.skillInput.leftMousePressed;
            var entityId = item.skillInput.entityId;
            var gameEntity = _gameContext.GetEntityWithLocalId(
                entityId + EntityUtil.BaseCharacterEntityID
            );

            if (gameEntity == null)
            {
                UnityEngine.Debug.LogError(
                    $"Entity 不存在  {entityId + EntityUtil.BaseCharacterEntityID}  "
                );
                continue;
            }

            if (gameEntity.hasLife)
            {
                if (gameEntity.life.Dead)
                {
                    continue;
                }
            }

            uint skillId = item.skillInput.skillId;

            var dir = item.skillInput.shootDir;

            var pos = gameEntity.position.value + dir;

            if (skillId == 0) continue;



            LogMaster.L($"Shoot pos: {pos} dir: {dir}");


            //var entityData = 

            //EntityData data = new EntityData();

            //byte[] bs = ProtocolHelper.Instance.SerializableData(data);

            var bs = EntityUtil.CreateBulletData(dir);



            //ProtoBufSerializer.Serialize(msSend, tmp);

            //Serializer serializer = new Serializer();

            //var d = data.Serialize();
            //d.

            //data.

            //ProtoWrite


            //Serialization.Serialize



            ActionWorld.Instance.Execute(new SpawnCommand
            {
                Position = pos,
                // entityData = bs
            });


            //var entityId = item.actorId.value;

            //TickFire(entity, entity.skill.shootDir);




            //gameEntity.skill.skillId = item.skillInput.skillId;

            //gameEntity.skill.shootSkill = true;

            //gameEntity.skill.shootDir = item.skillInput.shootDir;



            // var speed = item.characterInput.moveDir;

            // var entityId = item.characterInput.entityId;

            // var actor = item.actorId.value;

            // // UnityEngine.Debug.Log($"<color=yellow>玩家输入 actor {actor}  speed {speed}  entityId {entityId}  </color>");

            // var gameEntity = _gameContext.GetEntityWithLocalId(entityId + EntityUtil.BaseCharacterEntityID);

            // if (gameEntity == null)
            // {
            //     UnityEngine.Debug.LogError($"Entity 不存在  {entityId + EntityUtil.BaseCharacterEntityID}  ");
            //     return;
            // }

            // gameEntity.ReplaceCharacterInput(entityId, speed, item.characterInput.viewDir);
        }
    }
}
