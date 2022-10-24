using Entitas;

public sealed class SnapshotMatcher
{
	private static IMatcher<SnapshotEntity> _matcherTick;

	public static IMatcher<SnapshotEntity> Tick
	{
		get
		{
			if (_matcherTick == null)
			{
				Matcher<SnapshotEntity> val = (Matcher<SnapshotEntity>)(object)Matcher<SnapshotEntity>.AllOf(new int[1]);
				val.set_componentNames(SnapshotComponentsLookup.componentNames);
				_matcherTick = (IMatcher<SnapshotEntity>)(object)val;
			}
			return _matcherTick;
		}
	}

	public static IAllOfMatcher<SnapshotEntity> AllOf(params int[] indices)
	{
		return Matcher<SnapshotEntity>.AllOf(indices);
	}

	public static IAllOfMatcher<SnapshotEntity> AllOf(params IMatcher<SnapshotEntity>[] matchers)
	{
		return Matcher<SnapshotEntity>.AllOf(matchers);
	}

	public static IAnyOfMatcher<SnapshotEntity> AnyOf(params int[] indices)
	{
		return Matcher<SnapshotEntity>.AnyOf(indices);
	}

	public static IAnyOfMatcher<SnapshotEntity> AnyOf(params IMatcher<SnapshotEntity>[] matchers)
	{
		return Matcher<SnapshotEntity>.AnyOf(matchers);
	}
}
