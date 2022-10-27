using Entitas;

namespace Lockstep.Core.Logic.Systems.GameState
{

    public class CalculateHashCode : IInitializeSystem, ISystem, IExecuteSystem
    {
        private readonly IGroup<GameEntity> _hashableEntities;

        private readonly GameStateContext _gameStateContext;

        /// <summary>
        /// 通过位置、速度、目标点来计算HashCode。这个可以作为同步的判断标准之一
        /// </summary>
        /// <param name="contexts"></param>
        public CalculateHashCode(Contexts contexts)
        {
            _gameStateContext = contexts.gameState;
            _hashableEntities = (contexts.game).GetGroup((IMatcher<GameEntity>)(object)((IAnyOfMatcher<GameEntity>)(object)GameMatcher.AllOf(GameMatcher.LocalId, GameMatcher.Position)).NoneOf(new IMatcher<GameEntity>[1] { GameMatcher.Backup }));
        }

        public void Initialize()
        {
            _gameStateContext.ReplaceHashCode(0L);
        }

        public void Execute()
        {
            long num = 0L;
            num ^= (_hashableEntities).count;
            foreach (GameEntity hashableEntity in _hashableEntities)
            {
                num ^= hashableEntity.position.value.X.RawValue;
                num ^= hashableEntity.position.value.Y.RawValue;
                if (hashableEntity.hasVelocity)
                {
                    num ^= hashableEntity.velocity.value.X.RawValue;
                    num ^= hashableEntity.velocity.value.Y.RawValue;
                }
                if (hashableEntity.hasDestination)
                {
                    num ^= hashableEntity.destination.value.X.RawValue;
                    num ^= hashableEntity.destination.value.Y.RawValue;
                }
            }
            _gameStateContext.ReplaceHashCode(num);
        }
    }
}