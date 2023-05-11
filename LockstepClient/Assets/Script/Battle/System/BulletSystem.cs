using Entitas;
using Lockstep;

public class BulletSystem : IExecuteSystem, ISystem
{
    private readonly IGroup<GameEntity> _bulletGroup;

    private readonly IGroup<GameEntity> _characterGroup;

    //private readonly LFloat minDistance = new LFloat(false, 100);

    public BulletSystem(Contexts contexts)
    {
        _bulletGroup = contexts.game.GetGroup(
            GameMatcher.AllOf(GameMatcher.Bullet, GameMatcher.LocalId)
        );

        _characterGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Skill, GameMatcher.LocalId));
    }

    public void Execute()
    {
        foreach (var bullet in _bulletGroup.GetEntities())
        {
            return;

            if (Contexts.sharedInstance.gameState.tick.value - bullet.bullet.frameIndexOnStart >= 300)
            {
                bullet.Destroy();
                continue;
            }
        }

        foreach (var character in _characterGroup.GetEntities())
        {

            foreach (var bullet in _bulletGroup.GetEntities())
            {
                byte camp = bullet.actorId.value;
                if (camp == character.actorId.value)
                {
                    continue;
                }
                if (character.life.Dead)
                {
                    continue;
                }



                if (bullet == null || bullet.isDestroyed || bullet.isEnabled == false)
                {
                    continue;
                }


                var distance = LMath.Distance(bullet.position.value, character.position.value);

                //UnityEngine.Debug.Log($"扣血 5  distance {distance} ");
                if (distance < 3)
                {

                    //character.life.value = character.life.value - 5;
                    //UnityEngine.Debug.Log($"扣血 5  distance {distance} ");



                    //if (character.life.value <= 0)
                    //{
                    //    character.life.Dead = true;

                    //    LogMaster.E("GameEntity 被击杀");
                    //}

                    //bullet.InternalDestroy();

                    //continue;
                }

            }
        }
    }
}
