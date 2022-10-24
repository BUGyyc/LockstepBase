using System;

namespace Entitas
{

    public abstract class AbstractEntityIndex<TEntity, TKey> : IEntityIndex where TEntity : class, IEntity
    {
        protected readonly string _name;

        protected readonly IGroup<TEntity> _group;

        protected readonly Func<TEntity, IComponent, TKey> _getKey;

        protected readonly Func<TEntity, IComponent, TKey[]> _getKeys;

        protected readonly bool _isSingleKey;

        public string name => _name;

        protected AbstractEntityIndex(string name, IGroup<TEntity> group, Func<TEntity, IComponent, TKey> getKey)
        {
            _name = name;
            _group = group;
            _getKey = getKey;
            _isSingleKey = true;
        }

        protected AbstractEntityIndex(string name, IGroup<TEntity> group, Func<TEntity, IComponent, TKey[]> getKeys)
        {
            _name = name;
            _group = group;
            _getKeys = getKeys;
            _isSingleKey = false;
        }

        public virtual void Activate()
        {
            _group.OnEntityAdded += onEntityAdded;
            _group.OnEntityRemoved += onEntityRemoved;
        }

        public virtual void Deactivate()
        {
            _group.OnEntityAdded -= onEntityAdded;
            _group.OnEntityRemoved -= onEntityRemoved;
            clear();
        }

        public override string ToString()
        {
            return name;
        }

        protected void indexEntities(IGroup<TEntity> group)
        {
            foreach (TEntity item in group)
            {
                if (_isSingleKey)
                {
                    addEntity(_getKey(item, null), item);
                    continue;
                }
                TKey[] array = _getKeys(item, null);
                for (int i = 0; i < array.Length; i++)
                {
                    addEntity(array[i], item);
                }
            }
        }

        protected void onEntityAdded(IGroup<TEntity> group, TEntity entity, int index, IComponent component)
        {
            if (_isSingleKey)
            {
                addEntity(_getKey(entity, component), entity);
                return;
            }
            TKey[] array = _getKeys(entity, component);
            for (int i = 0; i < array.Length; i++)
            {
                addEntity(array[i], entity);
            }
        }

        protected void onEntityRemoved(IGroup<TEntity> group, TEntity entity, int index, IComponent component)
        {
            if (_isSingleKey)
            {
                removeEntity(_getKey(entity, component), entity);
                return;
            }
            TKey[] array = _getKeys(entity, component);
            for (int i = 0; i < array.Length; i++)
            {
                removeEntity(array[i], entity);
            }
        }

        protected abstract void addEntity(TKey key, TEntity entity);

        protected abstract void removeEntity(TKey key, TEntity entity);

        protected abstract void clear();

        ~AbstractEntityIndex()
        {
            Deactivate();
        }
    }
}