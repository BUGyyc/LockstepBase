using Entitas;

namespace Lockstep.Core.Logic.Systems.GameState
{
    /// <summary>
    /// 单纯的进行帧累加
    /// </summary>
    public class IncrementTick : IInitializeSystem, ISystem, IExecuteSystem
    {
        private readonly GameStateContext _gameStateContext;

        /// <summary>
        /// 单纯的进行帧累加
        /// </summary>
        /// <param name="contexts"></param>
        public IncrementTick(Contexts contexts)
        {
            _gameStateContext = contexts.gameState;
        }

        public void Initialize()
        {
            _gameStateContext.SetTick(0u);
        }

        public void Execute()
        {
            _gameStateContext.tickEntity.ReplaceTick(_gameStateContext.tick.value + 1);
        }
    }
}


