﻿using Entitas;

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
                //先用左右作为参考，后续可以补充其他模块进来运算
                num ^= hashableEntity.position.value._x;//X.RawValue;
                num ^= hashableEntity.position.value._y;// Y.RawValue;
                num ^= hashableEntity.position.value._z;
            }
            _gameStateContext.ReplaceHashCode(num);

#if UNITY_EDITOR
            var tick = Contexts.sharedInstance.gameState.tick.value;
            if (DebugSetting.HashCodePrintStepTick > 0 && tick % DebugSetting.HashCodePrintStepTick == 0)
            {
                //间隔输出，防止刷屏
                UnityEngine.Debug.Log($"[HashCode]   hashCode:{num}");
            }
#endif


        }
    }
}