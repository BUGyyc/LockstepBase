using System;
using Entitas;
using Lockstep.Core.State.GameState;

public sealed partial class GameStateContext : Context<GameStateEntity>
{
    public GameStateContext()
        : base(GameStateComponentsLookup.TotalComponents, 0, new ContextInfo("GameState", GameStateComponentsLookup.componentNames, GameStateComponentsLookup.componentTypes), (Func<IEntity, IAERC>)((IEntity entity) => (IAERC)new SafeAERC(entity)), (Func<GameStateEntity>)(() => new GameStateEntity()))
    {
    }//IL_0012: Unknown result type (might be due to invalid IL or missing references)
     //IL_005a: Expected O, but got Unknown

}
