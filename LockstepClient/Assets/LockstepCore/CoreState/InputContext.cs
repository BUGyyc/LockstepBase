using System;
using Entitas;

public sealed class InputContext : Context<InputEntity>
{
    public InputContext()
        : base(InputComponentsLookup.TotalComponents, 0, new ContextInfo("Input", InputComponentsLookup.componentNames, InputComponentsLookup.componentTypes), (Func<IEntity, IAERC>)((IEntity entity) => (IAERC)new SafeAERC(entity)), (Func<InputEntity>)(() => new InputEntity()))
    {
    }//IL_0012: Unknown result type (might be due to invalid IL or missing references)
     //IL_005a: Expected O, but got Unknown

}
