//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Lockstep.Core.State.Game.MaxSpeedComponent maxSpeed { get { return (Lockstep.Core.State.Game.MaxSpeedComponent)GetComponent(GameComponentsLookup.MaxSpeed); } }
    public bool hasMaxSpeed { get { return HasComponent(GameComponentsLookup.MaxSpeed); } }

 

    public void RemoveMaxSpeed() {
        RemoveComponent(GameComponentsLookup.MaxSpeed);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherMaxSpeed;

    public static Entitas.IMatcher<GameEntity> MaxSpeed {
        get {
            if (_matcherMaxSpeed == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MaxSpeed);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMaxSpeed = matcher;
            }

            return _matcherMaxSpeed;
        }
    }
}
