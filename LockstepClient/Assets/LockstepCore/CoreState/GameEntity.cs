using System.Collections.Generic;
using BEPUutilities;
using Entitas;
using FixMath.NET;
using Lockstep.Core.State.Game;

/// <summary>
/// 游戏对象Entity
/// </summary>
public sealed class GameEntity : Entity
{
    private static readonly DestroyedComponent destroyedComponent = new DestroyedComponent();

    private static readonly ControllableComponent controllableComponent = new ControllableComponent();

    private static readonly HashableComponent hashableComponent = new HashableComponent();

    private static readonly NavigableComponent navigableComponent = new NavigableComponent();


    public AnimationComponent animation => (AnimationComponent)GetComponent(GameComponentsLookup.Animation);
    public bool hasAnimtion => HasComponent(GameComponentsLookup.Animation);


    public ModelComponent model => (ModelComponent)GetComponent(GameComponentsLookup.Model);
    public bool hasModel => HasComponent(GameComponentsLookup.Model);

    public RadiusComponent radius => (RadiusComponent)GetComponent(14);

    public bool hasRadius => HasComponent(14);

    public RvoAgentSettingsComponent rvoAgentSettings => (RvoAgentSettingsComponent)GetComponent(15);

    public bool hasRvoAgentSettings => HasComponent(15);

    public bool isDestroyed
    {
        get
        {
            return HasComponent(6);
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
                Stack<IComponent> componentPool = GetComponentPool(num);
                IComponent obj;
                if (componentPool.Count <= 0)
                {
                    IComponent val = destroyedComponent;
                    obj = val;
                }
                else
                {
                    obj = componentPool.Pop();
                }
                IComponent val2 = obj;
                AddComponent(num, val2);
            }
            else
            {
                RemoveComponent(num);
            }
        }
    }

    public ActorIdComponent actorId => (ActorIdComponent)GetComponent(1);

    public bool hasActorId => HasComponent(1);

    public AssetComponent asset => (AssetComponent)GetComponent(2);

    public bool hasAsset => HasComponent(2);

    public BackupComponent backup => (BackupComponent)GetComponent(3);

    public bool hasBackup => HasComponent(3);

    public bool isControllable
    {
        get
        {
            return HasComponent(4);
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
                Stack<IComponent> componentPool = GetComponentPool(num);
                IComponent obj;
                if (componentPool.Count <= 0)
                {
                    IComponent val = controllableComponent;
                    obj = val;
                }
                else
                {
                    obj = componentPool.Pop();
                }
                IComponent val2 = obj;
                AddComponent(num, val2);
            }
            else
            {
                RemoveComponent(num);
            }
        }
    }

    public DestinationComponent destination => (DestinationComponent)GetComponent(5);

    public bool hasDestination => HasComponent(5);

    public bool isHashable
    {
        get
        {
            return HasComponent(7);
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
                Stack<IComponent> componentPool = GetComponentPool(num);
                IComponent obj;
                if (componentPool.Count <= 0)
                {
                    IComponent val = hashableComponent;
                    obj = val;
                }
                else
                {
                    obj = componentPool.Pop();
                }
                IComponent val2 = obj;
                AddComponent(num, val2);
            }
            else
            {
                RemoveComponent(num);
            }
        }
    }

    public HealthComponent health => (HealthComponent)GetComponent(8);

    public bool hasHealth => HasComponent(8);

    public IdComponent id => (IdComponent)GetComponent(9);

    public bool hasId => HasComponent(9);

    public LocalIdComponent localId => (LocalIdComponent)GetComponent(10);

    public bool hasLocalId => HasComponent(10);

    public MaxSpeedComponent maxSpeed => (MaxSpeedComponent)GetComponent(11);

    public bool hasMaxSpeed => HasComponent(11);

    public bool isNavigable
    {
        get
        {
            return HasComponent(12);
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
                Stack<IComponent> componentPool = GetComponentPool(num);
                IComponent obj;
                if (componentPool.Count <= 0)
                {
                    IComponent val = navigableComponent;
                    obj = val;
                }
                else
                {
                    obj = componentPool.Pop();
                }
                IComponent val2 = obj;
                AddComponent(num, val2);
            }
            else
            {
                RemoveComponent(num);
            }
        }
    }

    public PositionComponent position => (PositionComponent)GetComponent(13);

    public bool hasPosition => HasComponent(13);

    public TeamComponent team => (TeamComponent)GetComponent(16);

    public bool hasTeam => HasComponent(16);

    public VelocityComponent velocity => (VelocityComponent)GetComponent(17);

    public bool hasVelocity => HasComponent(17);

    public DestinationListenerComponent destinationListener => (DestinationListenerComponent)GetComponent(0);

    public bool hasDestinationListener => HasComponent(0);

    public PositionListenerComponent positionListener => (PositionListenerComponent)GetComponent(18);

    public bool hasPositionListener => HasComponent(18);


    public CharacterComponent character => (CharacterComponent)GetComponent(GameComponentsLookup.Character);

    public bool hasCharacter => HasComponent(GameComponentsLookup.Character);

    public void AddRadius(Fix64 newValue)
    {
        int num = 14;
        RadiusComponent radiusComponent = (RadiusComponent)CreateComponent(num, typeof(RadiusComponent));
        radiusComponent.value = newValue;
        AddComponent(num, radiusComponent);
    }

    public void ReplaceRadius(Fix64 newValue)
    {
        int num = 14;
        RadiusComponent radiusComponent = (RadiusComponent)CreateComponent(num, typeof(RadiusComponent));
        radiusComponent.value = newValue;
        ReplaceComponent(num, radiusComponent);
    }

    public void RemoveRadius()
    {
        RemoveComponent(14);
    }

    public void AddRvoAgentSettings(Vector2 newPreferredVelocity, Fix64 newTimeHorizonObst, IList<KeyValuePair<Fix64, uint>> newAgentNeighbors)
    {
        int num = 15;
        RvoAgentSettingsComponent rvoAgentSettingsComponent = (RvoAgentSettingsComponent)CreateComponent(num, typeof(RvoAgentSettingsComponent));
        rvoAgentSettingsComponent.preferredVelocity = newPreferredVelocity;
        rvoAgentSettingsComponent.timeHorizonObst = newTimeHorizonObst;
        rvoAgentSettingsComponent.agentNeighbors = newAgentNeighbors;
        AddComponent(num, rvoAgentSettingsComponent);
    }

    public void ReplaceRvoAgentSettings(Vector2 newPreferredVelocity, Fix64 newTimeHorizonObst, IList<KeyValuePair<Fix64, uint>> newAgentNeighbors)
    {
        int num = 15;
        RvoAgentSettingsComponent rvoAgentSettingsComponent = (RvoAgentSettingsComponent)CreateComponent(num, typeof(RvoAgentSettingsComponent));
        rvoAgentSettingsComponent.preferredVelocity = newPreferredVelocity;
        rvoAgentSettingsComponent.timeHorizonObst = newTimeHorizonObst;
        rvoAgentSettingsComponent.agentNeighbors = newAgentNeighbors;
        ReplaceComponent(num, rvoAgentSettingsComponent);
    }

    public void RemoveRvoAgentSettings()
    {
        RemoveComponent(15);
    }

    public void AddActorId(byte newValue)
    {
        int num = 1;
        ActorIdComponent actorIdComponent = (ActorIdComponent)CreateComponent(num, typeof(ActorIdComponent));
        actorIdComponent.value = newValue;
        AddComponent(num, actorIdComponent);
    }

    public void ReplaceActorId(byte newValue)
    {
        int num = 1;
        ActorIdComponent actorIdComponent = (ActorIdComponent)CreateComponent(num, typeof(ActorIdComponent));
        actorIdComponent.value = newValue;
        ReplaceComponent(num, actorIdComponent);
    }

    public void RemoveActorId()
    {
        RemoveComponent(1);
    }

    public void AddAsset(string newName)
    {
        int num = 2;
        AssetComponent assetComponent = (AssetComponent)CreateComponent(num, typeof(AssetComponent));
        assetComponent.name = newName;
        AddComponent(num, assetComponent);
    }

    public void ReplaceAsset(string newName)
    {
        int num = 2;
        AssetComponent assetComponent = (AssetComponent)CreateComponent(num, typeof(AssetComponent));
        assetComponent.name = newName;
        ReplaceComponent(num, assetComponent);
    }

    public void RemoveAsset()
    {
        RemoveComponent(2);
    }

    public void AddBackup(uint newLocalEntityId, uint newTick)
    {
        int num = 3;
        BackupComponent backupComponent = (BackupComponent)CreateComponent(num, typeof(BackupComponent));
        backupComponent.localEntityId = newLocalEntityId;
        backupComponent.tick = newTick;
        AddComponent(num, backupComponent);
    }

    public void ReplaceBackup(uint newLocalEntityId, uint newTick)
    {
        int num = 3;
        BackupComponent backupComponent = (BackupComponent)CreateComponent(num, typeof(BackupComponent));
        backupComponent.localEntityId = newLocalEntityId;
        backupComponent.tick = newTick;
        ReplaceComponent(num, backupComponent);
    }

    public void RemoveBackup()
    {
        RemoveComponent(3);
    }

    public void AddDestination(Vector2 newValue)
    {
        int num = 5;
        DestinationComponent destinationComponent = (DestinationComponent)CreateComponent(num, typeof(DestinationComponent));
        destinationComponent.value = newValue;
        AddComponent(num, destinationComponent);
    }

    public void ReplaceDestination(Vector2 newValue)
    {
        int num = 5;
        DestinationComponent destinationComponent = (DestinationComponent)CreateComponent(num, typeof(DestinationComponent));
        destinationComponent.value = newValue;
        ReplaceComponent(num, destinationComponent);
    }

    public void RemoveDestination()
    {
        RemoveComponent(5);
    }

    public void AddHealth(int newValue)
    {
        int num = 8;
        HealthComponent healthComponent = (HealthComponent)CreateComponent(num, typeof(HealthComponent));
        healthComponent.value = newValue;
        AddComponent(num, healthComponent);
    }

    public void ReplaceHealth(int newValue)
    {
        int num = 8;
        HealthComponent healthComponent = (HealthComponent)CreateComponent(num, typeof(HealthComponent));
        healthComponent.value = newValue;
        ReplaceComponent(num, healthComponent);
    }

    public void RemoveHealth()
    {
        RemoveComponent(8);
    }


    //ID 希望自增，且只读
    public void AddId(uint newValue)
    {
        int num = 9;
        IdComponent idComponent = (IdComponent)CreateComponent(num, typeof(IdComponent));
        idComponent.value = newValue;
        AddComponent(num, idComponent);
    }

    public void ReplaceId(uint newValue)
    {
        int num = 9;
        IdComponent idComponent = (IdComponent)CreateComponent(num, typeof(IdComponent));
        idComponent.value = newValue;
        ReplaceComponent(num, idComponent);
    }

    public void RemoveId()
    {
        RemoveComponent(9);
    }

    //public void ReplaceModel(uint modelId)
    //{
    //    ModelComponent model = (ModelComponent)CreateComponent()
    //}


    public void AddLocalId(uint newValue)
    {
        int num = 10;
        LocalIdComponent localIdComponent = (LocalIdComponent)CreateComponent(num, typeof(LocalIdComponent));
        localIdComponent.value = newValue;
        AddComponent(num, localIdComponent);
    }

    public void ReplaceLocalId(uint newValue)
    {
        int num = 10;
        LocalIdComponent localIdComponent = (LocalIdComponent)CreateComponent(num, typeof(LocalIdComponent));
        localIdComponent.value = newValue;
        ReplaceComponent(num, localIdComponent);
    }

    public void RemoveLocalId()
    {
        RemoveComponent(10);
    }

    public void AddMaxSpeed(Fix64 newValue)
    {
        int num = 11;
        MaxSpeedComponent maxSpeedComponent = (MaxSpeedComponent)CreateComponent(num, typeof(MaxSpeedComponent));
        maxSpeedComponent.value = newValue;
        AddComponent(num, maxSpeedComponent);
    }

    public void ReplaceMaxSpeed(Fix64 newValue)
    {
        int num = 11;
        MaxSpeedComponent maxSpeedComponent = (MaxSpeedComponent)CreateComponent(num, typeof(MaxSpeedComponent));
        maxSpeedComponent.value = newValue;
        ReplaceComponent(num, maxSpeedComponent);
    }

    public void RemoveMaxSpeed()
    {
        RemoveComponent(11);
    }

    public void AddPosition(Vector2 newValue)
    {
        int num = 13;
        PositionComponent positionComponent = (PositionComponent)CreateComponent(num, typeof(PositionComponent));
        positionComponent.value = newValue;
        AddComponent(num, positionComponent);
    }

    public void ReplacePosition(Vector2 newValue)
    {
        int num = 13;
        PositionComponent positionComponent = (PositionComponent)CreateComponent(num, typeof(PositionComponent));
        positionComponent.value = newValue;
        ReplaceComponent(num, positionComponent);
    }

    public void RemovePosition()
    {
        RemoveComponent(13);
    }

    public void AddTeam(byte newValue)
    {
        int num = 16;
        TeamComponent teamComponent = (TeamComponent)CreateComponent(num, typeof(TeamComponent));
        teamComponent.value = newValue;
        AddComponent(num, teamComponent);
    }

    public void ReplaceTeam(byte newValue)
    {
        int num = 16;
        TeamComponent teamComponent = (TeamComponent)CreateComponent(num, typeof(TeamComponent));
        teamComponent.value = newValue;
        ReplaceComponent(num, teamComponent);
    }

    public void RemoveTeam()
    {
        RemoveComponent(16);
    }

    public void AddVelocity(Vector2 newValue)
    {
        int num = 17;
        VelocityComponent velocityComponent = (VelocityComponent)CreateComponent(num, typeof(VelocityComponent));
        velocityComponent.value = newValue;
        AddComponent(num, velocityComponent);
    }

    public void ReplaceVelocity(Vector2 newValue)
    {
        int num = 17;
        VelocityComponent velocityComponent = (VelocityComponent)CreateComponent(num, typeof(VelocityComponent));
        velocityComponent.value = newValue;
        ReplaceComponent(num, velocityComponent);
    }

    public void RemoveVelocity()
    {
        RemoveComponent(17);
    }

    public void AddDestinationListener(List<IDestinationListener> newValue)
    {
        int num = 0;
        DestinationListenerComponent destinationListenerComponent = (DestinationListenerComponent)CreateComponent(num, typeof(DestinationListenerComponent));
        destinationListenerComponent.value = newValue;
        AddComponent(num, destinationListenerComponent);
    }

    public void ReplaceDestinationListener(List<IDestinationListener> newValue)
    {
        int num = 0;
        DestinationListenerComponent destinationListenerComponent = (DestinationListenerComponent)CreateComponent(num, typeof(DestinationListenerComponent));
        destinationListenerComponent.value = newValue;
        ReplaceComponent(num, destinationListenerComponent);
    }

    public void RemoveDestinationListener()
    {
        RemoveComponent(0);
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
        PositionListenerComponent positionListenerComponent = (PositionListenerComponent)CreateComponent(num, typeof(PositionListenerComponent));
        positionListenerComponent.value = newValue;
        AddComponent(num, positionListenerComponent);
    }

    public void ReplacePositionListener(List<IPositionListener> newValue)
    {
        int num = 18;
        PositionListenerComponent positionListenerComponent = (PositionListenerComponent)CreateComponent(num, typeof(PositionListenerComponent));
        positionListenerComponent.value = newValue;
        ReplaceComponent(num, positionListenerComponent);
    }

    public void RemovePositionListener()
    {
        RemoveComponent(18);
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
