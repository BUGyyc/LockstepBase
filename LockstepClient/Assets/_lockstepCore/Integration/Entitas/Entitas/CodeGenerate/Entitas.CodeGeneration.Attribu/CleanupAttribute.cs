using System;

namespace Entitas.CodeGeneration.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
    public class CleanupAttribute : Attribute
    {
        public readonly CleanupMode cleanupMode;

        public CleanupAttribute(CleanupMode cleanupMode)
        {
            this.cleanupMode = cleanupMode;
        }
    }
}


