namespace Entitas
{
    public static class CollectorContextExtension
    {
        /// Creates a Collector.
        public static ICollector<TEntity> CreateCollector<TEntity>(this IContext<TEntity> context, IMatcher<TEntity> matcher) where TEntity : class, IEntity
        {
            return context.CreateCollector(new TriggerOnEvent<TEntity>(matcher, GroupEvent.Added));
        }

        /// Creates a Collector.
        public static ICollector<TEntity> CreateCollector<TEntity>(this IContext<TEntity> context, params TriggerOnEvent<TEntity>[] triggers) where TEntity : class, IEntity
        {
            IGroup<TEntity>[] array = new IGroup<TEntity>[triggers.Length];
            GroupEvent[] array2 = new GroupEvent[triggers.Length];
            for (int i = 0; i < triggers.Length; i++)
            {
                array[i] = context.GetGroup(triggers[i].matcher);
                array2[i] = triggers[i].groupEvent;
            }
            return new Collector<TEntity>(array, array2);
        }
    }
}


