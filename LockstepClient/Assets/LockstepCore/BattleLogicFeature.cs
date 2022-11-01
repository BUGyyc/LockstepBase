//using Lockstep.Game.Features.Cleanup;
using Entitas;
using UnityEngine;
namespace Lockstep.Game.Features
{
    internal sealed class BattleLogicFeature : Feature
    {
        public BattleLogicFeature(Contexts contexts, ServiceContainer services)
            : base("BattleLogic")
        {
            Debug.LogFormat("初始化 System");
            //这里的顺序很关键， 是影响代码时序的地方
            this.Add(new CharacterSystem(contexts, services));
            this.Add(new MoveSystem());

            this.Add(new CharacterMoveSystem());
        }
    }
}
