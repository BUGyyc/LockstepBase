using System.Collections.Generic;
// using BEPUutilities;
using Entitas;
// using FixMath.NET;
using Lockstep.Core.State.Game;
using Lockstep;

/// <summary>
/// 游戏对象Entity
/// </summary>
public sealed partial class GameEntity : Entity
{
    public void AddPositionListener(IPositionListener value)
    {
        List<IPositionListener> list = (hasPositionListener ? positionListener.value : new List<IPositionListener>());
        list.Add(value);
        ReplacePositionListener(list);
    }

    public void RemovePositionListener(IPositionListener value, bool removeComponentWhenEmpty = true)
    {
        List<IPositionListener> value2 = positionListener.value;
        value2.Remove(value);
        if (removeComponentWhenEmpty && value2.Count == 0)
        {
            RemovePositionListener();
        }
        else
        {
            ReplacePositionListener(value2);
        }
    }
}
