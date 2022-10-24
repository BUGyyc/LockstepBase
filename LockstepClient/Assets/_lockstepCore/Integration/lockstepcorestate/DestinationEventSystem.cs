using System.Collections.Generic;
using Entitas;
using Lockstep.Core.State.Game;

public sealed class DestinationEventSystem : ReactiveSystem<GameEntity>
{
	private readonly List<IDestinationListener> _listenerBuffer;

	public DestinationEventSystem(Contexts contexts)
		: base((IContext<GameEntity>)(object)contexts.game)
	{
		_listenerBuffer = new List<IDestinationListener>();
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return CollectorContextExtension.CreateCollector<GameEntity>(context, new TriggerOnEvent<GameEntity>[1] { TriggerOnEventMatcherExtension.Added<GameEntity>(GameMatcher.Destination) });
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.hasDestination && entity.hasDestinationListener;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity entity in entities)
		{
			DestinationComponent destination = entity.destination;
			_listenerBuffer.Clear();
			_listenerBuffer.AddRange(entity.destinationListener.value);
			foreach (IDestinationListener item in _listenerBuffer)
			{
				item.OnDestination(entity, destination.value);
			}
		}
	}
}
