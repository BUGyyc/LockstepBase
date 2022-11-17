using Entitas;

public sealed class GameEventSystems : Feature
{
    public GameEventSystems(Contexts contexts)
    {
        // Add(new DestinationEventSystem(contexts));
        Add(new PositionEventSystem(contexts));
    }
}
