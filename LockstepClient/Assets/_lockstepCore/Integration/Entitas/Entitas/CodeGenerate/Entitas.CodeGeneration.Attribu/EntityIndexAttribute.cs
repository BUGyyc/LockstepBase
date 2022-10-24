using System;

namespace Entitas.CodeGeneration.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    public class EntityIndexAttribute : AbstractEntityIndexAttribute
    {
        public EntityIndexAttribute()
            : base(EntityIndexType.EntityIndex)
        {
        }
    }
}


