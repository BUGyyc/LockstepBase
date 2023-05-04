using Entitas;

public class BulletSystem : IExecuteSystem, ISystem
{
    private readonly IGroup<GameEntity> _gravityGroup;

    public BulletSystem(Contexts contexts)
    {
        _gravityGroup = contexts.game.GetGroup(
            GameMatcher.AllOf(GameMatcher.Bullet, GameMatcher.LocalId)
        );
    }

    public void Execute()
    {
        var entities = _gravityGroup.GetEntities();
        foreach (var entity in entities) {

            var pos = entity.position.value;

        }
    }
}
