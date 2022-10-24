﻿namespace Entitas
{
    public class ContextEntityIndexDoesNotExistException : EntitasException
    {
        public ContextEntityIndexDoesNotExistException(IContext context, string name)
            : base(string.Concat("Cannot get EntityIndex '", name, "' from context '", context, "'!"), "No EntityIndex with this name has been added.")
        {
        }
    }
}


