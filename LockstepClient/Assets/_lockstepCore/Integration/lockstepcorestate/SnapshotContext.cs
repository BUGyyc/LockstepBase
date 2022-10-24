using System;
using Entitas;

public sealed class SnapshotContext : Context<SnapshotEntity>
{
	public SnapshotContext()
		: base(1, 0, new ContextInfo("Snapshot", SnapshotComponentsLookup.componentNames, SnapshotComponentsLookup.componentTypes), (Func<IEntity, IAERC>)((IEntity entity) => (IAERC)new SafeAERC(entity)), (Func<SnapshotEntity>)(() => new SnapshotEntity()))
	{
	}//IL_0012: Unknown result type (might be due to invalid IL or missing references)
	//IL_005a: Expected O, but got Unknown

}
