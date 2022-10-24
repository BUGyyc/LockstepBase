using System;
using System.Collections.Generic;

namespace Entitas
{

    public class EntityIndex<TEntity, TKey> : AbstractEntityIndex<TEntity, TKey> where TEntity : class, IEntity
    {
        private readonly Dictionary<TKey, HashSet<TEntity>> _index;

        public EntityIndex(string name, IGroup<TEntity> group, Func<TEntity, IComponent, TKey> getKey)
            : base(name, group, getKey)
        {
            _index = new Dictionary<TKey, HashSet<TEntity>>();
            Activate();
        }

        public EntityIndex(string name, IGroup<TEntity> group, Func<TEntity, IComponent, TKey[]> getKeys)
            : base(name, group, getKeys)
        {
            _index = new Dictionary<TKey, HashSet<TEntity>>();
            Activate();
        }

        public EntityIndex(string name, IGroup<TEntity> group, Func<TEntity, IComponent, TKey> getKey, IEqualityComparer<TKey> comparer)
            : base(name, group, getKey)
        {
            _index = new Dictionary<TKey, HashSet<TEntity>>(comparer);
            Activate();
        }

        public EntityIndex(string name, IGroup<TEntity> group, Func<TEntity, IComponent, TKey[]> getKeys, IEqualityComparer<TKey> comparer)
            : base(name, group, getKeys)
        {
            _index = new Dictionary<TKey, HashSet<TEntity>>(comparer);
            Activate();
        }

        public override void Activate()
        {
            base.Activate();
            indexEntities(_group);
        }

        public HashSet<TEntity> GetEntities(TKey key)
        {
            if (!_index.TryGetValue(key, out var value))
            {
                value = new HashSet<TEntity>(EntityEqualityComparer<TEntity>.comparer);
                _index.Add(key, value);
            }
            return value;
        }

        public override string ToString()
        {
            return "EntityIndex(" + base.name + ")";
        }

        protected override void clear()
        {
            foreach (HashSet<TEntity> value in _index.Values)
            {
                foreach (TEntity item in value)
                {
                    if (item.aerc is SafeAERC safeAERC)
                    {
                        if (safeAERC.owners.Contains(this))
                        {
                            item.Release(this);
                        }
                    }
                    else
                    {
                        item.Release(this);
                    }
                }
            }
            _index.Clear();
        }

        protected override void addEntity(TKey key, TEntity entity)
        {
            GetEntities(key).Add(entity);
            if (entity.aerc is SafeAERC safeAERC)
            {
                if (!safeAERC.owners.Contains(this))
                {
                    entity.Retain(this);
                }
            }
            else
            {
                entity.Retain(this);
            }
        }

        protected override void removeEntity(TKey key, TEntity entity)
        {
            GetEntities(key).Remove(entity);
            if (entity.aerc is SafeAERC safeAERC)
            {
                if (safeAERC.owners.Contains(this))
                {
                    entity.Release(this);
                }
            }
            else
            {
                entity.Release(this);
            }
        }
    }
}