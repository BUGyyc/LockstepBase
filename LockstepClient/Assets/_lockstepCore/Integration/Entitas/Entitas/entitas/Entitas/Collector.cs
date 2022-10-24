using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entitas
{

    /// A Collector can observe one or more groups from the same context
    /// and collects changed entities based on the specified groupEvent.
    public class Collector<TEntity> : ICollector<TEntity>, ICollector where TEntity : class, IEntity
    {
        private readonly HashSet<TEntity> _collectedEntities;

        private readonly IGroup<TEntity>[] _groups;

        private readonly GroupEvent[] _groupEvents;

        private GroupChanged<TEntity> _addEntityCache;

        private string _toStringCache;

        private StringBuilder _toStringBuilder;

        /// Returns all collected entities.
        /// Call collector.ClearCollectedEntities()
        /// once you processed all entities.
        public HashSet<TEntity> collectedEntities => _collectedEntities;

        /// Returns the number of all collected entities.
        public int count => _collectedEntities.Count;

        /// Creates a Collector and will collect changed entities
        /// based on the specified groupEvent.
        public Collector(IGroup<TEntity> group, GroupEvent groupEvent)
            : this(new IGroup<TEntity>[1] { group }, new GroupEvent[1] { groupEvent })
        {
        }

        /// Creates a Collector and will collect changed entities
        /// based on the specified groupEvents.
        public Collector(IGroup<TEntity>[] groups, GroupEvent[] groupEvents)
        {
            _groups = groups;
            _collectedEntities = new HashSet<TEntity>(EntityEqualityComparer<TEntity>.comparer);
            _groupEvents = groupEvents;
            if (groups.Length != groupEvents.Length)
            {
                throw new CollectorException("Unbalanced count with groups (" + groups.Length + ") and group events (" + groupEvents.Length + ").", "Group and group events count must be equal.");
            }
            _addEntityCache = addEntity;
            Activate();
        }

        /// Activates the Collector and will start collecting
        /// changed entities. Collectors are activated by default.
        public void Activate()
        {
            for (int i = 0; i < _groups.Length; i++)
            {
                IGroup<TEntity> group = _groups[i];
                switch (_groupEvents[i])
                {
                    case GroupEvent.Added:
                        group.OnEntityAdded -= _addEntityCache;
                        group.OnEntityAdded += _addEntityCache;
                        break;
                    case GroupEvent.Removed:
                        group.OnEntityRemoved -= _addEntityCache;
                        group.OnEntityRemoved += _addEntityCache;
                        break;
                    case GroupEvent.AddedOrRemoved:
                        group.OnEntityAdded -= _addEntityCache;
                        group.OnEntityAdded += _addEntityCache;
                        group.OnEntityRemoved -= _addEntityCache;
                        group.OnEntityRemoved += _addEntityCache;
                        break;
                }
            }
        }

        /// Deactivates the Collector.
        /// This will also clear all collected entities.
        /// Collectors are activated by default.
        public void Deactivate()
        {
            for (int i = 0; i < _groups.Length; i++)
            {
                IGroup<TEntity> obj = _groups[i];
                obj.OnEntityAdded -= _addEntityCache;
                obj.OnEntityRemoved -= _addEntityCache;
            }
            ClearCollectedEntities();
        }

        /// Returns all collected entities and casts them.
        /// Call collector.ClearCollectedEntities()
        /// once you processed all entities.
        public IEnumerable<TCast> GetCollectedEntities<TCast>() where TCast : class, IEntity
        {
            return _collectedEntities.Cast<TCast>();
        }

        /// Clears all collected entities.
        public void ClearCollectedEntities()
        {
            foreach (TEntity collectedEntity in _collectedEntities)
            {
                collectedEntity.Release(this);
            }
            _collectedEntities.Clear();
        }

        private void addEntity(IGroup<TEntity> group, TEntity entity, int index, IComponent component)
        {
            if (_collectedEntities.Add(entity))
            {
                entity.Retain(this);
            }
        }

        public override string ToString()
        {
            if (_toStringCache == null)
            {
                if (_toStringBuilder == null)
                {
                    _toStringBuilder = new StringBuilder();
                }
                _toStringBuilder.Length = 0;
                _toStringBuilder.Append("Collector(");
                int num = _groups.Length - 1;
                for (int i = 0; i < _groups.Length; i++)
                {
                    _toStringBuilder.Append(_groups[i]);
                    if (i < num)
                    {
                        _toStringBuilder.Append(", ");
                    }
                }
                _toStringBuilder.Append(")");
                _toStringCache = _toStringBuilder.ToString();
            }
            return _toStringCache;
        }

        ~Collector()
        {
            Deactivate();
        }
    }
}