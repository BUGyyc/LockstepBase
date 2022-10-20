using Entitas;

public sealed class GameEventSystems : Feature
{
	public GameEventSystems(Contexts contexts)
	{
		((Systems)this).Add((ISystem)(object)new DestinationEventSystem(contexts));
		((Systems)this).Add((ISystem)(object)new PositionEventSystem(contexts));
	}
}
