using System;

namespace Entitas.CodeGeneration.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class PrimaryEntityIndexAttribute : AbstractEntityIndexAttribute
    {
        public PrimaryEntityIndexAttribute()
            : base(EntityIndexType.PrimaryEntityIndex)
        {
        }
    }
}


