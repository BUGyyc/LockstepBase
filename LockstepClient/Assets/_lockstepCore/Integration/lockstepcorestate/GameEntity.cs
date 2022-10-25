using System.Collections.Generic;
using BEPUutilities;
using Entitas;
using FixMath.NET;
using Lockstep.Core.State.Game;

public sealed class GameEntity : Entity
{
    private static readonly DestroyedComponent destroyedComponent = new DestroyedComponent();

    private static readonly ControllableComponent controllableComponent = new ControllableComponent();

    private static readonly HashableComponent hashableComponent = new HashableComponent();

    private static readonly NavigableComponent navigableComponent = new NavigableComponent();

    public RadiusComponent radius => (RadiusComponent)(object)((Entity)this).GetComponent(14);

    public bool hasRadius => ((Entity)this).HasComponent(14);

    public RvoAgentSettingsComponent rvoAgentSettings => (RvoAgentSettingsComponent)(object)((Entity)this).GetComponent(15);

    public bool hasRvoAgentSettings => ((Entity)this).HasComponent(15);

    public bool isDestroyed
    {
        get
        {
            return ((Entity)this).HasComponent(6);
        }
        set
        {
            if (value == isDestroyed)
            {
                return;
            }
            int num = 6;
            if (value)
            {
                Stack<IComponent> componentPool = ((Entity)this).GetComponentPool(num);
                IComponent obj;
                if (componentPool.Count <= 0)
                {
                    IComponent val = (IComponent)(object)destroyedComponent;
                    obj = val;
                }
                else
                {
                    obj = componentPool.Pop();
                }
                IComponent val2 = obj;
                ((Entity)this).AddComponent(num, val2);
            }
            else
            {
                ((Entity)this).RemoveComponent(num);
            }
        }
    }

    public ActorIdComponent actorId => (ActorIdComponent)(object)((Entity)this).GetComponent(1);

    public bool hasActorId => ((Entity)this).HasComponent(1);

    public AssetComponent asset => (AssetComponent)(object)((Entity)this).GetComponent(2);

    public bool hasAsset => ((Entity)this).HasComponent(2);

    public BackupComponent backup => (BackupComponent)(object)((Entity)this).GetComponent(3);

    public bool hasBackup => ((Entity)this).HasComponent(3);

    public bool isControllable
    {
        get
        {
            return ((Entity)this).HasComponent(4);
        }
        set
        {
            if (value == isControllable)
            {
                return;
            }
            int num = 4;
            if (value)
            {
                Stack<IComponent> componentPool = ((Entity)this).GetComponentPool(num);
                IComponent obj;
                if (componentPool.Count <= 0)
                {
                    IComponent val = (IComponent)(object)controllableComponent;
                    obj = val;
                }
                else
                {
                    obj = componentPool.Pop();
                }
                IComponent val2 = obj;
                ((Entity)this).AddComponent(num, val2);
            }
            else
            {
                ((Entity)this).RemoveComponent(num);
            }
        }
    }

    public DestinationComponent destination => (DestinationComponent)(object)((Entity)this).GetComponent(5);

    public bool hasDestination => ((Entity)this).HasComponent(5);

    public bool isHashable
    {
        get
        {
            return ((Entity)this).HasComponent(7);
        }
        set
        {
            if (value == isHashable)
            {
                return;
            }
            int num = 7;
            if (value)
            {
                Stack<IComponent> componentPool = ((Entity)this).GetComponentPool(num);
                IComponent obj;
                if (componentPool.Count <= 0)
                {
                    IComponent val = (IComponent)(object)hashableComponent;
                    obj = val;
                }
                else
                {
                    obj = componentPool.Pop();
                }
                IComponent val2 = obj;
                ((Entity)this).AddComponent(num, val2);
            }
            else
            {
                ((Entity)this).RemoveComponent(num);
            }
        }
    }

    public HealthComponent health => (HealthComponent)(object)((Entity)this).GetComponent(8);

    public bool hasHealth => ((Entity)this).HasComponent(8);

    public IdComponent id => (IdComponent)(object)((Entity)this).GetComponent(9);

    public bool hasId => ((Entity)this).HasComponent(9);

    public LocalIdComponent localId => (LocalIdComponent)(object)((Entity)this).GetComponent(10);

    public bool hasLocalId => ((Entity)this).HasComponent(10);

    public MaxSpeedComponent maxSpeed => (MaxSpeedComponent)(object)((Entity)this).GetComponent(11);

    public bool hasMaxSpeed => ((Entity)this).HasComponent(11);

    public bool isNavigable
    {
        get
        {
            return ((Entity)this).HasComponent(12);
        }
        set
        {
            if (value == isNavigable)
            {
                return;
            }
            int num = 12;
            if (value)
            {
                Stack<IComponent> componentPool = ((Entity)this).GetComponentPool(num);
                IComponent obj;
                if (componentPool.Count <= 0)
                {
                    IComponent val = (IComponent)(object)navigableComponent;
                    obj = val;
                }
                else
                {
                    obj = componentPool.Pop();
                }
                IComponent val2 = obj;
                ((Entity)this).AddComponent(num, val2);
            }
            else
            {
                ((Entity)this).RemoveComponent(num);
            }
        }
    }

    public PositionComponent position => (PositionComponent)(object)((Entity)this).GetComponent(13);

    public bool hasPosition => ((Entity)this).HasComponent(13);

    public TeamComponent team => (TeamComponent)(object)((Entity)this).GetComponent(16);

    public bool hasTeam => ((Entity)this).HasComponent(16);

    public VelocityComponent velocity => (VelocityComponent)(object)((Entity)this).GetComponent(17);

    public bool hasVelocity => ((Entity)this).HasComponent(17);

    public DestinationListenerComponent destinationListener => (DestinationListenerComponent)(object)((Entity)this).GetComponent(0);

    public bool hasDestinationListener => ((Entity)this).HasComponent(0);

    public PositionListenerComponent positionListener => (PositionListenerComponent)(object)((Entity)this).GetComponent(18);

    public bool hasPositionListener => ((Entity)this).HasComponent(18);

    public void AddRadius(Fix64 newValue)
    {
        int num = 14;
        RadiusComponent radiusComponent = (RadiusComponent)(object)((Entity)this).CreateComponent(num, typeof(RadiusComponent));
        radiusComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)radiusComponent);
    }

    public void ReplaceRadius(Fix64 newValue)
    {
        int num = 14;
        RadiusComponent radiusComponent = (RadiusComponent)(object)((Entity)this).CreateComponent(num, typeof(RadiusComponent));
        radiusComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)radiusComponent);
    }

    public void RemoveRadius()
    {
        ((Entity)this).RemoveComponent(14);
    }

    public void AddRvoAgentSettings(Vector2 newPreferredVelocity, Fix64 newTimeHorizonObst, IList<KeyValuePair<Fix64, uint>> newAgentNeighbors)
    {
        int num = 15;
        RvoAgentSettingsComponent rvoAgentSettingsComponent = (RvoAgentSettingsComponent)(object)((Entity)this).CreateComponent(num, typeof(RvoAgentSettingsComponent));
        rvoAgentSettingsComponent.preferredVelocity = newPreferredVelocity;
        rvoAgentSettingsComponent.timeHorizonObst = newTimeHorizonObst;
        rvoAgentSettingsComponent.agentNeighbors = newAgentNeighbors;
        ((Entity)this).AddComponent(num, (IComponent)(object)rvoAgentSettingsComponent);
    }

    public void ReplaceRvoAgentSettings(Vector2 newPreferredVelocity, Fix64 newTimeHorizonObst, IList<KeyValuePair<Fix64, uint>> newAgentNeighbors)
    {
        int num = 15;
        RvoAgentSettingsComponent rvoAgentSettingsComponent = (RvoAgentSettingsComponent)(object)((Entity)this).CreateComponent(num, typeof(RvoAgentSettingsComponent));
        rvoAgentSettingsComponent.preferredVelocity = newPreferredVelocity;
        rvoAgentSettingsComponent.timeHorizonObst = newTimeHorizonObst;
        rvoAgentSettingsComponent.agentNeighbors = newAgentNeighbors;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)rvoAgentSettingsComponent);
    }

    public void RemoveRvoAgentSettings()
    {
        ((Entity)this).RemoveComponent(15);
    }

    public void AddActorId(byte newValue)
    {
        int num = 1;
        ActorIdComponent actorIdComponent = (ActorIdComponent)(object)((Entity)this).CreateComponent(num, typeof(ActorIdComponent));
        actorIdComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)actorIdComponent);
    }

    public void ReplaceActorId(byte newValue)
    {
        int num = 1;
        ActorIdComponent actorIdComponent = (ActorIdComponent)(object)((Entity)this).CreateComponent(num, typeof(ActorIdComponent));
        actorIdComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)actorIdComponent);
    }

    public void RemoveActorId()
    {
        ((Entity)this).RemoveComponent(1);
    }

    public void AddAsset(string newName)
    {
        int num = 2;
        AssetComponent assetComponent = (AssetComponent)(object)((Entity)this).CreateComponent(num, typeof(AssetComponent));
        assetComponent.name = newName;
        ((Entity)this).AddComponent(num, (IComponent)(object)assetComponent);
    }

    public void ReplaceAsset(string newName)
    {
        int num = 2;
        AssetComponent assetComponent = (AssetComponent)(object)((Entity)this).CreateComponent(num, typeof(AssetComponent));
        assetComponent.name = newName;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)assetComponent);
    }

    public void RemoveAsset()
    {
        ((Entity)this).RemoveComponent(2);
    }

    public void AddBackup(uint newLocalEntityId, uint newTick)
    {
        int num = 3;
        BackupComponent backupComponent = (BackupComponent)(object)((Entity)this).CreateComponent(num, typeof(BackupComponent));
        backupComponent.localEntityId = newLocalEntityId;
        backupComponent.tick = newTick;
        ((Entity)this).AddComponent(num, (IComponent)(object)backupComponent);
    }

    public void ReplaceBackup(uint newLocalEntityId, uint newTick)
    {
        int num = 3;
        BackupComponent backupComponent = (BackupComponent)(object)((Entity)this).CreateComponent(num, typeof(BackupComponent));
        backupComponent.localEntityId = newLocalEntityId;
        backupComponent.tick = newTick;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)backupComponent);
    }

    public void RemoveBackup()
    {
        ((Entity)this).RemoveComponent(3);
    }

    public void AddDestination(Vector2 newValue)
    {
        int num = 5;
        DestinationComponent destinationComponent = (DestinationComponent)(object)((Entity)this).CreateComponent(num, typeof(DestinationComponent));
        destinationComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)destinationComponent);
    }

    public void ReplaceDestination(Vector2 newValue)
    {
        int num = 5;
        DestinationComponent destinationComponent = (DestinationComponent)(object)((Entity)this).CreateComponent(num, typeof(DestinationComponent));
        destinationComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)destinationComponent);
    }

    public void RemoveDestination()
    {
        ((Entity)this).RemoveComponent(5);
    }

    public void AddHealth(int newValue)
    {
        int num = 8;
        HealthComponent healthComponent = (HealthComponent)(object)((Entity)this).CreateComponent(num, typeof(HealthComponent));
        healthComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)healthComponent);
    }

    public void ReplaceHealth(int newValue)
    {
        int num = 8;
        HealthComponent healthComponent = (HealthComponent)(object)((Entity)this).CreateComponent(num, typeof(HealthComponent));
        healthComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)healthComponent);
    }

    public void RemoveHealth()
    {
        ((Entity)this).RemoveComponent(8);
    }

    public void AddId(uint newValue)
    {
        int num = 9;
        IdComponent idComponent = (IdComponent)(object)((Entity)this).CreateComponent(num, typeof(IdComponent));
        idComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)idComponent);
    }

    public void ReplaceId(uint newValue)
    {
        int num = 9;
        IdComponent idComponent = (IdComponent)(object)((Entity)this).CreateComponent(num, typeof(IdComponent));
        idComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)idComponent);
    }

    public void RemoveId()
    {
        ((Entity)this).RemoveComponent(9);
    }

    public void AddLocalId(uint newValue)
    {
        int num = 10;
        LocalIdComponent localIdComponent = (LocalIdComponent)(object)((Entity)this).CreateComponent(num, typeof(LocalIdComponent));
        localIdComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)localIdComponent);
    }

    public void ReplaceLocalId(uint newValue)
    {
        int num = 10;
        LocalIdComponent localIdComponent = (LocalIdComponent)(object)((Entity)this).CreateComponent(num, typeof(LocalIdComponent));
        localIdComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)localIdComponent);
    }

    public void RemoveLocalId()
    {
        ((Entity)this).RemoveComponent(10);
    }

    public void AddMaxSpeed(Fix64 newValue)
    {
        int num = 11;
        MaxSpeedComponent maxSpeedComponent = (MaxSpeedComponent)(object)((Entity)this).CreateComponent(num, typeof(MaxSpeedComponent));
        maxSpeedComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)maxSpeedComponent);
    }

    public void ReplaceMaxSpeed(Fix64 newValue)
    {
        int num = 11;
        MaxSpeedComponent maxSpeedComponent = (MaxSpeedComponent)(object)((Entity)this).CreateComponent(num, typeof(MaxSpeedComponent));
        maxSpeedComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)maxSpeedComponent);
    }

    public void RemoveMaxSpeed()
    {
        ((Entity)this).RemoveComponent(11);
    }

    public void AddPosition(Vector2 newValue)
    {
        int num = 13;
        PositionComponent positionComponent = (PositionComponent)(object)((Entity)this).CreateComponent(num, typeof(PositionComponent));
        positionComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)positionComponent);
    }

    public void ReplacePosition(Vector2 newValue)
    {
        int num = 13;
        PositionComponent positionComponent = (PositionComponent)(object)((Entity)this).CreateComponent(num, typeof(PositionComponent));
        positionComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)positionComponent);
    }

    public void RemovePosition()
    {
        ((Entity)this).RemoveComponent(13);
    }

    public void AddTeam(byte newValue)
    {
        int num = 16;
        TeamComponent teamComponent = (TeamComponent)(object)((Entity)this).CreateComponent(num, typeof(TeamComponent));
        teamComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)teamComponent);
    }

    public void ReplaceTeam(byte newValue)
    {
        int num = 16;
        TeamComponent teamComponent = (TeamComponent)(object)((Entity)this).CreateComponent(num, typeof(TeamComponent));
        teamComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)teamComponent);
    }

    public void RemoveTeam()
    {
        ((Entity)this).RemoveComponent(16);
    }

    public void AddVelocity(Vector2 newValue)
    {
        int num = 17;
        VelocityComponent velocityComponent = (VelocityComponent)(object)((Entity)this).CreateComponent(num, typeof(VelocityComponent));
        velocityComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)velocityComponent);
    }

    public void ReplaceVelocity(Vector2 newValue)
    {
        int num = 17;
        VelocityComponent velocityComponent = (VelocityComponent)(object)((Entity)this).CreateComponent(num, typeof(VelocityComponent));
        velocityComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)velocityComponent);
    }

    public void RemoveVelocity()
    {
        ((Entity)this).RemoveComponent(17);
    }

    public void AddDestinationListener(List<IDestinationListener> newValue)
    {
        int num = 0;
        DestinationListenerComponent destinationListenerComponent = (DestinationListenerComponent)(object)((Entity)this).CreateComponent(num, typeof(DestinationListenerComponent));
        destinationListenerComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)destinationListenerComponent);
    }

    public void ReplaceDestinationListener(List<IDestinationListener> newValue)
    {
        int num = 0;
        DestinationListenerComponent destinationListenerComponent = (DestinationListenerComponent)(object)((Entity)this).CreateComponent(num, typeof(DestinationListenerComponent));
        destinationListenerComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)destinationListenerComponent);
    }

    public void RemoveDestinationListener()
    {
        ((Entity)this).RemoveComponent(0);
    }

    public void AddDestinationListener(IDestinationListener value)
    {
        List<IDestinationListener> list = (hasDestinationListener ? destinationListener.value : new List<IDestinationListener>());
        list.Add(value);
        ReplaceDestinationListener(list);
    }

    public void RemoveDestinationListener(IDestinationListener value, bool removeComponentWhenEmpty = true)
    {
        List<IDestinationListener> value2 = destinationListener.value;
        value2.Remove(value);
        if (removeComponentWhenEmpty && value2.Count == 0)
        {
            RemoveDestinationListener();
        }
        else
        {
            ReplaceDestinationListener(value2);
        }
    }

    public void AddPositionListener(List<IPositionListener> newValue)
    {
        int num = 18;
        PositionListenerComponent positionListenerComponent = (PositionListenerComponent)(object)((Entity)this).CreateComponent(num, typeof(PositionListenerComponent));
        positionListenerComponent.value = newValue;
        ((Entity)this).AddComponent(num, (IComponent)(object)positionListenerComponent);
    }

    public void ReplacePositionListener(List<IPositionListener> newValue)
    {
        int num = 18;
        PositionListenerComponent positionListenerComponent = (PositionListenerComponent)(object)((Entity)this).CreateComponent(num, typeof(PositionListenerComponent));
        positionListenerComponent.value = newValue;
        ((Entity)this).ReplaceComponent(num, (IComponent)(object)positionListenerComponent);
    }

    public void RemovePositionListener()
    {
        ((Entity)this).RemoveComponent(18);
    }

    public void AddPositionListener(IPositionListener value)
    {
        List<IPositionListener> list = (hasPositionListener ? positionListener.value : new List<IPositionListener>());
        list.Add(value);
        ReplacePositionListener(list);
    }

    public void RemovePositionListener(IPositionListener value, bool removeComponentWhenEmpty = true)
    {
        List<IPositionListener> value2 = positionListener.value;
        value2.Remove(value);
        if (removeComponentWhenEmpty && value2.Count == 0)
        {
            RemovePositionListener();
        }
        else
        {
            ReplacePositionListener(value2);
        }
    }
}
