namespace Entitas
{
    public interface INoneOfMatcher<TEntity> : ICompoundMatcher<TEntity>, IMatcher<TEntity> where TEntity : class, IEntity
    {
    }
}


