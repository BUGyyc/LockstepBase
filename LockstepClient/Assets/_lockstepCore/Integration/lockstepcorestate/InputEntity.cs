using BEPUutilities;
using Entitas;
using Lockstep.Core.State.Input;

public sealed class InputEntity : Entity
{
	public ActorIdComponent actorId => (ActorIdComponent)(object)((Entity)this).GetComponent(0);

	public bool hasActorId => ((Entity)this).HasComponent(0);

	public CoordinateComponent coordinate => (CoordinateComponent)(object)((Entity)this).GetComponent(1);

	public bool hasCoordinate => ((Entity)this).HasComponent(1);

	public EntityConfigIdComponent entityConfigId => (EntityConfigIdComponent)(object)((Entity)this).GetComponent(2);

	public bool hasEntityConfigId => ((Entity)this).HasComponent(2);

	public SelectionComponent selection => (SelectionComponent)(object)((Entity)this).GetComponent(3);

	public bool hasSelection => ((Entity)this).HasComponent(3);

	public TargetActorIdComponent targetActorId => (TargetActorIdComponent)(object)((Entity)this).GetComponent(4);

	public bool hasTargetActorId => ((Entity)this).HasComponent(4);

	public TickComponent tick => (TickComponent)(object)((Entity)this).GetComponent(5);

	public bool hasTick => ((Entity)this).HasComponent(5);

	public void AddActorId(byte newValue)
	{
		int num = 0;
		ActorIdComponent actorIdComponent = (ActorIdComponent)(object)((Entity)this).CreateComponent(num, typeof(ActorIdComponent));
		actorIdComponent.value = newValue;
		((Entity)this).AddComponent(num, (IComponent)(object)actorIdComponent);
	}

	public void ReplaceActorId(byte newValue)
	{
		int num = 0;
		ActorIdComponent actorIdComponent = (ActorIdComponent)(object)((Entity)this).CreateComponent(num, typeof(ActorIdComponent));
		actorIdComponent.value = newValue;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)actorIdComponent);
	}

	public void RemoveActorId()
	{
		((Entity)this).RemoveComponent(0);
	}

	public void AddCoordinate(Vector2 newValue)
	{
		int num = 1;
		CoordinateComponent coordinateComponent = (CoordinateComponent)(object)((Entity)this).CreateComponent(num, typeof(CoordinateComponent));
		coordinateComponent.value = newValue;
		((Entity)this).AddComponent(num, (IComponent)(object)coordinateComponent);
	}

	public void ReplaceCoordinate(Vector2 newValue)
	{
		int num = 1;
		CoordinateComponent coordinateComponent = (CoordinateComponent)(object)((Entity)this).CreateComponent(num, typeof(CoordinateComponent));
		coordinateComponent.value = newValue;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)coordinateComponent);
	}

	public void RemoveCoordinate()
	{
		((Entity)this).RemoveComponent(1);
	}

	public void AddEntityConfigId(int newValue)
	{
		int num = 2;
		EntityConfigIdComponent entityConfigIdComponent = (EntityConfigIdComponent)(object)((Entity)this).CreateComponent(num, typeof(EntityConfigIdComponent));
		entityConfigIdComponent.value = newValue;
		((Entity)this).AddComponent(num, (IComponent)(object)entityConfigIdComponent);
	}

	public void ReplaceEntityConfigId(int newValue)
	{
		int num = 2;
		EntityConfigIdComponent entityConfigIdComponent = (EntityConfigIdComponent)(object)((Entity)this).CreateComponent(num, typeof(EntityConfigIdComponent));
		entityConfigIdComponent.value = newValue;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)entityConfigIdComponent);
	}

	public void RemoveEntityConfigId()
	{
		((Entity)this).RemoveComponent(2);
	}

	public void AddSelection(uint[] newEntityIds)
	{
		int num = 3;
		SelectionComponent selectionComponent = (SelectionComponent)(object)((Entity)this).CreateComponent(num, typeof(SelectionComponent));
		selectionComponent.entityIds = newEntityIds;
		((Entity)this).AddComponent(num, (IComponent)(object)selectionComponent);
	}

	public void ReplaceSelection(uint[] newEntityIds)
	{
		int num = 3;
		SelectionComponent selectionComponent = (SelectionComponent)(object)((Entity)this).CreateComponent(num, typeof(SelectionComponent));
		selectionComponent.entityIds = newEntityIds;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)selectionComponent);
	}

	public void RemoveSelection()
	{
		((Entity)this).RemoveComponent(3);
	}

	public void AddTargetActorId(byte newValue)
	{
		int num = 4;
		TargetActorIdComponent targetActorIdComponent = (TargetActorIdComponent)(object)((Entity)this).CreateComponent(num, typeof(TargetActorIdComponent));
		targetActorIdComponent.value = newValue;
		((Entity)this).AddComponent(num, (IComponent)(object)targetActorIdComponent);
	}

	public void ReplaceTargetActorId(byte newValue)
	{
		int num = 4;
		TargetActorIdComponent targetActorIdComponent = (TargetActorIdComponent)(object)((Entity)this).CreateComponent(num, typeof(TargetActorIdComponent));
		targetActorIdComponent.value = newValue;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)targetActorIdComponent);
	}

	public void RemoveTargetActorId()
	{
		((Entity)this).RemoveComponent(4);
	}

	public void AddTick(uint newValue)
	{
		int num = 5;
		TickComponent tickComponent = (TickComponent)(object)((Entity)this).CreateComponent(num, typeof(TickComponent));
		tickComponent.value = newValue;
		((Entity)this).AddComponent(num, (IComponent)(object)tickComponent);
	}

	public void ReplaceTick(uint newValue)
	{
		int num = 5;
		TickComponent tickComponent = (TickComponent)(object)((Entity)this).CreateComponent(num, typeof(TickComponent));
		tickComponent.value = newValue;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)tickComponent);
	}

	public void RemoveTick()
	{
		((Entity)this).RemoveComponent(5);
	}
}
