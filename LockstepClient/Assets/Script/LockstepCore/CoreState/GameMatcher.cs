using Entitas;

public sealed partial class GameMatcher
{
    public static IAllOfMatcher<GameEntity> AllOf(params int[] indices)
    {
        return Matcher<GameEntity>.AllOf(indices);
    }

    public static IAllOfMatcher<GameEntity> AllOf(params IMatcher<GameEntity>[] matchers)
    {
        return Matcher<GameEntity>.AllOf(matchers);
    }

    public static IAnyOfMatcher<GameEntity> AnyOf(params int[] indices)
    {
        return Matcher<GameEntity>.AnyOf(indices);
    }

    public static IAnyOfMatcher<GameEntity> AnyOf(params IMatcher<GameEntity>[] matchers)
    {
        return Matcher<GameEntity>.AnyOf(matchers);
    }
}
