using Entitas;
// using Lockstep.Core.State.Actor;

/// <summary>
/// 相当于玩家对象的Entity
/// </summary>
public sealed partial class ActorEntity : Entity
{
    // public BackupComponent backup => (BackupComponent)GetComponent(0);

    // public bool hasBackup => HasComponent(0);

    // public EntityCountComponent entityCount => (EntityCountComponent)GetComponent(1);

    // public bool hasEntityCount => HasComponent(1);

    // public IdComponent id => (IdComponent)GetComponent(2);

    // public bool hasId => HasComponent(2);

    // public void AddBackup(byte newActorId, uint newTick)
    // {
    //     int num = 0;
    //     BackupComponent backupComponent = (BackupComponent)CreateComponent(num, typeof(BackupComponent));
    //     backupComponent.actorId = newActorId;
    //     backupComponent.tick = newTick;
    //     AddComponent(num, backupComponent);
    // }

    // public void ReplaceBackup(byte newActorId, uint newTick)
    // {
    //     int num = 0;
    //     BackupComponent backupComponent = (BackupComponent)CreateComponent(num, typeof(BackupComponent));
    //     backupComponent.actorId = newActorId;
    //     backupComponent.tick = newTick;
    //     ReplaceComponent(num, backupComponent);
    // }

    // public void RemoveBackup()
    // {
    //     RemoveComponent(0);
    // }

    // public void AddEntityCount(uint newValue)
    // {
    //     int num = 1;
    //     EntityCountComponent entityCountComponent = (EntityCountComponent)CreateComponent(num, typeof(EntityCountComponent));
    //     entityCountComponent.value = newValue;
    //     AddComponent(num, entityCountComponent);
    // }

    // public void ReplaceEntityCount(uint newValue)
    // {
    //     int num = 1;
    //     EntityCountComponent entityCountComponent = (EntityCountComponent)CreateComponent(num, typeof(EntityCountComponent));
    //     entityCountComponent.value = newValue;
    //     ReplaceComponent(num, entityCountComponent);
    // }

    // public void RemoveEntityCount()
    // {
    //     RemoveComponent(1);
    // }

    // public void AddId(byte newValue)
    // {
    //     int num = 2;
    //     IdComponent idComponent = (IdComponent)CreateComponent(num, typeof(IdComponent));
    //     idComponent.value = newValue;
    //     AddComponent(num, idComponent);
    // }

    // public void ReplaceId(byte newValue)
    // {
    //     int num = 2;
    //     IdComponent idComponent = (IdComponent)CreateComponent(num, typeof(IdComponent));
    //     idComponent.value = newValue;
    //     ReplaceComponent(num, idComponent);
    // }

    // public void RemoveId()
    // {
    //     RemoveComponent(2);
    // }
}
