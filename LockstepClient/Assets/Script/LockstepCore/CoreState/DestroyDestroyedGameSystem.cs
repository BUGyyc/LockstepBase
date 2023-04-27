using System.Collections.Generic;
using Entitas;

public sealed class DestroyDestroyedGameSystem : ICleanupSystem, ISystem
{
    private readonly IGroup<GameEntity> _group;

    private readonly List<GameEntity> _buffer = new List<GameEntity>();

    public DestroyDestroyedGameSystem(Contexts contexts)
    {
        _group = (contexts.game).GetGroup(GameMatcher.Destroyed);
    }

    /// <summary>
    /// 把标记好的 GameEntity 进行销毁
    /// </summary>
    public void Cleanup()
    {
        
        foreach (GameEntity entity in _group.GetEntities(_buffer))
        {
            (entity).Destroy();
        }
    }
}
