using Entitas;

public static class ContextsExtensions
{
    public static ActorEntity GetEntityWithId(this ActorContext context, byte value)
    {
        return ((PrimaryEntityIndex<ActorEntity, byte>)(object)(context).GetEntityIndex("Id")).GetEntity(value);
    }

    public static GameEntity GetEntityWithLocalId(this GameContext context, uint value)
    {
        return ((PrimaryEntityIndex<GameEntity, uint>)(object)(context).GetEntityIndex("LocalId")).GetEntity(value);
    }

    public static SnapshotEntity GetEntityWithTick(this SnapshotContext context, uint value)
    {
        return ((PrimaryEntityIndex<SnapshotEntity, uint>)(object)(context).GetEntityIndex("Tick")).GetEntity(value);
    }
}
