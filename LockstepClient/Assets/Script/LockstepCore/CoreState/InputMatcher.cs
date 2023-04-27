using Entitas;

public sealed partial class InputMatcher
{

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
