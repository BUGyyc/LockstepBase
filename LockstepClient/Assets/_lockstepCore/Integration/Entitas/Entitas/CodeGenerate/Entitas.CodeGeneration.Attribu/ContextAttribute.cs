using System;

namespace Entitas.CodeGeneration.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, AllowMultiple = true)]
    public class ContextAttribute : Attribute
    {
        public readonly string contextName;

        public ContextAttribute(string contextName)
        {
            this.contextName = contextName;
        }
    }
}
