//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class GameStateComponentsLookup {

    public const int HashCode = 0;
    public const int Paused = 1;
    public const int Predicting = 2;
    public const int Tick = 3;

    public const int TotalComponents = 4;

    public static readonly string[] componentNames = {
        "HashCode",
        "Paused",
        "Predicting",
        "Tick"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(Lockstep.Core.State.GameState.HashCodeComponent),
        typeof(Lockstep.Core.State.GameState.PausedComponent),
        typeof(Lockstep.Core.State.GameState.PredictingComponent),
        typeof(Lockstep.Core.State.GameState.TickComponent)
    };
}
