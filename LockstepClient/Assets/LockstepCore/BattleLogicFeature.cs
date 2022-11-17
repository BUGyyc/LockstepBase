/*
 * @Author: delevin.ying 
 * @Date: 2022-11-17 19:15:41 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-17 19:30:25
 */
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
            // this.Add(new CharacterSystem(contexts, services));
            // this.Add(new MoveSystem(contexts));

            // this.Add(new CharacterMoveSystem(contexts));

            OnPreLayer(contexts);

            OnPhysicsLayer(contexts);

            OnLogicLayer(contexts);

            OnViewLayer(contexts);
        }

        /// <summary>
        /// 前置逻辑层
        /// </summary>
        /// <param name="contexts"></param>
        private void OnPreLayer(Contexts contexts)
        {

        }

        /// <summary>
        /// 物理层
        /// </summary>
        /// <param name="contexts"></param>
        private void OnPhysicsLayer(Contexts contexts)
        {

        }

        /// <summary>
        /// 逻辑层
        /// </summary>
        /// <param name="contexts"></param>
        private void OnLogicLayer(Contexts contexts)
        {
            Add(new SkillSystem(contexts));
            Add(new BaseMoveSystem(contexts));
            Add(new MoveMotionSystem(contexts));
        }

        /// <summary>
        /// 显示层
        /// </summary>
        /// <param name="contexts"></param>
        private void OnViewLayer(Contexts contexts)
        {

            //将坐标和朝向传递给 显示层
            Add(new TransformSystem(contexts));
        }
    }
}
