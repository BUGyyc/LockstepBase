//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TestComponent test { get { return (TestComponent)GetComponent(GameComponentsLookup.Test); } }
    public bool hasTest { get { return HasComponent(GameComponentsLookup.Test); } }

    public void AddTest(uint newTestID) {
        var index = GameComponentsLookup.Test;
        var component = (TestComponent)CreateComponent(index, typeof(TestComponent));
        component.testID = newTestID;
        AddComponent(index, component);
    }

    public void ReplaceTest(uint newTestID) {
        var index = GameComponentsLookup.Test;
        var component = (TestComponent)CreateComponent(index, typeof(TestComponent));
        component.testID = newTestID;
        ReplaceComponent(index, component);
    }

    public void RemoveTest() {
        RemoveComponent(GameComponentsLookup.Test);
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

    static Entitas.IMatcher<GameEntity> _matcherTest;

    public static Entitas.IMatcher<GameEntity> Test {
        get {
            if (_matcherTest == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Test);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTest = matcher;
            }

            return _matcherTest;
        }
    }
}
