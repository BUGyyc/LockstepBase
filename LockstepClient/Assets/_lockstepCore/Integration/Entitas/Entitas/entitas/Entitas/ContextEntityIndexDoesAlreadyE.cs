namespace Entitas
{
    public class ContextEntityIndexDoesAlreadyExistException : EntitasException
    {
        public ContextEntityIndexDoesAlreadyExistException(IContext context, string name)
            : base(string.Concat("Cannot add EntityIndex '", name, "' to context '", context, "'!"), "An EntityIndex with this name has already been added.")
        {
        }
    }
}

