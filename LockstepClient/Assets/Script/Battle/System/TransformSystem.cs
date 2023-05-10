using Entitas;


public class TransformSystem : IExecuteSystem, ISystem
{
    private readonly IGroup<GameEntity> _gameEntities;
    public TransformSystem(Contexts contexts)
    {

        _gameEntities = contexts.game.GetGroup((IMatcher<GameEntity>)(object)GameMatcher.AllOf(GameMatcher.PositionListener, GameMatcher.Position, GameMatcher.LocalId));

        //_gameEntities = ((Context<GameEntity>)contexts.game).GetGroup((IMatcher<GameEntity>)(object)GameMatcher.AllOf(GameMatcher.Position, GameMatcher.PositionListener));
    }


    public void Execute()
    {
        foreach (GameEntity entity in _gameEntities.GetEntities())
        {
            var position = entity.position;

            //if (entity.hasPositionListener == false) continue;

            var positionListener = entity.positionListener;

            foreach (var listener in positionListener.value)
            {
                listener.OnPosition(entity, position.value, position.rotate);
            }
        }
    }
}

