using System;
using Entitas;
using Lockstep.Core.State.GameState;

public sealed partial class GameStateContext : Context<GameStateEntity>
{
    // public GameStateEntity hashCodeEntity => base.GetGroup(GameStateMatcher.HashCode).GetSingleEntity();

    // public HashCodeComponent hashCode => hashCodeEntity.hashCode;

    // public bool hasHashCode => hashCodeEntity != null;

    // public GameStateEntity pausedEntity => base.GetGroup(GameStateMatcher.Paused).GetSingleEntity();

    // public bool isPaused
    // {
    //     get
    //     {
    //         return pausedEntity != null;
    //     }
    //     set
    //     {
    //         GameStateEntity gameStateEntity = pausedEntity;
    //         if (value != (gameStateEntity != null))
    //         {
    //             if (value)
    //             {
    //                 base.CreateEntity().isPaused = true;
    //             }
    //             else
    //             {
    //                 ((Entity)gameStateEntity).Destroy();
    //             }
    //         }
    //     }
    // }

    // public GameStateEntity predictingEntity => base.GetGroup(GameStateMatcher.Predicting).GetSingleEntity();

    // public bool isPredicting
    // {
    //     get
    //     {
    //         return predictingEntity != null;
    //     }
    //     set
    //     {
    //         GameStateEntity gameStateEntity = predictingEntity;
    //         if (value != (gameStateEntity != null))
    //         {
    //             if (value)
    //             {
    //                 base.CreateEntity().isPredicting = true;
    //             }
    //             else
    //             {
    //                 ((Entity)gameStateEntity).Destroy();
    //             }
    //         }
    //     }
    // }

    // public GameStateEntity tickEntity => base.GetGroup(GameStateMatcher.Tick).GetSingleEntity();

    // public TickComponent tick => tickEntity.tick;

    // public bool hasTick => tickEntity != null;

    //     public GameStateEntity SetHashCode(long newValue)
    //     {
    //         //IL_0021: Unknown result type (might be due to invalid IL or missing references)
    //         if (hasHashCode)
    //         {
    //             throw new EntitasException(string.Concat("Could not set HashCode!\n", this, " already has an entity with Lockstep.Core.State.GameState.HashCodeComponent!"), "You should check if the context already has a hashCodeEntity before setting it or use context.ReplaceHashCode().");
    //         }
    //         GameStateEntity gameStateEntity = base.CreateEntity();
    //         gameStateEntity.AddHashCode(newValue);


    // #if UNITY_EDITOR

    //         if (hasTick) UnityEngine.Debug.Log($"Tick {tick.value} HashCode {newValue}");
    // #endif
    //         return gameStateEntity;
    //     }

    //     public void ReplaceHashCode(long newValue)
    //     {
    //         GameStateEntity gameStateEntity = hashCodeEntity;
    //         if (gameStateEntity == null)
    //         {
    //             gameStateEntity = SetHashCode(newValue);
    //         }
    //         else
    //         {
    //             gameStateEntity.ReplaceHashCode(newValue);
    //         }

    //     }

    //     public void RemoveHashCode()
    //     {
    //         ((Entity)hashCodeEntity).Destroy();
    //     }

    // public GameStateEntity SetTick(uint newValue)
    // {
    //     //IL_0021: Unknown result type (might be due to invalid IL or missing references)
    //     if (hasTick)
    //     {
    //         throw new EntitasException(string.Concat("Could not set Tick!\n", this, " already has an entity with Lockstep.Core.State.GameState.TickComponent!"), "You should check if the context already has a tickEntity before setting it or use context.ReplaceTick().");
    //     }
    //     GameStateEntity gameStateEntity = base.CreateEntity();
    //     gameStateEntity.AddTick(newValue);
    //     return gameStateEntity;
    // }

    // public void ReplaceTick(uint newValue)
    // {
    //     GameStateEntity gameStateEntity = tickEntity;
    //     if (gameStateEntity == null)
    //     {
    //         gameStateEntity = SetTick(newValue);
    //     }
    //     else
    //     {
    //         gameStateEntity.ReplaceTick(newValue);
    //     }
    // }

    // public void RemoveTick()
    // {
    //     ((Entity)tickEntity).Destroy();
    // }

    public GameStateContext()
        : base(4, 0, new ContextInfo("GameState", GameStateComponentsLookup.componentNames, GameStateComponentsLookup.componentTypes), (Func<IEntity, IAERC>)((IEntity entity) => (IAERC)new SafeAERC(entity)), (Func<GameStateEntity>)(() => new GameStateEntity()))
    {
    }//IL_0012: Unknown result type (might be due to invalid IL or missing references)
     //IL_005a: Expected O, but got Unknown

}
