using System;
using Lockstep.Core.State.Snapshot;

public static class SnapshotComponentsLookup
{
	public const int Tick = 0;

	public const int TotalComponents = 1;

	public static readonly string[] componentNames = new string[1] { "Tick" };

	public static readonly Type[] componentTypes = new Type[1] { typeof(TickComponent) };
}
