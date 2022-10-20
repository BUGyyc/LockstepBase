using Entitas;

public sealed class DebugMatcher
{
	private static IMatcher<DebugEntity> _matcherHashCode;

	private static IMatcher<DebugEntity> _matcherTick;

	public static IMatcher<DebugEntity> HashCode
	{
		get
		{
			if (_matcherHashCode == null)
			{
				Matcher<DebugEntity> val = (Matcher<DebugEntity>)(object)Matcher<DebugEntity>.AllOf(new int[1]);
				val.set_componentNames(DebugComponentsLookup.componentNames);
				_matcherHashCode = (IMatcher<DebugEntity>)(object)val;
			}
			return _matcherHashCode;
		}
	}

	public static IMatcher<DebugEntity> Tick
	{
		get
		{
			if (_matcherTick == null)
			{
				Matcher<DebugEntity> val = (Matcher<DebugEntity>)(object)Matcher<DebugEntity>.AllOf(new int[1] { 1 });
				val.set_componentNames(DebugComponentsLookup.componentNames);
				_matcherTick = (IMatcher<DebugEntity>)(object)val;
			}
			return _matcherTick;
		}
	}

	public static IAllOfMatcher<DebugEntity> AllOf(params int[] indices)
	{
		return Matcher<DebugEntity>.AllOf(indices);
	}

	public static IAllOfMatcher<DebugEntity> AllOf(params IMatcher<DebugEntity>[] matchers)
	{
		return Matcher<DebugEntity>.AllOf(matchers);
	}

	public static IAnyOfMatcher<DebugEntity> AnyOf(params int[] indices)
	{
		return Matcher<DebugEntity>.AnyOf(indices);
	}

	public static IAnyOfMatcher<DebugEntity> AnyOf(params IMatcher<DebugEntity>[] matchers)
	{
		return Matcher<DebugEntity>.AnyOf(matchers);
	}
}
