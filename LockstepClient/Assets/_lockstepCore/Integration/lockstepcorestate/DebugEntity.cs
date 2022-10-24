using Entitas;
using Lockstep.Core.State.Debug;

public sealed class DebugEntity : Entity
{
	public HashCodeComponent hashCode => (HashCodeComponent)(object)((Entity)this).GetComponent(0);

	public bool hasHashCode => ((Entity)this).HasComponent(0);

	public TickComponent tick => (TickComponent)(object)((Entity)this).GetComponent(1);

	public bool hasTick => ((Entity)this).HasComponent(1);

	public void AddHashCode(long newValue)
	{
		int num = 0;
		HashCodeComponent hashCodeComponent = (HashCodeComponent)(object)((Entity)this).CreateComponent(num, typeof(HashCodeComponent));
		hashCodeComponent.value = newValue;
		((Entity)this).AddComponent(num, (IComponent)(object)hashCodeComponent);
	}

	public void ReplaceHashCode(long newValue)
	{
		int num = 0;
		HashCodeComponent hashCodeComponent = (HashCodeComponent)(object)((Entity)this).CreateComponent(num, typeof(HashCodeComponent));
		hashCodeComponent.value = newValue;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)hashCodeComponent);
	}

	public void RemoveHashCode()
	{
		((Entity)this).RemoveComponent(0);
	}

	public void AddTick(uint newValue)
	{
		int num = 1;
		TickComponent tickComponent = (TickComponent)(object)((Entity)this).CreateComponent(num, typeof(TickComponent));
		tickComponent.value = newValue;
		((Entity)this).AddComponent(num, (IComponent)(object)tickComponent);
	}

	public void ReplaceTick(uint newValue)
	{
		int num = 1;
		TickComponent tickComponent = (TickComponent)(object)((Entity)this).CreateComponent(num, typeof(TickComponent));
		tickComponent.value = newValue;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)tickComponent);
	}

	public void RemoveTick()
	{
		((Entity)this).RemoveComponent(1);
	}
}
