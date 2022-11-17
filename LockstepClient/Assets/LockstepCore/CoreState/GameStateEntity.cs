using System.Collections.Generic;
using Entitas;
using Lockstep.Core.State.GameState;


/// <summary>
/// 游戏状态相关 Entity
/// </summary>
public sealed partial class GameStateEntity : Entity
{
    // private static readonly PausedComponent pausedComponent = new PausedComponent();

    // private static readonly PredictingComponent predictingComponent = new PredictingComponent();

    // public HashCodeComponent hashCode => (HashCodeComponent)GetComponent(0);

    // public bool hasHashCode => HasComponent(0);

    // public bool isPaused
    // {
    //     get
    //     {
    //         return HasComponent(1);
    //     }
    //     set
    //     {
    //         if (value == isPaused)
    //         {
    //             return;
    //         }
    //         int num = 1;
    //         if (value)
    //         {
    //             Stack<IComponent> componentPool = GetComponentPool(num);
    //             IComponent obj;
    //             if (componentPool.Count <= 0)
    //             {
    //                 IComponent val = (IComponent)(object)pausedComponent;
    //                 obj = val;
    //             }
    //             else
    //             {
    //                 obj = componentPool.Pop();
    //             }
    //             IComponent val2 = obj;
    //             ((Entity)this).AddComponent(num, val2);
    //         }
    //         else
    //         {
    //             ((Entity)this).RemoveComponent(num);
    //         }
    //     }
    // }

    // public bool isPredicting
    // {
    //     get
    //     {
    //         return ((Entity)this).HasComponent(2);
    //     }
    //     set
    //     {
    //         if (value == isPredicting)
    //         {
    //             return;
    //         }
    //         int num = 2;
    //         if (value)
    //         {
    //             Stack<IComponent> componentPool = ((Entity)this).GetComponentPool(num);
    //             IComponent obj;
    //             if (componentPool.Count <= 0)
    //             {
    //                 IComponent val = (IComponent)(object)predictingComponent;
    //                 obj = val;
    //             }
    //             else
    //             {
    //                 obj = componentPool.Pop();
    //             }
    //             IComponent val2 = obj;
    //             ((Entity)this).AddComponent(num, val2);
    //         }
    //         else
    //         {
    //             ((Entity)this).RemoveComponent(num);
    //         }
    //     }
    // }

    // public TickComponent tick => (TickComponent)(object)((Entity)this).GetComponent(3);

    // public bool hasTick => ((Entity)this).HasComponent(3);

    // public void AddHashCode(long newValue)
    // {
    //     int num = 0;
    //     HashCodeComponent hashCodeComponent = (HashCodeComponent)(object)((Entity)this).CreateComponent(num, typeof(HashCodeComponent));
    //     hashCodeComponent.value = newValue;
    //     ((Entity)this).AddComponent(num, (IComponent)(object)hashCodeComponent);
    // }

    // public void ReplaceHashCode(long newValue)
    // {
    //     int num = 0;
    //     HashCodeComponent hashCodeComponent = (HashCodeComponent)(object)((Entity)this).CreateComponent(num, typeof(HashCodeComponent));
    //     hashCodeComponent.value = newValue;
    //     ((Entity)this).ReplaceComponent(num, (IComponent)(object)hashCodeComponent);
    // }

    // public void RemoveHashCode()
    // {
    //     ((Entity)this).RemoveComponent(0);
    // }

    // public void AddTick(uint newValue)
    // {
    //     int num = 3;
    //     TickComponent tickComponent = (TickComponent)(object)((Entity)this).CreateComponent(num, typeof(TickComponent));
    //     tickComponent.value = newValue;
    //     ((Entity)this).AddComponent(num, (IComponent)(object)tickComponent);
    // }

    // public void ReplaceTick(uint newValue)
    // {
    //     int num = 3;
    //     TickComponent tickComponent = (TickComponent)(object)((Entity)this).CreateComponent(num, typeof(TickComponent));
    //     tickComponent.value = newValue;
    //     ((Entity)this).ReplaceComponent(num, (IComponent)(object)tickComponent);
    // }

    // public void RemoveTick()
    // {
    //     ((Entity)this).RemoveComponent(3);
    // }
}
