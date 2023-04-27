using Entitas;
using Lockstep.Core.Logic.Systems.Actor;
using Lockstep.Core.Logic.Systems.GameState;

namespace Lockstep.Core.Logic.Systems
{
    /// <summary>
    /// 这里描述的System 顺序很关键
    /// </summary>
    public sealed class WorldSystems : Feature
    {
        /// <summary>
        /// 初始化所有的System，并且按照这个去更新逻辑
        /// </summary>
        /// <param name="contexts"></param>
        /// <param name="features">这个是暴露给外部补充的System,一般我们新增的游戏内System 也是写在这些Feature 内</param>
        public WorldSystems(Contexts contexts, params Feature[] features)
        {
            //初始化
            Add(new InitializeEntityCount(contexts));
            //??预测
            Add(new OnNewPredictionCreateSnapshot(contexts));

            //外部补充的内容System，自定义，后续这里是主要的暴露 System 添加方式
            foreach (Feature feature in features)
            {
                Add(feature);
            }
            //TODO:
            Add(new GameEventSystems(contexts));
            //计算游戏对象的HashCode,判断同步
            Add(new CalculateHashCode(contexts));
            //GameEntity 会在末尾进行销毁
            Add(new DestroyDestroyedGameSystem(contexts));
            //帧累加
            Add(new IncrementTick(contexts));
        }
    }
}

