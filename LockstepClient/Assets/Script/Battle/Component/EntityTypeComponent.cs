using Entitas;

[Game]
public class EntityTypeComponent : IComponent
{
    public EntityType type;
}


public enum EntityType
{
    Empty = 0,

    Hero = 1,
    Bullet = 2
}