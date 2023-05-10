using Entitas;
using System.Collections.Generic;

namespace Lockstep.Core.Logic.Systems.GameState
{

    public class CalculateHashCode : IInitializeSystem, ISystem, IExecuteSystem
    {
        private readonly IGroup<GameEntity> _hashableEntities;

        private readonly GameStateContext _gameStateContext;

        private Dictionary<uint, List<long>> hashCodeDic = new Dictionary<uint, List<long>>();

        private uint lastCheckTick;

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
                //UnityEngine.Debug.Log($"[HashCode]  tick: {tick}   hashCode:{num}"); //frame:{ActionWorld.Instance.Simulation.GetWorld().Tick}   {tick} 

                if (hashCodeDic.TryGetValue(tick - (uint)GlobalSetting.LagCompensation, out var hashList))
                {
                    var hash = hashList[0];
                    bool desynced = false;
                    for (int i = 1; i < hashList.Count; i++)
                    {
                        if (hash != hashList[i])
                        {
                            desynced = true;
                            break;
                        }
                    }

                    if (desynced)
                    {
                        LogMaster.E("异常 HashCode 不一样");
                    }
                    else
                    {
                        //LogMaster.L(" 数据正常，HashCode 未发现异常，同步正常  ");
                    }
                }

                if (tick > lastCheckTick)
                {
                    if (hashCodeDic.ContainsKey(tick))
                    {
                        hashCodeDic[tick].AddRange(new List<long>() { num });
                    }
                    else
                    {
                        hashCodeDic.Add(tick, new List<long>() { num });
                    }
                    lastCheckTick = tick;
                }

            }
#endif


        }
    }
}