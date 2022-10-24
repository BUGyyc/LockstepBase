using System;

namespace Entitas.CodeGeneration.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
    public class FlagPrefixAttribute : Attribute
    {
        public readonly string prefix;

        public FlagPrefixAttribute(string prefix)
        {
            this.prefix = prefix;
        }
    }

}


