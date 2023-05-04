using Entitas;
using Lockstep;

public class BulletSystem : IExecuteSystem, ISystem
{
    private readonly IGroup<GameEntity> _gravityGroup;

    private readonly IGroup<GameEntity> _characterGroup;

    private readonly LFloat minDistance = new LFloat(false, 100);

    public BulletSystem(Contexts contexts)
    {
        _gravityGroup = contexts.game.GetGroup(
            GameMatcher.AllOf(GameMatcher.Bullet, GameMatcher.LocalId)
        );

        _characterGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Skill));
    }

    public void Execute()
    {
        var entities = _gravityGroup.GetEntities();
        foreach (var entity in entities)
        {
            byte camp = entity.actorId.value;
            var pos = entity.position.value;

            foreach (var character in _characterGroup.GetEntities())
            {
                if (camp == character.actorId.value)
                {
                    continue;
                }
                var distance = LMath.Distance(entity.position.value, pos);
                if (distance < minDistance)
                {
                    character.life.value -= new LFloat(false, 5);
                    UnityEngine.Debug.Log("扣血 5");
                }
            }

        }
    }
}
