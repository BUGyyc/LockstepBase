using System.Collections.Generic;

namespace Entitas
{

    public interface IGroup
    {
        int count { get; }

        void RemoveAllEventHandlers();
    }
    public interface IGroup<TEntity> : IGroup where TEntity : class, IEntity
    {
        IMatcher<TEntity> matcher { get; }

        event GroupChanged<TEntity> OnEntityAdded;

        event GroupChanged<TEntity> OnEntityRemoved;

        event GroupUpdated<TEntity> OnEntityUpdated;

        void HandleEntitySilently(TEntity entity);

        void HandleEntity(TEntity entity, int index, IComponent component);

        GroupChanged<TEntity> HandleEntity(TEntity entity);

        void UpdateEntity(TEntity entity, int index, IComponent previousComponent, IComponent newComponent);

        bool ContainsEntity(TEntity entity);

        TEntity[] GetEntities();

        List<TEntity> GetEntities(List<TEntity> buffer);

        TEntity GetSingleEntity();

        IEnumerable<TEntity> AsEnumerable();

        HashSet<TEntity>.Enumerator GetEnumerator();
    }
}