/*
 * @Author: delevin.ying 
 * @Date: 2022-11-17 19:08:05 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-17 19:54:56
 */

using Entitas;
using UnityEngine;
using Lockstep;
using System.Collections.Generic;



/// <summary>
/// 子弹等物体移动专属
/// </summary>
public class MoveMotionSystem : IExecuteSystem, ISystem
{
    // readonly GameContext _context;

    private readonly IGroup<GameEntity> _moveGroup;
    public MoveMotionSystem(Contexts contexts)
    {
        _moveGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Move, GameMatcher.LocalId, GameMatcher.Bullet));
    }

    void IExecuteSystem.Execute()
    {
        foreach (var entity in _moveGroup.GetEntities())
        {
            var speed = entity.move.speed;
            var forward = entity.move.moveDir;
            var dir = speed * forward;

            entity.position.value += (new LVector3(true, dir._x, 0, dir._z) * GameSetting.Key_Time);


            //             var speed2d = entity.characterInput.moveDir * speed;
            //             var speed2dNor = speed2d.normalized;
            // #if UNITY_EDITOR
            //             var tempV3 = entity.characterInput.moveDir.ToLVector3().ToVector3();
            //             tempV3 = tempV3.normalized;

            //             Debug.DrawRay(entity.position.value.ToVector3(), tempV3 * 3f, Color.blue, 1f);
            // #endif
            //             var state = entity.move.moveState;

            //             if (speed2d.magnitude._val < 100)
            //             {
            //                 entity.move.moveState = MoveState.Idle;
            //                 continue;
            //             }

            //             if (speed._val == 1000)
            //             {
            //                 entity.move.moveState = MoveState.Walk;
            //             }
            //             else if (speed._val > 1000)
            //             {
            //                 entity.move.moveState = MoveState.Run;
            //             }




            //             LFloat angleLf = LMath.AngleInt(currForward2dNor, speed2dNor);
            //             var crossV3 = LVector3.Cross(currForward2dNor, speed2dNor);
            //             bool isRotateToLeft = crossV3._y < 0;
            //             bool needRotate = angleLf._val >= 20 * LFloat.Precision;
            //             // Debug.Log($"baseMove  speed2dNor {speed2dNor}   needRotate {needRotate} isRotateToLeft {isRotateToLeft} ");
            //             if (needRotate)
            //             {
            //                 //先旋转
            //                 RotateEntity(entity, isRotateToLeft);
            //             }
            //             else
            //             {
            //                 //立即转向到方向，然后直接移动
            //                 LVector3 forwardLv3 = speed2d.ToLVector3();
            //                 LQuaternion target = LQuaternion.LookRotation(forwardLv3);
            //                 entity.position.rotate = target;
            //                 entity.position.value += (new LVector3(true, speed2d._x, 0, speed2d._y) * GameSetting.Key_Time);
            //             }
        }
    }

    // private void RotateEntity(GameEntity entity, bool isLeftRotate)
    // {
    //     LFloat rotateAngleSpeedLf = new LFloat(true, GameSetting.ARC_ROTATE_SPEED);
    //     LFloat deltaAngleLf = rotateAngleSpeedLf * GameSetting.Key_Time;
    //     if (isLeftRotate) deltaAngleLf *= -1;
    //     var lq = entity.position.rotate;
    //     entity.position.rotate = LQuaternion.Euler(lq.eulerAngles + (LVector3.up * deltaAngleLf));

    //     var dir = entity.position.rotate * LVector3.forward;
    //     Debug.DrawRay(entity.position.value.ToVector3(), dir.ToVector3() * 3f, Color.red, 0.3f);
    // }
}