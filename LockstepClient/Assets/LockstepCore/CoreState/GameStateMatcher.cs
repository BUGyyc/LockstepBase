using Entitas;

public sealed class GameStateMatcher
{
    private static IMatcher<GameStateEntity> _matcherHashCode;

    private static IMatcher<GameStateEntity> _matcherPaused;

    private static IMatcher<GameStateEntity> _matcherPredicting;

    private static IMatcher<GameStateEntity> _matcherTick;

    public static IMatcher<GameStateEntity> HashCode
    {
        get
        {
            if (_matcherHashCode == null)
            {
                Matcher<GameStateEntity> val = (Matcher<GameStateEntity>)(object)Matcher<GameStateEntity>.AllOf(new int[1]);
                val.componentNames = (GameStateComponentsLookup.componentNames);
                _matcherHashCode = (IMatcher<GameStateEntity>)(object)val;
            }
            return _matcherHashCode;
        }
    }

    public static IMatcher<GameStateEntity> Paused
    {
        get
        {
            if (_matcherPaused == null)
            {
                Matcher<GameStateEntity> val = (Matcher<GameStateEntity>)(object)Matcher<GameStateEntity>.AllOf(new int[1] { 1 });
                val.componentNames = (GameStateComponentsLookup.componentNames);
                _matcherPaused = (IMatcher<GameStateEntity>)(object)val;
            }
            return _matcherPaused;
        }
    }

    public static IMatcher<GameStateEntity> Predicting
    {
        get
        {
            if (_matcherPredicting == null)
            {
                Matcher<GameStateEntity> val = (Matcher<GameStateEntity>)(object)Matcher<GameStateEntity>.AllOf(new int[1] { 2 });
                val.componentNames = (GameStateComponentsLookup.componentNames);
                _matcherPredicting = (IMatcher<GameStateEntity>)(object)val;
            }
            return _matcherPredicting;
        }
    }

    public static IMatcher<GameStateEntity> Tick
    {
        get
        {
            if (_matcherTick == null)
            {
                Matcher<GameStateEntity> val = (Matcher<GameStateEntity>)(object)Matcher<GameStateEntity>.AllOf(new int[1] { 3 });
                val.componentNames = (GameStateComponentsLookup.componentNames);
                _matcherTick = (IMatcher<GameStateEntity>)(object)val;
            }
            return _matcherTick;
        }
    }

    public static IAllOfMatcher<GameStateEntity> AllOf(params int[] indices)
    {
        return Matcher<GameStateEntity>.AllOf(indices);
    }

    public static IAllOfMatcher<GameStateEntity> AllOf(params IMatcher<GameStateEntity>[] matchers)
    {
        return Matcher<GameStateEntity>.AllOf(matchers);
    }

    public static IAnyOfMatcher<GameStateEntity> AnyOf(params int[] indices)
    {
        return Matcher<GameStateEntity>.AnyOf(indices);
    }

    public static IAnyOfMatcher<GameStateEntity> AnyOf(params IMatcher<GameStateEntity>[] matchers)
    {
        return Matcher<GameStateEntity>.AnyOf(matchers);
    }
}
