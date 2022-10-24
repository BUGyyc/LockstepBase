using System.Linq;

namespace Entitas
{
    public class GroupSingleEntityException<TEntity> : EntitasException where TEntity : class, IEntity
    {
        public GroupSingleEntityException(IGroup<TEntity> group)
            : base(string.Concat("Cannot get the single entity from ", group, "!\nGroup contains ", group.count, " entities:"), string.Join("\n", (from e in @group.GetEntities()
                                                                                                                                                   select e.ToString()).ToArray()))
        {
        }
    }
}


