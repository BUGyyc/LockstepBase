using System;
using Lockstep.Core.State.Actor;

public static class ActorComponentsLookup
{
	public const int Backup = 0;

	public const int EntityCount = 1;

	public const int Id = 2;

	public const int TotalComponents = 3;

	public static readonly string[] componentNames = new string[3] { "Backup", "EntityCount", "Id" };

	public static readonly Type[] componentTypes = new Type[3]
	{
		typeof(BackupComponent),
		typeof(EntityCountComponent),
		typeof(IdComponent)
	};
}
