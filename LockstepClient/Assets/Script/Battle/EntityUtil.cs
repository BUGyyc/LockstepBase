using Entitas;
using Entitas.Unity;
using UnityEngine;
using System.Collections.Generic;
using Lockstep;
public static class EntityUtil
{

    private static Dictionary<uint, GameObject> linkedEntities = new Dictionary<uint, GameObject>();

    // public const byte BaseCharacterEntityID = 0;

    public static uint BaseCharacterEntityID = 10000;

    public static uint AutoCreateEntityID = BaseCharacterEntityID;



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

    public static GameEntity CreateCharacterEntity(byte actorId)
    {
        GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
        gameEntity.AddId(actorId);
        gameEntity.AddActorId(actorId);
        gameEntity.AddLocalId(AutoCreateEntityID);
        gameEntity.AddVelocity(BEPUutilities.Vector2.Zero);

        gameEntity.AddCharacterInput(0, LVector2.zero, LVector3.zero);
        gameEntity.AddMove(GameSetting.HERO_BASE_SPEED, MoveState.Idle);

        int index = actorId;

        gameEntity.AddPosition(Lockstep.LVector3.zero + 3 * index * Lockstep.LVector3.forward, Lockstep.LQuaternion.identity);

        // gameEntity

        //初始动画
        AnimationComponent animation = new AnimationComponent()
        {
            readyPlay = true,
            animationName = GameSetting.HeroInitAnimationName
        };
        gameEntity.AddComponent(GameComponentsLookup.Animation, animation);
        // gameEntity.Ad
        var go = LoadEntityView(gameEntity, GameSetting.HeroPrefabPath);

        if (go != null)
        {
            var aniBindEntity = go.GetComponentInChildren<AnimatorBindEntity>();
            if (aniBindEntity) aniBindEntity.SetGameEntity(gameEntity);
        }

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

    private static UnityEngine.GameObject LoadEntityView(GameEntity entity, string path)
    {
        bool isLocalMaster = entity.id.value == ActionWorld.Instance.LocalCharacterEntityId;
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

            if (isLocalMaster)
            {
                CharacterCameraController.Instance.InitCharacterCamera(viewGo.transform);
            }
        }

        return viewGo;
    }
}

