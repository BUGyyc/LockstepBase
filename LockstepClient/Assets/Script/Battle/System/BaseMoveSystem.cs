/*
 * @Author: delevin.ying 
 * @Date: 2022-11-17 11:16:09 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-17 15:09:09
 */
using Entitas;
using UnityEngine;
using Lockstep;
using System.Collections.Generic;


//NOTE: ReactiveSystem 很特别，只有通过过滤条件的Entity 才具备执行的条件，这意味着可以在某种情况下减少无效的执行
// IExecuteSystem 的情况就不一样，是每次都是全部Entity. 所以在某些灵敏的检查需求下，才需要每次执行所有Entity

/// <summary>
/// 基础移动
/// </summary>
public class BaseMoveSystem : IExecuteSystem, ISystem
{
    // readonly GameContext _context;

    private readonly IGroup<GameEntity> _characterGroup;
    public BaseMoveSystem(Contexts contexts)
    {
        _characterGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Move, GameMatcher.LocalId));
    }

    void IExecuteSystem.Execute()
    {
        foreach (var entity in _characterGroup.GetEntities())
        {
            var currForward = entity.entityForwardLv3;
            var currForward2d = currForward.ToLVector2();
            var currForward2dNor = currForward2d.normalized;

            var speed = entity.move.speed;
            var speed2d = entity.characterInput.moveDir * speed;
            var speed2dNor = speed2d.normalized;
#if UNITY_EDITOR
            var tempV3 = entity.characterInput.moveDir.ToLVector3().ToVector3();
            tempV3 = tempV3.normalized;

            Debug.DrawRay(entity.position.value.ToVector3(), tempV3 * 3f, Color.blue, 1f);
#endif
            var state = entity.move.moveState;

            if (speed2d.magnitude._val < 100)
            {
                entity.move.moveState = MoveState.Idle;
                continue;
            }

            if (speed._val == 1000)
            {
                entity.move.moveState = MoveState.Walk;
            }
            else if (speed._val > 1000)
            {
                entity.move.moveState = MoveState.Run;
            }




            LFloat angleLf = LMath.AngleInt(currForward2dNor, speed2dNor);
            var crossV3 = LVector3.Cross(currForward2dNor, speed2dNor);
            bool isRotateToLeft = crossV3._y < 0;
            bool needRotate = angleLf._val >= 20 * LFloat.Precision;
            Debug.Log($"baseMove  speed2dNor {speed2dNor}   needRotate {needRotate} isRotateToLeft {isRotateToLeft} ");
            if (needRotate)
            {
                //先旋转
                RotateEntity(entity, isRotateToLeft);
            }
            else
            {
                //立即转向到方向，然后直接移动
                LVector3 forwardLv3 = speed2d.ToLVector3();
                LQuaternion target = LQuaternion.LookRotation(forwardLv3);
                entity.position.rotate = target;
                entity.position.value += (new LVector3(true, speed2d._x, 0, speed2d._y) * GameSetting.Key_Time);
            }
        }
    }

    private void RotateEntity(GameEntity entity, bool isLeftRotate)
    {
        LFloat rotateAngleSpeedLf = new LFloat(true, GameSetting.ARC_ROTATE_SPEED);
        LFloat deltaAngleLf = rotateAngleSpeedLf * GameSetting.Key_Time;
        if (isLeftRotate) deltaAngleLf *= -1;
        var lq = entity.position.rotate;
        entity.position.rotate = LQuaternion.Euler(lq.eulerAngles + (LVector3.up * deltaAngleLf));

        var dir = entity.position.rotate * LVector3.forward;
        Debug.DrawRay(entity.position.value.ToVector3(), dir.ToVector3() * 3f, Color.red, 0.3f);
    }
}