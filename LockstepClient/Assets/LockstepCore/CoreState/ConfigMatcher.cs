using Entitas;

public sealed class ConfigMatcher
{
	public static IAllOfMatcher<ConfigEntity> AllOf(params int[] indices)
	{
		return Matcher<ConfigEntity>.AllOf(indices);
	}

	public static IAllOfMatcher<ConfigEntity> AllOf(params IMatcher<ConfigEntity>[] matchers)
	{
		return Matcher<ConfigEntity>.AllOf(matchers);
	}

	public static IAnyOfMatcher<ConfigEntity> AnyOf(params int[] indices)
	{
		return Matcher<ConfigEntity>.AnyOf(indices);
	}

	public static IAnyOfMatcher<ConfigEntity> AnyOf(params IMatcher<ConfigEntity>[] matchers)
	{
		return Matcher<ConfigEntity>.AnyOf(matchers);
	}
}
