using System.Collections.Generic;
using Entitas;

public sealed class DestroyDestroyedGameSystem : ICleanupSystem, ISystem
{
	private readonly IGroup<GameEntity> _group;

	private readonly List<GameEntity> _buffer = new List<GameEntity>();

	public DestroyDestroyedGameSystem(Contexts contexts)
	{
		_group = ((Context<GameEntity>)contexts.game).GetGroup(GameMatcher.Destroyed);
	}

	public void Cleanup()
	{
		foreach (GameEntity entity in _group.GetEntities(_buffer))
		{
			((Entity)entity).Destroy();
		}
	}
}
