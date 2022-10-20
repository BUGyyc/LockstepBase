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
                ((Context<ActorEntity>)Contexts.actor).CreateEntity().AddId(actorId);
            }
            _systems = new WorldSystems(Contexts, features);
            ((Systems)_systems).Initialize();
        }

        public void Predict()
        {
            if (!Contexts.gameState.isPredicting)
            {
                Contexts.gameState.isPredicting = true;
            }
            Log.Trace(this, "Predict " + Contexts.gameState.tick.value);
            ((Systems)_systems).Execute();
            ((Systems)_systems).Cleanup();
        }

        public void Simulate()
        {
            if (Contexts.gameState.isPredicting)
            {
                Contexts.gameState.isPredicting = false;
            }
            Log.Trace(this, "Simulate " + Contexts.gameState.tick.value);
            ((Systems)_systems).Execute();
            ((Systems)_systems).Cleanup();
            DebugEntity debugEntity = ((Context<DebugEntity>)Contexts.debug).CreateEntity();
            debugEntity.AddTick(Tick);
            debugEntity.AddHashCode(Contexts.gameState.hashCode.value);
        }

        public void RevertToTick(uint tick)
        {
            List<uint> source = (from entity in ContextExtension.GetEntities<SnapshotEntity>((IContext<SnapshotEntity>)(object)Contexts.snapshot, SnapshotMatcher.Tick)
                                 where entity.tick.value <= tick
                                 select entity.tick.value).ToList();
            uint resultTick = (source.Any() ? source.Max() : 0u);
            Log.Info(this, "Rolling back from " + resultTick + " to " + Contexts.gameState.tick.value);
            IEnumerable<ActorEntity> enumerable = from e in ContextExtension.GetEntities<ActorEntity>((IContext<ActorEntity>)(object)Contexts.actor, ActorMatcher.Backup)
                                                  where e.backup.tick == resultTick
                                                  select e;
            foreach (ActorEntity item in enumerable)
            {
                ActorEntity entityWithId = Contexts.actor.GetEntityWithId(item.backup.actorId);
                IEnumerable<int> enumerable2 = ((Entity)entityWithId).GetComponentIndices().Except(((Entity)item).GetComponentIndices().Except(new int[1]).Concat(new int[1] { 2 }));
                foreach (int item2 in enumerable2)
                {
                    ((Entity)entityWithId).RemoveComponent(item2);
                }
                PublicMemberInfoEntityExtension.CopyTo((IEntity)(object)item, (IEntity)(object)entityWithId, true, ((Entity)item).GetComponentIndices().Except(new int[1]).ToArray());
            }
            GameEntity[] entities = ContextExtension.GetEntities<GameEntity>((IContext<GameEntity>)(object)Contexts.game, GameMatcher.LocalId);
            List<GameEntity> list = (from e in ContextExtension.GetEntities<GameEntity>((IContext<GameEntity>)(object)Contexts.game, GameMatcher.Backup)
                                     where e.backup.tick == resultTick
                                     select e).ToList();
            IEnumerable<uint> backupEntityIds = list.Select((GameEntity entity) => entity.backup.localEntityId);
            List<GameEntity> list2 = entities.Where((GameEntity entity) => !backupEntityIds.Contains(entity.localId.value)).ToList();
            foreach (GameEntity item3 in list2)
            {
                item3.isDestroyed = true;
            }
            foreach (GameEntity item4 in from e in ContextExtension.GetEntities<GameEntity>((IContext<GameEntity>)(object)Contexts.game, GameMatcher.Backup)
                                         where e.backup.tick > resultTick
                                         select e)
            {
                ((Entity)item4).Destroy();
            }
            foreach (SnapshotEntity item5 in from e in ContextExtension.GetEntities<SnapshotEntity>((IContext<SnapshotEntity>)(object)Contexts.snapshot, SnapshotMatcher.Tick)
                                             where e.tick.value > resultTick
                                             select e)
            {
                ((Entity)item5).Destroy();
            }
            foreach (GameEntity item6 in list)
            {
                GameEntity entityWithLocalId = Contexts.game.GetEntityWithLocalId(item6.backup.localEntityId);
                IEnumerable<int> enumerable3 = ((Entity)entityWithLocalId).GetComponentIndices().Except(((Entity)item6).GetComponentIndices().Except(new int[1] { 3 }).Concat(new int[1] { 10 }));
                foreach (int item7 in enumerable3)
                {
                    ((Entity)entityWithLocalId).RemoveComponent(item7);
                }
                PublicMemberInfoEntityExtension.CopyTo((IEntity)(object)item6, (IEntity)(object)entityWithLocalId, true, ((Entity)item6).GetComponentIndices().Except(new int[1] { 3 }).ToArray());
            }
            ((Systems)_systems).Cleanup();
            Contexts.gameState.ReplaceTick(resultTick);
            while (Tick <= tick)
            {
                Simulate();
            }
        }
    }
}

