using Entitas;
using Entitas.Unity;
using UnityEngine;
using System.Collections.Generic;
public static class EntityUtil
{

    private static Dictionary<uint, GameObject> linkedEntities = new Dictionary<uint, GameObject>();
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
        GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
        gameEntity.AddId(AutoCreateEntityID);
        gameEntity.AddActorId(ActionWorld.Instance.LocalActorId);
        gameEntity.AddLocalId(AutoCreateEntityID);
        gameEntity.AddVelocity(BEPUutilities.Vector2.Zero);
        gameEntity.AddPosition(Lockstep.LVector3.zero);

        //初始动画
        AnimationComponent animation = new AnimationComponent()
        {
            readyPlay = true,
            animationName = "Idle_Wait_C"
        };
        gameEntity.AddComponent(GameComponentsLookup.Animation, animation);
        LoadEntityView(gameEntity, "Prefabs/ClazyRunner");
        AutoCreateEntityID++;
        return gameEntity;
    }

    public static GameObject GetEntityGameObject(uint entityId)
    {
        if (linkedEntities.ContainsKey(entityId))
        {
            return linkedEntities[entityId];
        }
        return null;
    }

    private static void LoadEntityView(GameEntity entity, string path)
    {
        //var viewGo = UnityEngine.Object.Instantiate(_entityDatabase.Entities[configId]).gameObject;
        var obj = Resources.Load<GameObject>(path);
        var viewGo = GameObject.Instantiate(obj);
        if (viewGo != null)
        {
            viewGo.Link(entity);

            var componentSetters = viewGo.GetComponents<IComponentSetter>();
            foreach (var componentSetter in componentSetters)
            {
                componentSetter.SetComponent(entity);
                UnityEngine.Object.Destroy((MonoBehaviour)componentSetter);
            }

            var eventListeners = viewGo.GetComponents<IEventListener>();
            foreach (var listener in eventListeners)
            {
                listener.RegisterListeners(entity);
            }

            linkedEntities.Add(entity.localId.value, viewGo);

            //if (isMaster)
            //{
            CharacterCameraController.Instance.InitCharacterCamera(viewGo.transform);
            //}
        }
    }
}

