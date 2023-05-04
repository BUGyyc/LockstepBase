using Entitas;
using Entitas.Unity;
using UnityEngine;
using System.Collections.Generic;
using Lockstep;
using ET;
using BM;

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


    public static GameEntity CreateAI(int index)
    {
        GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
        AddBaseComponent(gameEntity, GameSetting.AI_ACTOR, EntityType.AI);

        gameEntity.AddAI((int)AutoCreateEntityID, AIAction.Idle, 0);

        // int index = actorId;
        gameEntity.AddPosition(Lockstep.LVector3.zero + 3 * index * Lockstep.LVector3.forward, Lockstep.LQuaternion.identity);

        LoadAsyncEntityView(gameEntity, GameSetting.AIPrefab, false).Coroutine();

        return gameEntity;
    }

    public static GameEntity CreateSimpleHero(byte actorId)
    {
        GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
        AddBaseComponent(gameEntity, actorId, EntityType.Hero);


        gameEntity.AddCharacterInput(0, LVector2.zero, LVector3.zero);
        gameEntity.AddMove(GameSetting.HERO_BASE_SPEED, MoveState.Idle, LVector3.zero);
        gameEntity.AddCharacterAttr(LFloat.one * 100, LFloat.one * 100);
        gameEntity.AddSkill(0, false);

        int index = actorId;
        gameEntity.AddPosition(Lockstep.LVector3.zero + 3 * index * Lockstep.LVector3.forward, Lockstep.LQuaternion.identity);

        LoadAsyncEntityView(gameEntity, GameSetting.AIPrefab, true).Coroutine();

        return gameEntity;
    }

    public static GameEntity CreateCharacterEntity(byte actorId)
    {
        GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
        // gameEntity.AddId(AutoCreateEntityID);
        // gameEntity.AddActorId(actorId);
        // gameEntity.AddLocalId(AutoCreateEntityID);
        AddBaseComponent(gameEntity, actorId, EntityType.Hero);

        // gameEntity.AddVelocity(BEPUutilities.Vector2.Zero);

        gameEntity.AddCharacterInput(0, LVector2.zero, LVector3.zero);
        gameEntity.AddMove(GameSetting.HERO_BASE_SPEED, MoveState.Idle, LVector3.zero);

        gameEntity.AddCharacterAttr(LFloat.one * 100, LFloat.one * 100);

        gameEntity.AddSkill(0, false);

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
        LoadAsyncEntityView(gameEntity, GameSetting.HeroPrefabPath, true).Coroutine();

        //if (go != null)
        //{
        //    var aniBindEntity = go.GetComponentInChildren<AnimatorBindEntity>();
        //    if (aniBindEntity) aniBindEntity.SetGameEntity(gameEntity);
        //}

        return gameEntity;
    }


    public static GameEntity CreateBulletEntity(byte actorId, GameEntity shooter, bool initSetPosition = true)
    {
        GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
        AddBaseComponent(gameEntity, actorId, EntityType.Bullet);

        gameEntity.AddPosition(shooter.position.value + LVector3.up, shooter.position.rotate);
        gameEntity.AddBullet(LFloat.one);

        var forward = shooter.position.rotate * LVector3.forward;

        Debug.DrawRay(shooter.position.value.ToVector3(), forward.ToVector3(), Color.yellow, 0.2f);

        gameEntity.AddMove(GameSetting.BULLET_SPEED, MoveState.Walk, forward);
        //var go = LoadEntityView(gameEntity, GameSetting.BulletPrefab);

        LoadAsyncEntityView(gameEntity, GameSetting.BulletPrefab).Coroutine();

        return gameEntity;
    }

    public static void AddBaseComponent(GameEntity gameEntity, byte actorId, EntityType type)
    {
        gameEntity.AddId(AutoCreateEntityID);
        gameEntity.AddActorId(actorId);
        gameEntity.AddLocalId(AutoCreateEntityID);
        gameEntity.AddEntityType(type);
        AutoCreateEntityID++;
    }


    public static GameObject GetEntityGameObject(uint entityId)
    {
        if (linkedEntities.ContainsKey(entityId))
        {
            return linkedEntities[entityId];
        }
        return null;
    }

    private static async ETTask LoadAsyncEntityView(GameEntity entity, string path, bool isLocalMaster = false)
    {
        var obj = await AssetComponent.LoadAsync<GameObject>(out LoadHandler handler, path);
        if (obj == null) 
        {
            Debug.LogError($"加载失败    path {path}");
            return;
        }
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
        //return obj;
    }


    private static UnityEngine.GameObject LoadEntityView(GameEntity entity, string path, bool isLocalMaster = false)
    {
        // bool isLocalMaster = entity.actorId.value == ActionWorld.Instance.LocalCharacterEntityId;
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

