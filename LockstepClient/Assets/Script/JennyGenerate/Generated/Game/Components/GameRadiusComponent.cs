//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Lockstep.Core.State.Game.RadiusComponent radiusComponent = new Lockstep.Core.State.Game.RadiusComponent();

    public bool isRadius {
        get { return HasComponent(GameComponentsLookup.Radius); }
        set {
            if (value != isRadius) {
                var index = GameComponentsLookup.Radius;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : radiusComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherRadius;

    public static Entitas.IMatcher<GameEntity> Radius {
        get {
            if (_matcherRadius == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Radius);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRadius = matcher;
            }

            return _matcherRadius;
        }
    }
}