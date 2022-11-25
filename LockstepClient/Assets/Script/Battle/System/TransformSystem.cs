using Entitas;
using System.Collections.Generic;
using System.Linq;
// using BEPUutilities;
// using FixMath.NET;
using Lockstep.Common.Logging;
using Lockstep.Game.Interfaces;
using Lockstep.Game;
using UnityEngine;

public class TransformSystem : IExecuteSystem, ISystem
{
    private readonly IGroup<GameEntity> _gameEntities;
    public TransformSystem(Contexts contexts)
    {

        _gameEntities = contexts.game.GetGroup((IMatcher<GameEntity>)(object)GameMatcher.AllOf(GameMatcher.Position, GameMatcher.LocalId));
    }


    public void Execute()
    {
        foreach (GameEntity entity in _gameEntities.GetEntities())
        {
            var position = entity.position;
            var positionListener = entity.positionListener;
            foreach (var listener in positionListener.value)
            {
                listener.OnPosition(entity, position.value, position.rotate);
            }
        }
    }
}

