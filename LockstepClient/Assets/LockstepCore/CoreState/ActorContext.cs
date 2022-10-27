using System;
using Entitas;

public sealed class ActorContext : Context<ActorEntity>
{
    public ActorContext()
        : base(3, 0, new ContextInfo("Actor", ActorComponentsLookup.componentNames, ActorComponentsLookup.componentTypes), ((IEntity entity) => new SafeAERC(entity)), (() => new ActorEntity()))
    {
    }//IL_0012: Unknown result type (might be due to invalid IL or missing references)
     //IL_005a: Expected O, but got Unknown

}
