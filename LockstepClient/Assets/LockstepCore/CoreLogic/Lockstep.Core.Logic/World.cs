using System.Collections.Generic;
using System.Linq;
using Entitas;
using Lockstep.Common.Logging;
using Lockstep.Core.Logic.Systems;

namespace Lockstep.Core.Logic
{
    public class World
    {
        private readonly WorldSystems _systems;

        public Contexts Contexts { get; }

        public uint Tick => Contexts.gameState.tick.value;

        public World(Contexts contexts, IEnumerable<byte> actorIds, params Feature[] features)
        {
            Contexts = contexts;
            foreach (byte actorId in actorIds)
            {
                (Contexts.actor).CreateEntity().AddId(actorId);
            }
            _systems = new WorldSystems(Contexts, features);
            (_systems).Initialize();
        }

        /// <summary>
        /// 预测
        /// </summary>
        public void Predict()
        {
            if (!Contexts.gameState.isPredicting)
            {
                Contexts.gameState.isPredicting = true;
            }
            Log.Trace(this, "Predict " + Contexts.gameState.tick.value);
            (_systems).Execute();
            (_systems).Cleanup();
        }


        /// <summary>
        /// 模拟
        /// </summary>
        public void Simulate()
        {
            if (Contexts.gameState.isPredicting)
            {
                Contexts.gameState.isPredicting = false;
            }
            Log.Trace(this, "Simulate " + Contexts.gameState.tick.value);
            (_systems).Execute();
            (_systems).Cleanup();


            //创建了一个 DebugEntity，并且把计算所得 HashCode 给 DebugEntity
            DebugEntity debugEntity = Contexts.debug.CreateEntity();
            debugEntity.AddTick(Tick);
            debugEntity.AddHashCode(Contexts.gameState.hashCode.value);
        }

        /// <summary>
        /// 回滚到指定帧
        /// </summary>
        /// <param name="tick"></param>
        public void RevertToTick(uint tick)
        {
            List<uint> source = (from entity in ContextExtension.GetEntities((IContext<SnapshotEntity>)(object)Contexts.snapshot, SnapshotMatcher.Tick)
                                 where entity.tick.value <= tick
                                 select entity.tick.value).ToList();

            //把快照中的最大帧号取出来，其实就是找最近一次的帧号
            uint resultTick = source.Any() ? source.Max() : 0u;

            Log.Info(this, "Rolling back from " + resultTick + " to " + Contexts.gameState.tick.value);

            //通过本地的备份数据，找目标帧号的 ActorEntity 数据
            IEnumerable<ActorEntity> enumerable = from e in ContextExtension.GetEntities((IContext<ActorEntity>)(object)Contexts.actor, ActorMatcher.Backup)
                                                  where e.backup.tick == resultTick
                                                  select e;

            //遍历帧缓存下的 Actor
            foreach (ActorEntity item in enumerable)
            {
                //游戏世界的目标 Actor
                ActorEntity entityWithId = Contexts.actor.GetEntityWithId(item.backup.actorId);
                //获得Actor 上，除Id以外的全部 Component
                IEnumerable<int> enumerable2 = (entityWithId).GetComponentIndices().Except((item).GetComponentIndices().Except(new int[1]).Concat(new int[1] { ActorComponentsLookup.Id/*2 */}));
                //先清理所有Component,除 Id 以外
                foreach (int item2 in enumerable2)
                {
                    //游戏事件的目标 Actor 移除 所有Component，除 Id 以外
                    (entityWithId).RemoveComponent(item2);
                }

                //把缓存的 Actor 上的 Component ,全部拷贝给 游戏世界的 Actor; 
                PublicMemberInfoEntityExtension.CopyTo((IEntity)(object)item, (IEntity)(object)entityWithId, true, (item).GetComponentIndices().Except(new int[1]).ToArray());
            }


            //游戏世界内全部 GameEntity ??
            GameEntity[] entities = ContextExtension.GetEntities((IContext<GameEntity>)(object)Contexts.game, GameMatcher.LocalId);
            //目标帧号的备份 GameEntity
            List<GameEntity> list = (from e in ContextExtension.GetEntities((IContext<GameEntity>)(object)Contexts.game, GameMatcher.Backup)
                                     where e.backup.tick == resultTick
                                     select e).ToList();

            //备份中的GameEntity 内的 LocalEntityId,收集起来
            IEnumerable<uint> backupEntityIds = list.Select((GameEntity entity) => entity.backup.localEntityId);
            //游戏世界内存在，但缓存帧中不存在的GameEntity,收集起来
            List<GameEntity> list2 = entities.Where((GameEntity entity) => !backupEntityIds.Contains(entity.localId.value)).ToList();

            //标记销毁多余的GameEntity
            foreach (GameEntity item3 in list2)
            {
                item3.isDestroyed = true;
            }

            //另外，备份帧号大于目标帧号的GameEntity 也是不需要的直接销毁
            foreach (GameEntity item4 in from e in ContextExtension.GetEntities((IContext<GameEntity>)(object)Contexts.game, GameMatcher.Backup)
                                         where e.backup.tick > resultTick
                                         select e)
            {
                (item4).Destroy();
            }

            //然后要把大于目标帧号的历史备份都销毁
            foreach (SnapshotEntity item5 in from e in ContextExtension.GetEntities((IContext<SnapshotEntity>)(object)Contexts.snapshot, SnapshotMatcher.Tick)
                                             where e.tick.value > resultTick
                                             select e)
            {
                (item5).Destroy();
            }

            //最后把GameEntity 内的 Component 数据调整到目标状态
            foreach (GameEntity item6 in list)
            {
                //层层筛选后，应该被重置的GameEntity
                GameEntity entityWithLocalId = Contexts.game.GetEntityWithLocalId(item6.backup.localEntityId);
                IEnumerable<int> enumerable3 = (entityWithLocalId).GetComponentIndices().Except((item6).GetComponentIndices().Except(new int[1] { GameComponentsLookup.Backup /**3**/ }).Concat(new int[1] { GameComponentsLookup.LocalId/*10*/ }));

                //移除多余的Component
                foreach (int item7 in enumerable3)
                {
                    (entityWithLocalId).RemoveComponent(item7);
                }
                //拷贝进去
                PublicMemberInfoEntityExtension.CopyTo((IEntity)(object)item6, (IEntity)(object)entityWithLocalId, true, (item6).GetComponentIndices().Except(new int[1] { GameComponentsLookup.Backup /**3**/ }).ToArray());
            }
            //进行一次清理，方便做一些释放与回收
            (_systems).Cleanup();
            //设置目标帧号
            Contexts.gameState.ReplaceTick(resultTick);

            //TODO: 这里应该写成控制速率，防止帧数差太多的追帧压力
            //追到目标帧号
            while (Tick <= tick)
            {
                Simulate();
            }
        }
    }
}

