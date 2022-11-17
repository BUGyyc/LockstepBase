/*
 * @Author: delevin.ying 
 * @Date: 2022-11-17 15:22:43 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-17 16:43:44
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
public class SkillSystem : IExecuteSystem, ISystem
{


    private readonly GameContext _gameContext;

    private readonly GameStateContext _gameStateContext;



    private readonly IGroup<GameEntity> _skill;


    public SkillSystem(Contexts contexts)
    {
        _skill = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Skill));
    }

    public void Execute()
    {
        var tick = Contexts.sharedInstance.gameState.tick.value;
        foreach (var entity in _skill.GetEntities())
        {
            uint skillId = entity.skill.skillId;

            if (skillId == 0) continue;

            uint lastTick = entity.skill.lastStartTick;

            if (tick - lastTick >= 10)
            {
                entity.skill.lastStartTick = tick;

                TickFire(entity);
            }

        }
    }

    private void TickFire(GameEntity entity)
    {
        var entityId = entity.actorId.value;

        //TODO:这里的创建可能需要挪到Command,因为子弹也是Entity

        EntityUtil.CreateBulletEntity(entityId);

    }
}

