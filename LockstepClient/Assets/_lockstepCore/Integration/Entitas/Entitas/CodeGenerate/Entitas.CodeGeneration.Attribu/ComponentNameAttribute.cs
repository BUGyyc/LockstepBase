using System;

namespace Entitas.CodeGeneration.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
    public class ComponentNameAttribute : Attribute
    {
        public readonly string[] componentNames;

        public ComponentNameAttribute(params string[] componentNames)
        {
            this.componentNames = componentNames;
        }
    }
}


