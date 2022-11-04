using System.Collections.Generic;
using Entitas;
using Lockstep.Core.State.Game;

public sealed class PositionEventSystem : ReactiveSystem<GameEntity>
{
    private readonly List<IPositionListener> _listenerBuffer;

    public PositionEventSystem(Contexts contexts)
        : base((IContext<GameEntity>)(object)contexts.game)
    {
        _listenerBuffer = new List<IPositionListener>();
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        //IL_000f: Unknown result type (might be due to invalid IL or missing references)
        //IL_0014: Unknown result type (might be due to invalid IL or missing references)
        return CollectorContextExtension.CreateCollector<GameEntity>(context, new TriggerOnEvent<GameEntity>[1] { TriggerOnEventMatcherExtension.Added<GameEntity>(GameMatcher.Position) });
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition && entity.hasPositionListener;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity entity in entities)
        {
            PositionComponent position = entity.position;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(entity.positionListener.value);
            foreach (IPositionListener item in _listenerBuffer)
            {
                item.OnPosition(entity, position.value, position.rotate);
            }
        }
    }
}
