using Entitas;
/// <summary>
/// 特别的物理重力模拟计算,这种属于检查型的System，所以必须每帧执行
/// </summary>
public class PhysicsGravitySystem : IExecuteSystem, ISystem
{
    private readonly IGroup<GameEntity> _gravityGroup;

    public PhysicsGravitySystem(Contexts contexts)
    {
        _gravityGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.PhysicsGravity));
    }

    public void Execute()
    {
        var entities = _gravityGroup.GetEntities();
    }


}