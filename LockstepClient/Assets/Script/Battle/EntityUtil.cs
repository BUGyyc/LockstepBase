using Entitas;
using Lockstep.Core.State.Game;
public static class EntityUtil
{
    //public static T AddComponent<T>(this GameEntity self, T com) where T : IComponent
    //{
    //    //self.

    //    self.CreateComponent();
    //    return T;
    //}
    public static uint AutoCreateEntityID = 1000;

    /// <summary>
    /// 
    /// </summary>
    public static uint LocalEntityID = 1000;

    public static IComponent AddComponent(this GameEntity self, int index, out bool result)
    {
        if (self.HasComponent(index))
        {
            result = false;
            return self.GetComponent(index);
        }

        var type = GameComponentsLookup.componentTypes[index];
        var component = self.CreateComponent(index, type);
        self.AddComponent(index, component);
        result = true;
        return component;
    }

    public static IComponent AddComponent(this GameEntity self, int index)
    {
        var type = GameComponentsLookup.componentTypes[index];
        var com = self.CreateComponent(index, type);
        self.AddComponent(index, com);
        return com;
    }

    public static GameEntity CreateEntity()
    {
        var entity = Contexts.sharedInstance.game.CreateEntity();

        IdComponent idComponent = entity.AddComponent(GameComponentsLookup.Id) as IdComponent;
        idComponent.value = AutoCreateEntityID++;


        //if (entity.HasComponent(GameComponentsLookup.LocalId) == false)
        //{
        //    LocalIdComponent localId = new LocalIdComponent();
        //    localId.value = LocalEntityID++;
        //    entity.AddComponent(GameComponentsLookup.LocalId, localId);
        //}
        //else
        //{
        //    LocalIdComponent localId = entity.GetComponent(GameComponentsLookup.LocalId) as LocalIdComponent;
        //    localId.value = LocalEntityID++;
        //}


        //ActorEntity entityWithId = Contexts.sharedInstance.actor.GetEntityWithId(ActionWorld.Instance.LocalActorId);
        //uint value = entityWithId.entityCount.value;
        //entityWithId.ReplaceEntityCount(value + 1);

        return entity;
    }
}

