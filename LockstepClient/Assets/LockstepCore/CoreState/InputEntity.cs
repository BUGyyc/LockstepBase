//using BEPUutilities;
using Entitas;
using Lockstep.Core.State.Input;
using Lockstep;

/// <summary>
/// 包裹输入指令的Entity，输入数据的载体
/// </summary>
public sealed class InputEntity : Entity
{
    public ActorIdComponent actorId => (ActorIdComponent)GetComponent(0);

    public bool hasActorId => HasComponent(0);

    public CoordinateComponent coordinate => (CoordinateComponent)GetComponent(1);

    public bool hasCoordinate => HasComponent(1);

    public EntityConfigIdComponent entityConfigId => (EntityConfigIdComponent)GetComponent(2);

    public bool hasEntityConfigId => HasComponent(2);

    public SelectionComponent selection => (SelectionComponent)GetComponent(3);

    public bool hasSelection => HasComponent(3);

    public TargetActorIdComponent targetActorId => (TargetActorIdComponent)GetComponent(4);

    public bool hasTargetActorId => HasComponent(4);

    public TickComponent tick => (TickComponent)GetComponent(5);

    public bool hasTick => HasComponent(5);

    public void AddActorId(byte newValue)
    {
        int num = 0;
        ActorIdComponent actorIdComponent = (ActorIdComponent)CreateComponent(num, typeof(ActorIdComponent));
        actorIdComponent.value = newValue;
        AddComponent(num, actorIdComponent);
    }

    public void ReplaceActorId(byte newValue)
    {
        int num = 0;
        ActorIdComponent actorIdComponent = (ActorIdComponent)CreateComponent(num, typeof(ActorIdComponent));
        actorIdComponent.value = newValue;
        ReplaceComponent(num, (IComponent)actorIdComponent);
    }

    public void RemoveActorId()
    {
        RemoveComponent(0);
    }

    public void AddCoordinate(LVector3 newValue)
    {
        int num = 1;
        CoordinateComponent coordinateComponent = (CoordinateComponent)CreateComponent(num, typeof(CoordinateComponent));
        coordinateComponent.value = newValue;
        AddComponent(num, coordinateComponent);
    }

    public void ReplaceCoordinate(LVector3 newValue)
    {
        int num = 1;
        CoordinateComponent coordinateComponent = (CoordinateComponent)CreateComponent(num, typeof(CoordinateComponent));
        coordinateComponent.value = newValue;
        ReplaceComponent(num, (IComponent)coordinateComponent);
    }

    public void RemoveCoordinate()
    {
        RemoveComponent(1);
    }

    public void AddEntityConfigId(int newValue)
    {
        int num = 2;
        EntityConfigIdComponent entityConfigIdComponent = (EntityConfigIdComponent)CreateComponent(num, typeof(EntityConfigIdComponent));
        entityConfigIdComponent.value = newValue;
        AddComponent(num, (IComponent)entityConfigIdComponent);
    }

    public void ReplaceEntityConfigId(int newValue)
    {
        int num = 2;
        EntityConfigIdComponent entityConfigIdComponent = (EntityConfigIdComponent)CreateComponent(num, typeof(EntityConfigIdComponent));
        entityConfigIdComponent.value = newValue;
        ReplaceComponent(num, (IComponent)entityConfigIdComponent);
    }

    public void RemoveEntityConfigId()
    {
        RemoveComponent(2);
    }

    public void AddSelection(uint[] newEntityIds)
    {
        int num = 3;
        SelectionComponent selectionComponent = (SelectionComponent)CreateComponent(num, typeof(SelectionComponent));
        selectionComponent.entityIds = newEntityIds;
        AddComponent(num, (IComponent)selectionComponent);
    }

    public void ReplaceSelection(uint[] newEntityIds)
    {
        int num = 3;
        SelectionComponent selectionComponent = (SelectionComponent)CreateComponent(num, typeof(SelectionComponent));
        selectionComponent.entityIds = newEntityIds;
        ReplaceComponent(num, (IComponent)selectionComponent);
    }

    public void RemoveSelection()
    {
        RemoveComponent(3);
    }

    public void AddTargetActorId(byte newValue)
    {
        int num = 4;
        TargetActorIdComponent targetActorIdComponent = (TargetActorIdComponent)CreateComponent(num, typeof(TargetActorIdComponent));
        targetActorIdComponent.value = newValue;
        AddComponent(num, (IComponent)targetActorIdComponent);
    }

    public void ReplaceTargetActorId(byte newValue)
    {
        int num = 4;
        TargetActorIdComponent targetActorIdComponent = (TargetActorIdComponent)CreateComponent(num, typeof(TargetActorIdComponent));
        targetActorIdComponent.value = newValue;
        ReplaceComponent(num, (IComponent)targetActorIdComponent);
    }

    public void RemoveTargetActorId()
    {
        RemoveComponent(4);
    }

    public void AddTick(uint newValue)
    {
        int num = 5;
        TickComponent tickComponent = (TickComponent)CreateComponent(num, typeof(TickComponent));
        tickComponent.value = newValue;
        AddComponent(num, (IComponent)tickComponent);
    }

    public void ReplaceTick(uint newValue)
    {
        int num = 5;
        TickComponent tickComponent = (TickComponent)CreateComponent(num, typeof(TickComponent));
        tickComponent.value = newValue;
        ReplaceComponent(num, (IComponent)tickComponent);
    }

    public void RemoveTick()
    {
        RemoveComponent(5);
    }
}
