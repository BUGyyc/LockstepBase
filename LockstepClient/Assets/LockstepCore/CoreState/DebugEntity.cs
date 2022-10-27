using Entitas;
using Lockstep.Core.State.Debug;

/// <summary>
/// 测试相关的Entity
/// </summary>
public sealed class DebugEntity : Entity
{
    public HashCodeComponent hashCode => (HashCodeComponent)GetComponent(0);

    public bool hasHashCode => HasComponent(0);

    public TickComponent tick => (TickComponent)GetComponent(1);

    public bool hasTick => HasComponent(1);

    public void AddHashCode(long newValue)
    {
        int num = 0;
        HashCodeComponent hashCodeComponent = (HashCodeComponent)CreateComponent(num, typeof(HashCodeComponent));
        hashCodeComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)hashCodeComponent);
    }

    public void ReplaceHashCode(long newValue)
    {
        int num = 0;
        HashCodeComponent hashCodeComponent = (HashCodeComponent)CreateComponent(num, typeof(HashCodeComponent));
        hashCodeComponent.value = newValue;
        ReplaceComponent(num, (IComponent)(object)hashCodeComponent);
    }

    public void RemoveHashCode()
    {
        RemoveComponent(0);
    }

    public void AddTick(uint newValue)
    {
        int num = 1;
        TickComponent tickComponent = (TickComponent)CreateComponent(num, typeof(TickComponent));
        tickComponent.value = newValue;
        AddComponent(num, tickComponent);
    }

    public void ReplaceTick(uint newValue)
    {
        int num = 1;
        TickComponent tickComponent = (TickComponent)CreateComponent(num, typeof(TickComponent));
        tickComponent.value = newValue;
        ReplaceComponent(num, tickComponent);
    }

    public void RemoveTick()
    {
        RemoveComponent(1);
    }
}
