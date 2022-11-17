using Entitas;
using Lockstep;
[Game]
public class MoveComponent : IComponent
{
    public LFloat speed;
    public MoveState moveState;
}


public enum MoveState
{
    Idle = 0,
    Walk,
    Run
}