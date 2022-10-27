using Entitas;

public sealed class InputMatcher
{
    private static IMatcher<InputEntity> _matcherActorId;

    private static IMatcher<InputEntity> _matcherCoordinate;

    private static IMatcher<InputEntity> _matcherEntityConfigId;

    private static IMatcher<InputEntity> _matcherSelection;

    private static IMatcher<InputEntity> _matcherTargetActorId;

    private static IMatcher<InputEntity> _matcherTick;

    public static IMatcher<InputEntity> ActorId
    {
        get
        {
            if (_matcherActorId == null)
            {
                Matcher<InputEntity> val = (Matcher<InputEntity>)(object)Matcher<InputEntity>.AllOf(new int[1]);
                val.componentNames = (InputComponentsLookup.componentNames);
                _matcherActorId = (IMatcher<InputEntity>)(object)val;
            }
            return _matcherActorId;
        }
    }

    public static IMatcher<InputEntity> Coordinate
    {
        get
        {
            if (_matcherCoordinate == null)
            {
                Matcher<InputEntity> val = (Matcher<InputEntity>)(object)Matcher<InputEntity>.AllOf(new int[1] { 1 });
                val.componentNames = (InputComponentsLookup.componentNames);
                _matcherCoordinate = (IMatcher<InputEntity>)(object)val;
            }
            return _matcherCoordinate;
        }
    }

    public static IMatcher<InputEntity> EntityConfigId
    {
        get
        {
            if (_matcherEntityConfigId == null)
            {
                Matcher<InputEntity> val = (Matcher<InputEntity>)(object)Matcher<InputEntity>.AllOf(new int[1] { 2 });
                val.componentNames = (InputComponentsLookup.componentNames);
                _matcherEntityConfigId = (IMatcher<InputEntity>)(object)val;
            }
            return _matcherEntityConfigId;
        }
    }

    public static IMatcher<InputEntity> Selection
    {
        get
        {
            if (_matcherSelection == null)
            {
                Matcher<InputEntity> val = (Matcher<InputEntity>)(object)Matcher<InputEntity>.AllOf(new int[1] { 3 });
                val.componentNames = (InputComponentsLookup.componentNames);
                _matcherSelection = (IMatcher<InputEntity>)(object)val;
            }
            return _matcherSelection;
        }
    }

    public static IMatcher<InputEntity> TargetActorId
    {
        get
        {
            if (_matcherTargetActorId == null)
            {
                Matcher<InputEntity> val = (Matcher<InputEntity>)(object)Matcher<InputEntity>.AllOf(new int[1] { 4 });
                val.componentNames = (InputComponentsLookup.componentNames);
                _matcherTargetActorId = (IMatcher<InputEntity>)(object)val;
            }
            return _matcherTargetActorId;
        }
    }

    public static IMatcher<InputEntity> Tick
    {
        get
        {
            if (_matcherTick == null)
            {
                Matcher<InputEntity> val = (Matcher<InputEntity>)(object)Matcher<InputEntity>.AllOf(new int[1] { 5 });
                val.componentNames = (InputComponentsLookup.componentNames);
                _matcherTick = (IMatcher<InputEntity>)(object)val;
            }
            return _matcherTick;
        }
    }

    public static IAllOfMatcher<InputEntity> AllOf(params int[] indices)
    {
        return Matcher<InputEntity>.AllOf(indices);
    }

    public static IAllOfMatcher<InputEntity> AllOf(params IMatcher<InputEntity>[] matchers)
    {
        return Matcher<InputEntity>.AllOf(matchers);
    }

    public static IAnyOfMatcher<InputEntity> AnyOf(params int[] indices)
    {
        return Matcher<InputEntity>.AnyOf(indices);
    }

    public static IAnyOfMatcher<InputEntity> AnyOf(params IMatcher<InputEntity>[] matchers)
    {
        return Matcher<InputEntity>.AnyOf(matchers);
    }
}
