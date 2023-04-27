using System;
using Entitas;

public sealed class GameContext : Context<GameEntity>
{
    public GameContext()
        : base(GameComponentsLookup.TotalComponents, 0, new ContextInfo("Game", GameComponentsLookup.componentNames, GameComponentsLookup.componentTypes), (IEntity entity) => new SafeAERC(entity), (() => new GameEntity()))
    {
    }//IL_0013: Unknown result type (might be due to invalid IL or missing references)
     //IL_005b: Expected O, but got Unknown

}
