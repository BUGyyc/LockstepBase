using System;
using Lockstep.Core.State.Debug;

public static class DebugComponentsLookup
{
	public const int HashCode = 0;

	public const int Tick = 1;

	public const int TotalComponents = 2;

	public static readonly string[] componentNames = new string[2] { "HashCode", "Tick" };

	public static readonly Type[] componentTypes = new Type[2]
	{
		typeof(HashCodeComponent),
		typeof(TickComponent)
	};
}
