using Entitas;
using Lockstep;

[Game]
public class LifeComponent : IComponent
{
    public LFloat value;


    public bool Dead;
}