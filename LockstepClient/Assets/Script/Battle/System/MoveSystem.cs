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
    public void Execute()
    {
        var entitys = Contexts.sharedInstance.game.GetEntities();
        for (var i = 0; i < entitys.Length; i++)
        {
            var entity = entitys[i];
            var position = entity.position;
            var positionListener = entitys[i].positionListener;
            foreach (var listener in positionListener.value)
            {
                listener.OnPosition(entity, position.value);
            }
        }
    }
}

