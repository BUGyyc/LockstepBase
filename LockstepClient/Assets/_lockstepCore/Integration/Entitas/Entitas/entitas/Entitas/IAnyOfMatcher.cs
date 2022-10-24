namespace Entitas
{

    public interface IAnyOfMatcher<TEntity> : INoneOfMatcher<TEntity>, ICompoundMatcher<TEntity>, IMatcher<TEntity> where TEntity : class, IEntity
    {
        INoneOfMatcher<TEntity> NoneOf(params int[] indices);

        INoneOfMatcher<TEntity> NoneOf(params IMatcher<TEntity>[] matchers);
    }
}