//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public AnimationComponent animation { get { return (AnimationComponent)GetComponent(GameComponentsLookup.Animation); } }
    public bool hasAnimation { get { return HasComponent(GameComponentsLookup.Animation); } }

    public void AddAnimation(uint newFrameID, uint newAnimationHashID, uint newPlaySinceStartFrameKey, uint newFrameLength, bool newReadyPlay, Lockstep.LVector3 newInputParams, Lockstep.LVector2 newOriginInput, AnimationMainState newState, string newAnimationName) {
        var index = GameComponentsLookup.Animation;
        var component = (AnimationComponent)CreateComponent(index, typeof(AnimationComponent));
        component.FrameID = newFrameID;
        component.AnimationHashID = newAnimationHashID;
        component.PlaySinceStartFrameKey = newPlaySinceStartFrameKey;
        component.FrameLength = newFrameLength;
        component.readyPlay = newReadyPlay;
        component.inputParams = newInputParams;
        component.originInput = newOriginInput;
        component.state = newState;
        component.animationName = newAnimationName;
        AddComponent(index, component);
    }

    public void ReplaceAnimation(uint newFrameID, uint newAnimationHashID, uint newPlaySinceStartFrameKey, uint newFrameLength, bool newReadyPlay, Lockstep.LVector3 newInputParams, Lockstep.LVector2 newOriginInput, AnimationMainState newState, string newAnimationName) {
        var index = GameComponentsLookup.Animation;
        var component = (AnimationComponent)CreateComponent(index, typeof(AnimationComponent));
        component.FrameID = newFrameID;
        component.AnimationHashID = newAnimationHashID;
        component.PlaySinceStartFrameKey = newPlaySinceStartFrameKey;
        component.FrameLength = newFrameLength;
        component.readyPlay = newReadyPlay;
        component.inputParams = newInputParams;
        component.originInput = newOriginInput;
        component.state = newState;
        component.animationName = newAnimationName;
        ReplaceComponent(index, component);
    }

    public void RemoveAnimation() {
        RemoveComponent(GameComponentsLookup.Animation);
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

    static Entitas.IMatcher<GameEntity> _matcherAnimation;

    public static Entitas.IMatcher<GameEntity> Animation {
        get {
            if (_matcherAnimation == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Animation);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAnimation = matcher;
            }

            return _matcherAnimation;
        }
    }
}