using Entitas;
using Lockstep;

[Game]
public class CharacterAttrComponent : IComponent
{
    public LFloat hp;
    public LFloat mp;
}

public enum CharacterAttrType
{
    NULL = 0,
    HP,
    MP,
    SPEED
}
