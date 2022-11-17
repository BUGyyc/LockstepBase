//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public Lockstep.Core.State.Input.SelectionComponent selection { get { return (Lockstep.Core.State.Input.SelectionComponent)GetComponent(InputComponentsLookup.Selection); } }
    public bool hasSelection { get { return HasComponent(InputComponentsLookup.Selection); } }

    public void AddSelection(uint[] newEntityIds) {
        var index = InputComponentsLookup.Selection;
        var component = (Lockstep.Core.State.Input.SelectionComponent)CreateComponent(index, typeof(Lockstep.Core.State.Input.SelectionComponent));
        component.entityIds = newEntityIds;
        AddComponent(index, component);
    }

    public void ReplaceSelection(uint[] newEntityIds) {
        var index = InputComponentsLookup.Selection;
        var component = (Lockstep.Core.State.Input.SelectionComponent)CreateComponent(index, typeof(Lockstep.Core.State.Input.SelectionComponent));
        component.entityIds = newEntityIds;
        ReplaceComponent(index, component);
    }

    public void RemoveSelection() {
        RemoveComponent(InputComponentsLookup.Selection);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherSelection;

    public static Entitas.IMatcher<InputEntity> Selection {
        get {
            if (_matcherSelection == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.Selection);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherSelection = matcher;
            }

            return _matcherSelection;
        }
    }
}
