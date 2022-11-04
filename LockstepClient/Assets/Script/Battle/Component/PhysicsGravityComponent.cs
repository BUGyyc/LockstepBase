using Entitas;
using Lockstep;
public class PhysicsGravityComponent : IComponent
{
    public LVector3 gravity = GameLogicSetting.GravityLV3;

    public bool isGround;
}

public sealed partial class GameEntity : Entity
{
    public PhysicsGravityComponent PhysicsGravity => (PhysicsGravityComponent)GetComponent(GameComponentsLookup.PhysicsGravity);
    public bool HasPhysicsGravity => HasComponent(GameComponentsLookup.PhysicsGravity);
}

public sealed partial class GameMatcher
{
    private static IMatcher<GameEntity> _physicsGravity;
    public static IMatcher<GameEntity> PhysicsGravity
    {
        get
        {
            if (_physicsGravity == null)
            {
                Matcher<GameEntity> val = Matcher<GameEntity>.AllOf(new int[1] { GameComponentsLookup.PhysicsGravity }) as Matcher<GameEntity>;
                val.componentNames = GameComponentsLookup.componentNames;
                _physicsGravity = val;
            }
            return _physicsGravity;
        }
    }
}