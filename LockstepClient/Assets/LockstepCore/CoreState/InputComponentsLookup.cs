using System;
using Lockstep.Core.State.Input;

public static class InputComponentsLookup
{
    public const int ActorId = 0;

    public const int Coordinate = 1;

    public const int EntityConfigId = 2;

    public const int Selection = 3;

    public const int TargetActorId = 4;

    public const int Tick = 5;

    public const int CharacterInput = 6;

    public const int SkillInput = 7;

    public const int TotalComponents = 8;

    public static readonly string[] componentNames = new string[TotalComponents]
    {
        "ActorId",
        "Coordinate",
        "EntityConfigId",
        "Selection",
        "TargetActorId",

        "Tick",
        "CharacterInput",
        "SkillInput"
    };

    public static readonly Type[] componentTypes = new Type[TotalComponents]
    {
        typeof(ActorIdComponent),
        typeof(CoordinateComponent),
        typeof(EntityConfigIdComponent),
        typeof(SelectionComponent),
        typeof(TargetActorIdComponent),

        typeof(TickComponent),
        typeof(CharacterInputComponent),
        typeof(SkillInputComponent)
    };
}
