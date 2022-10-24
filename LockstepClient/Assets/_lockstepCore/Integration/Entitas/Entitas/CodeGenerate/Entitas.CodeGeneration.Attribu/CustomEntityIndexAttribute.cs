using System;

namespace Entitas.CodeGeneration.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class CustomEntityIndexAttribute : Attribute
    {
        public readonly Type contextType;

        public CustomEntityIndexAttribute(Type contextType)
        {
            this.contextType = contextType;
        }
    }
}