using Entitas;

public sealed partial class GameMatcher
{
    //     private static IMatcher<GameEntity> _matcherRadius;

    //     private static IMatcher<GameEntity> _matcherRvoAgentSettings;

    //     private static IMatcher<GameEntity> _matcherDestroyed;

    //     private static IMatcher<GameEntity> _matcherActorId;

    //     private static IMatcher<GameEntity> _matcherAsset;

    //     private static IMatcher<GameEntity> _matcherBackup;

    //     private static IMatcher<GameEntity> _matcherControllable;

    //     private static IMatcher<GameEntity> _matcherDestination;

    //     private static IMatcher<GameEntity> _matcherHashable;

    //     private static IMatcher<GameEntity> _matcherHealth;

    //     private static IMatcher<GameEntity> _matcherId;

    //     private static IMatcher<GameEntity> _matcherLocalId;

    //     private static IMatcher<GameEntity> _matcherMaxSpeed;

    //     private static IMatcher<GameEntity> _matcherNavigable;

    //     private static IMatcher<GameEntity> _matcherPosition;

    //     private static IMatcher<GameEntity> _matcherTeam;

    //     private static IMatcher<GameEntity> _matcherVelocity;

    //     private static IMatcher<GameEntity> _matcherDestinationListener;

    //     private static IMatcher<GameEntity> _matcherPositionListener;

    //     public static IMatcher<GameEntity> Radius
    //     {
    //         get
    //         {
    //             if (_matcherRadius == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 14 });
    //                 //val.set_componentNames(GameComponentsLookup.componentNames);
    //                 val.componentNames = GameComponentsLookup.componentNames;
    //                 _matcherRadius = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherRadius;
    //         }
    //     }

    //     public static IMatcher<GameEntity> RvoAgentSettings
    //     {
    //         get
    //         {
    //             if (_matcherRvoAgentSettings == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 15 });
    //                 //val.set_componentNames(GameComponentsLookup.componentNames);
    //                 val.componentNames = GameComponentsLookup.componentNames;
    //                 _matcherRvoAgentSettings = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherRvoAgentSettings;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Destroyed
    //     {
    //         get
    //         {
    //             if (_matcherDestroyed == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 6 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherDestroyed = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherDestroyed;
    //         }
    //     }

    //     public static IMatcher<GameEntity> ActorId
    //     {
    //         get
    //         {
    //             if (_matcherActorId == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 1 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherActorId = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherActorId;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Asset
    //     {
    //         get
    //         {
    //             if (_matcherAsset == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 2 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherAsset = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherAsset;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Backup
    //     {
    //         get
    //         {
    //             if (_matcherBackup == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 3 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherBackup = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherBackup;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Controllable
    //     {
    //         get
    //         {
    //             if (_matcherControllable == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 4 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherControllable = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherControllable;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Destination
    //     {
    //         get
    //         {
    //             if (_matcherDestination == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 5 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherDestination = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherDestination;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Hashable
    //     {
    //         get
    //         {
    //             if (_matcherHashable == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 7 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherHashable = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherHashable;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Health
    //     {
    //         get
    //         {
    //             if (_matcherHealth == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 8 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherHealth = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherHealth;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Id
    //     {
    //         get
    //         {
    //             if (_matcherId == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 9 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherId = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherId;
    //         }
    //     }

    //     public static IMatcher<GameEntity> LocalId
    //     {
    //         get
    //         {
    //             if (_matcherLocalId == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 10 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherLocalId = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherLocalId;
    //         }
    //     }

    //     public static IMatcher<GameEntity> MaxSpeed
    //     {
    //         get
    //         {
    //             if (_matcherMaxSpeed == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 11 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherMaxSpeed = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherMaxSpeed;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Navigable
    //     {
    //         get
    //         {
    //             if (_matcherNavigable == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 12 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherNavigable = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherNavigable;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Position
    //     {
    //         get
    //         {
    //             if (_matcherPosition == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 13 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherPosition = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherPosition;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Team
    //     {
    //         get
    //         {
    //             if (_matcherTeam == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 16 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherTeam = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherTeam;
    //         }
    //     }

    //     public static IMatcher<GameEntity> Velocity
    //     {
    //         get
    //         {
    //             if (_matcherVelocity == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 17 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherVelocity = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherVelocity;
    //         }
    //     }

    //     public static IMatcher<GameEntity> DestinationListener
    //     {
    //         get
    //         {
    //             if (_matcherDestinationListener == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1]);
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherDestinationListener = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherDestinationListener;
    //         }
    //     }

    //     public static IMatcher<GameEntity> PositionListener
    //     {
    //         get
    //         {
    //             if (_matcherPositionListener == null)
    //             {
    //                 Matcher<GameEntity> val = (Matcher<GameEntity>)(object)Matcher<GameEntity>.AllOf(new int[1] { 18 });
    //                 val.componentNames = (GameComponentsLookup.componentNames);
    //                 _matcherPositionListener = (IMatcher<GameEntity>)(object)val;
    //             }
    //             return _matcherPositionListener;
    //         }
    //     }

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
