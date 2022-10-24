using System;

namespace Entitas.CodeGeneration.Attributes
{
    [Obsolete("Use [ComponentName] instead", false)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
    public class CustomComponentNameAttribute : Attribute
    {
        public readonly string[] componentNames;

        public CustomComponentNameAttribute(params string[] componentNames)
        {
            this.componentNames = componentNames;
        }
    }
}


