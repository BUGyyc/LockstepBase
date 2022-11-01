using Entitas;

public class CharacterUtil
{
    //public static void Init

    public CharacterComponent AddCharacterComponent(GameEntity entity)
    {
        CharacterComponent character = (CharacterComponent)entity.CreateComponent(GameComponentsLookup.Character, typeof(CharacterComponent));
        return character;
    }
}

