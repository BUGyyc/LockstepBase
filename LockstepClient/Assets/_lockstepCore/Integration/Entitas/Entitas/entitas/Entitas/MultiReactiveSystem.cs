﻿using System.Collections.Generic;

namespace Entitas
{

    /// A ReactiveSystem calls Execute(entities) if there were changes based on
    /// the specified Collector and will only pass in changed entities.
    /// A common use-case is to react to changes, e.g. a change of the position
    /// of an entity to update the gameObject.transform.position
    /// of the related gameObject.
    public abstract class MultiReactiveSystem<TEntity, TContexts> : IReactiveSystem, IExecuteSystem, ISystem where TEntity : class, IEntity where TContexts : class, IContexts
    {
        private readonly ICollector[] _collectors;

        private readonly HashSet<TEntity> _collectedEntities;

        private readonly List<TEntity> _buffer;

        private string _toStringCache;

        protected MultiReactiveSystem(TContexts contexts)
        {
            _collectors = GetTrigger(contexts);
            _collectedEntities = new HashSet<TEntity>();
            _buffer = new List<TEntity>();
        }

        protected MultiReactiveSystem(ICollector[] collectors)
        {
            _collectors = collectors;
            _collectedEntities = new HashSet<TEntity>();
            _buffer = new List<TEntity>();
        }

        /// Specify the collector that will trigger the ReactiveSystem.
        protected abstract ICollector[] GetTrigger(TContexts contexts);

        /// This will exclude all entities which don't pass the filter.
        protected abstract bool Filter(TEntity entity);

        protected abstract void Execute(List<TEntity> entities);

        /// Activates the ReactiveSystem and starts observing changes
        /// based on the specified Collector.
        /// ReactiveSystem are activated by default.
        public void Activate()
        {
            for (int i = 0; i < _collectors.Length; i++)
            {
                _collectors[i].Activate();
            }
        }

        /// Deactivates the ReactiveSystem.
        /// No changes will be tracked while deactivated.
        /// This will also clear the ReactiveSystem.
        /// ReactiveSystem are activated by default.
        public void Deactivate()
        {
            for (int i = 0; i < _collectors.Length; i++)
            {
                _collectors[i].Deactivate();
            }
        }

        /// Clears all accumulated changes.
        public void Clear()
        {
            for (int i = 0; i < _collectors.Length; i++)
            {
                _collectors[i].ClearCollectedEntities();
            }
        }

        /// Will call Execute(entities) with changed entities
        /// if there are any. Otherwise it will not call Execute(entities).
        public void Execute()
        {
            for (int i = 0; i < _collectors.Length; i++)
            {
                ICollector collector = _collectors[i];
                if (collector.count != 0)
                {
                    _collectedEntities.UnionWith(collector.GetCollectedEntities<TEntity>());
                    collector.ClearCollectedEntities();
                }
            }
            foreach (TEntity collectedEntity in _collectedEntities)
            {
                if (Filter(collectedEntity))
                {
                    collectedEntity.Retain(this);
                    _buffer.Add(collectedEntity);
                }
            }
            if (_buffer.Count == 0)
            {
                return;
            }
            try
            {
                Execute(_buffer);
            }
            finally
            {
                for (int j = 0; j < _buffer.Count; j++)
                {
                    _buffer[j].Release(this);
                }
                _collectedEntities.Clear();
                _buffer.Clear();
            }
        }

        public override string ToString()
        {
            if (_toStringCache == null)
            {
                _toStringCache = "MultiReactiveSystem(" + GetType().Name + ")";
            }
            return _toStringCache;
        }

        ~MultiReactiveSystem()
        {
            Deactivate();
        }
    }
}