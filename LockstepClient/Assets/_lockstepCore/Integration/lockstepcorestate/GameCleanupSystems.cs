using Entitas;

public sealed class GameCleanupSystems : Feature
{
	public GameCleanupSystems(Contexts contexts)
	{
		((Systems)this).Add((ISystem)(object)new DestroyDestroyedGameSystem(contexts));
	}
}
