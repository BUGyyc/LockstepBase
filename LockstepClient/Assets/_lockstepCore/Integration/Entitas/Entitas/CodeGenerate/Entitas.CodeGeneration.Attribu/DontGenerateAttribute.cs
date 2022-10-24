using System;

namespace Entitas.CodeGeneration.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
    public class DontGenerateAttribute : Attribute
    {
        public readonly bool generateIndex;

        public DontGenerateAttribute(bool generateIndex = true)
        {
            this.generateIndex = generateIndex;
        }
    }
}

