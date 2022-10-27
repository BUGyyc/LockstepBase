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


            foreach (ActorEntity item in enumerable)
            {
                ActorEntity entityWithId = Contexts.actor.GetEntityWithId(item.backup.actorId);
                IEnumerable<int> enumerable2 = (entityWithId).GetComponentIndices().Except((item).GetComponentIndices().Except(new int[1]).Concat(new int[1] { ActorComponentsLookup.Id/*2 */}));
                //先清理所有Component ???
                foreach (int item2 in enumerable2)
                {
                    (entityWithId).RemoveComponent(item2);
                }

                PublicMemberInfoEntityExtension.CopyTo((IEntity)(object)item, (IEntity)(object)entityWithId, true, (item).GetComponentIndices().Except(new int[1]).ToArray());
            }



            GameEntity[] entities = ContextExtension.GetEntities((IContext<GameEntity>)(object)Contexts.game, GameMatcher.LocalId);
            //目标帧号的备份 GameEntity
            List<GameEntity> list = (from e in ContextExtension.GetEntities((IContext<GameEntity>)(object)Contexts.game, GameMatcher.Backup)
                                     where e.backup.tick == resultTick
                                     select e).ToList();

            IEnumerable<uint> backupEntityIds = list.Select((GameEntity entity) => entity.backup.localEntityId);
            List<GameEntity> list2 = entities.Where((GameEntity entity) => !backupEntityIds.Contains(entity.localId.value)).ToList();

            //标记销毁多余的GameEntity
            foreach (GameEntity item3 in list2)
            {
                item3.isDestroyed = true;
            }

            //备份帧号大于目标帧号的GameEntity 也是不需要的直接销毁
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
                GameEntity entityWithLocalId = Contexts.game.GetEntityWithLocalId(item6.backup.localEntityId);
                IEnumerable<int> enumerable3 = (entityWithLocalId).GetComponentIndices().Except((item6).GetComponentIndices().Except(new int[1] { 3 }).Concat(new int[1] { GameComponentsLookup.LocalId/*10*/ }));

                //移除多余的Component
                foreach (int item7 in enumerable3)
                {
                    (entityWithLocalId).RemoveComponent(item7);
                }
                PublicMemberInfoEntityExtension.CopyTo((IEntity)(object)item6, (IEntity)(object)entityWithLocalId, true, (item6).GetComponentIndices().Except(new int[1] { 3 }).ToArray());
            }
            (_systems).Cleanup();
            Contexts.gameState.ReplaceTick(resultTick);

            //追到目标帧号
            while (Tick <= tick)
            {
                Simulate();
            }
        }
    }
}

