using Entitas;
using System.Collections.Generic;
using System.Linq;
using BEPUutilities;
using FixMath.NET;
using Lockstep.Common.Logging;
using Lockstep.Game.Interfaces;
using Lockstep.Game;
using UnityEngine;

public class MoveSystem : IExecuteSystem, ISystem
{
    private readonly IGroup<GameEntity> _gameEntities;
    public MoveSystem(Contexts contexts)
    {

        _gameEntities = contexts.game.GetGroup((IMatcher<GameEntity>)(object)GameMatcher.AllOf(GameMatcher.Position));
    }


    public void Execute()
    {
        foreach (GameEntity entity in _gameEntities.GetEntities())
        {
            if (entity.hasBackup) continue;
            var position = entity.position;
            var positionListener = entity.positionListener;
            foreach (var listener in positionListener.value)
            {
                listener.OnPosition(entity, position.value, position.rotate);
            }
        }

        // for (var i = 0; i < entitys.Length; i++)
        // {
        //     var entity = entitys[i];
        //     if (entity.hasBackup) continue;
        //     var position = entity.position;
        //     var positionListener = entitys[i].positionListener;
        //     foreach (var listener in positionListener.value)
        //     {
        //         listener.OnPosition(entity, position.value);
        //     }
        // }
    }
}

