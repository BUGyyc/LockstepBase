using Entitas;
using Lockstep.Core.State.Snapshot;

public sealed class SnapshotEntity : Entity
{
	public TickComponent tick => (TickComponent)(object)((Entity)this).GetComponent(0);

	public bool hasTick => ((Entity)this).HasComponent(0);

	public void AddTick(uint newValue)
	{
		int num = 0;
		TickComponent tickComponent = (TickComponent)(object)((Entity)this).CreateComponent(num, typeof(TickComponent));
		tickComponent.value = newValue;
		((Entity)this).AddComponent(num, (IComponent)(object)tickComponent);
	}

	public void ReplaceTick(uint newValue)
	{
		int num = 0;
		TickComponent tickComponent = (TickComponent)(object)((Entity)this).CreateComponent(num, typeof(TickComponent));
		tickComponent.value = newValue;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)tickComponent);
	}

	public void RemoveTick()
	{
		((Entity)this).RemoveComponent(0);
	}
}
