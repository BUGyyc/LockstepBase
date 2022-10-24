using System.Collections.Generic;

namespace Entitas
{

    public interface IContext
    {
        int totalComponents { get; }

        Stack<IComponent>[] componentPools { get; }

        ContextInfo contextInfo { get; }

        int count { get; }

        int reusableEntitiesCount { get; }

        int retainedEntitiesCount { get; }

        event ContextEntityChanged OnEntityCreated;

        event ContextEntityChanged OnEntityWillBeDestroyed;

        event ContextEntityChanged OnEntityDestroyed;

        event ContextGroupChanged OnGroupCreated;

        void DestroyAllEntities();

        void AddEntityIndex(IEntityIndex entityIndex);

        IEntityIndex GetEntityIndex(string name);

        void ResetCreationIndex();

        void ClearComponentPool(int index);

        void ClearComponentPools();

        void RemoveAllEventHandlers();

        void Reset();
    }
    public interface IContext<TEntity> : IContext where TEntity : class, IEntity
    {
        TEntity CreateEntity();

        bool HasEntity(TEntity entity);

        TEntity[] GetEntities();

        IGroup<TEntity> GetGroup(IMatcher<TEntity> matcher);
    }
}