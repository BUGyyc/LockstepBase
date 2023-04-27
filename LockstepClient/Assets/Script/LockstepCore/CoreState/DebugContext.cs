using System;
using Entitas;

public sealed class DebugContext : Context<DebugEntity>
{
	public DebugContext()
		: base(DebugComponentsLookup.TotalComponents, 0, new ContextInfo("Debug", DebugComponentsLookup.componentNames, DebugComponentsLookup.componentTypes), (Func<IEntity, IAERC>)((IEntity entity) => (IAERC)new SafeAERC(entity)), (Func<DebugEntity>)(() => new DebugEntity()))
	{
	}//IL_0012: Unknown result type (might be due to invalid IL or missing references)
	//IL_005a: Expected O, but got Unknown

}
