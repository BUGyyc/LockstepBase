using Entitas;

public sealed class ActorMatcher
{
    private static IMatcher<ActorEntity> _matcherBackup;

    private static IMatcher<ActorEntity> _matcherEntityCount;

    private static IMatcher<ActorEntity> _matcherId;

    public static IMatcher<ActorEntity> Backup
    {
        get
        {
            if (_matcherBackup == null)
            {
                Matcher<ActorEntity> val = (Matcher<ActorEntity>)(object)Matcher<ActorEntity>.AllOf(new int[1]);
                //val.set_componentNames(ActorComponentsLookup.componentNames);
                val.componentNames = ActorComponentsLookup.componentNames;
                _matcherBackup = (IMatcher<ActorEntity>)(object)val;
            }
            return _matcherBackup;
        }
    }

    public static IMatcher<ActorEntity> EntityCount
    {
        get
        {
            if (_matcherEntityCount == null)
            {
                Matcher<ActorEntity> val = (Matcher<ActorEntity>)(object)Matcher<ActorEntity>.AllOf(new int[1] { 1 });
                //val.set_componentNames(ActorComponentsLookup.componentNames);
                val.componentNames = ActorComponentsLookup.componentNames;
                _matcherEntityCount = (IMatcher<ActorEntity>)(object)val;
            }
            return _matcherEntityCount;
        }
    }

    public static IMatcher<ActorEntity> Id
    {
        get
        {
            if (_matcherId == null)
            {
                Matcher<ActorEntity> val = (Matcher<ActorEntity>)(object)Matcher<ActorEntity>.AllOf(new int[1] { 2 });
                val.componentNames = (ActorComponentsLookup.componentNames);
                _matcherId = (IMatcher<ActorEntity>)(object)val;
            }
            return _matcherId;
        }
    }

    public static IAllOfMatcher<ActorEntity> AllOf(params int[] indices)
    {
        return Matcher<ActorEntity>.AllOf(indices);
    }

    public static IAllOfMatcher<ActorEntity> AllOf(params IMatcher<ActorEntity>[] matchers)
    {
        return Matcher<ActorEntity>.AllOf(matchers);
    }

    public static IAnyOfMatcher<ActorEntity> AnyOf(params int[] indices)
    {
        return Matcher<ActorEntity>.AnyOf(indices);
    }

    public static IAnyOfMatcher<ActorEntity> AnyOf(params IMatcher<ActorEntity>[] matchers)
    {
        return Matcher<ActorEntity>.AnyOf(matchers);
    }
}
