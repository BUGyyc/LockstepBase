using Entitas;

public class CharacterUtil
{
    //public static void Init

    public CharacterComponent AddCharacterComponent(GameEntity entity)
    {
        CharacterComponent character = (CharacterComponent)entity.CreateComponent(GameComponentsLookup.CharacterComponet, typeof(CharacterComponent));
        return character;
    }
}

