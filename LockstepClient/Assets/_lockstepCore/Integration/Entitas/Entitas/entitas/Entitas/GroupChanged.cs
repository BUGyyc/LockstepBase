namespace Entitas
{
    public delegate void GroupChanged<TEntity>(IGroup<TEntity> group, TEntity entity, int index, IComponent component) where TEntity : class, IEntity;
}

