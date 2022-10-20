using Entitas;
using Lockstep.Core.State.Actor;

public sealed class ActorEntity : Entity
{
	public BackupComponent backup => (BackupComponent)(object)((Entity)this).GetComponent(0);

	public bool hasBackup => ((Entity)this).HasComponent(0);

	public EntityCountComponent entityCount => (EntityCountComponent)(object)((Entity)this).GetComponent(1);

	public bool hasEntityCount => ((Entity)this).HasComponent(1);

	public IdComponent id => (IdComponent)(object)((Entity)this).GetComponent(2);

	public bool hasId => ((Entity)this).HasComponent(2);

	public void AddBackup(byte newActorId, uint newTick)
	{
		int num = 0;
		BackupComponent backupComponent = (BackupComponent)(object)((Entity)this).CreateComponent(num, typeof(BackupComponent));
		backupComponent.actorId = newActorId;
		backupComponent.tick = newTick;
		((Entity)this).AddComponent(num, (IComponent)(object)backupComponent);
	}

	public void ReplaceBackup(byte newActorId, uint newTick)
	{
		int num = 0;
		BackupComponent backupComponent = (BackupComponent)(object)((Entity)this).CreateComponent(num, typeof(BackupComponent));
		backupComponent.actorId = newActorId;
		backupComponent.tick = newTick;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)backupComponent);
	}

	public void RemoveBackup()
	{
		((Entity)this).RemoveComponent(0);
	}

	public void AddEntityCount(uint newValue)
	{
		int num = 1;
		EntityCountComponent entityCountComponent = (EntityCountComponent)(object)((Entity)this).CreateComponent(num, typeof(EntityCountComponent));
		entityCountComponent.value = newValue;
		((Entity)this).AddComponent(num, (IComponent)(object)entityCountComponent);
	}

	public void ReplaceEntityCount(uint newValue)
	{
		int num = 1;
		EntityCountComponent entityCountComponent = (EntityCountComponent)(object)((Entity)this).CreateComponent(num, typeof(EntityCountComponent));
		entityCountComponent.value = newValue;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)entityCountComponent);
	}

	public void RemoveEntityCount()
	{
		((Entity)this).RemoveComponent(1);
	}

	public void AddId(byte newValue)
	{
		int num = 2;
		IdComponent idComponent = (IdComponent)(object)((Entity)this).CreateComponent(num, typeof(IdComponent));
		idComponent.value = newValue;
		((Entity)this).AddComponent(num, (IComponent)(object)idComponent);
	}

	public void ReplaceId(byte newValue)
	{
		int num = 2;
		IdComponent idComponent = (IdComponent)(object)((Entity)this).CreateComponent(num, typeof(IdComponent));
		idComponent.value = newValue;
		((Entity)this).ReplaceComponent(num, (IComponent)(object)idComponent);
	}

	public void RemoveId()
	{
		((Entity)this).RemoveComponent(2);
	}
}
