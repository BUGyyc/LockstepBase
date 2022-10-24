using System;
using Lockstep.Core.State.Game;

public static class GameComponentsLookup
{
	public const int DestinationListener = 0;

	public const int ActorId = 1;

	public const int Asset = 2;

	public const int Backup = 3;

	public const int Controllable = 4;

	public const int Destination = 5;

	public const int Destroyed = 6;

	public const int Hashable = 7;

	public const int Health = 8;

	public const int Id = 9;

	public const int LocalId = 10;

	public const int MaxSpeed = 11;

	public const int Navigable = 12;

	public const int Position = 13;

	public const int Radius = 14;

	public const int RvoAgentSettings = 15;

	public const int Team = 16;

	public const int Velocity = 17;

	public const int PositionListener = 18;

	public const int TotalComponents = 19;

	public static readonly string[] componentNames = new string[19]
	{
		"DestinationListener", "ActorId", "Asset", "Backup", "Controllable", "Destination", "Destroyed", "Hashable", "Health", "Id",
		"LocalId", "MaxSpeed", "Navigable", "Position", "Radius", "RvoAgentSettings", "Team", "Velocity", "PositionListener"
	};

	public static readonly Type[] componentTypes = new Type[19]
	{
		typeof(DestinationListenerComponent),
		typeof(ActorIdComponent),
		typeof(AssetComponent),
		typeof(BackupComponent),
		typeof(ControllableComponent),
		typeof(DestinationComponent),
		typeof(DestroyedComponent),
		typeof(HashableComponent),
		typeof(HealthComponent),
		typeof(IdComponent),
		typeof(LocalIdComponent),
		typeof(MaxSpeedComponent),
		typeof(NavigableComponent),
		typeof(PositionComponent),
		typeof(RadiusComponent),
		typeof(RvoAgentSettingsComponent),
		typeof(TeamComponent),
		typeof(VelocityComponent),
		typeof(PositionListenerComponent)
	};
}
