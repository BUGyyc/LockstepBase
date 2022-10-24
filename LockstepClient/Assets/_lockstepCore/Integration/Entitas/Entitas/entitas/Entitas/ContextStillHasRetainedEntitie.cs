using System.Linq;

namespace Entitas
{
    public class ContextStillHasRetainedEntitiesException : EntitasException
    {
        public ContextStillHasRetainedEntitiesException(IContext context, IEntity[] entities)
            : base(string.Concat("'", context, "' detected retained entities although all entities got destroyed!"), "Did you release all entities? Try calling systems.DeactivateReactiveSystems()before calling context.DestroyAllEntities() to avoid memory leaks.Do not forget to activate them back when needed.\n" + string.Join("\n", entities.Select((IEntity e) => (e.aerc is SafeAERC safeAERC) ? string.Concat(e, " - ", string.Join(", ", safeAERC.owners.Select((object o) => o.ToString()).ToArray())) : e.ToString()).ToArray()))
        {
        }
    }
}

