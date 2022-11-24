using Entitas;
using Lockstep;

public class CharacterComponent : IComponent
{
    public LVector3 speed;
}

// public sealed partial class GameMatcher
// {
//     private static IMatcher<GameEntity> _character;
//     public static IMatcher<GameEntity> Character
//     {
//         get
//         {
//             if (_character == null)
//             {
//                 Matcher<GameEntity> val = Matcher<GameEntity>.AllOf(new int[1] { GameComponentsLookup.Character }) as Matcher<GameEntity>;
//                 val.componentNames = GameComponentsLookup.componentNames;
//                 _character = val;
//             }
//             return _character;
//         }

//     }
// }
