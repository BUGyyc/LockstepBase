
/*
 * @Author: delevin.ying 
 * @Date: 2022-11-11 17:54:36 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-11 18:30:56
 */
using Entitas;
using Lockstep;
using UnityEngine;

public class AnimationComponent : IComponent
{
    /// <summary>
    /// 所播放的动画的帧号
    /// </summary>
    public uint FrameID;

    /// <summary>
    /// 动画 HashID
    /// </summary>
    public uint AnimationHashID;


    /// <summary>
    /// 动画播放的起始帧号
    /// </summary>
    public uint PlaySinceStartFrameKey;


    /// <summary>
    /// 动画长度
    /// </summary>
    public uint FrameLength;


    /// <summary>
    /// 标记是否当前帧播放
    /// </summary>
    public bool readyPlay;

    private string _animationName;


    public string animationName
    {
        set
        {
            _animationName = value;
            Debug.Log($"value write {value}");
        }
        get
        {
            return _animationName;
        }
    }


    public LVector3 inputParams;

    public LVector2 originInput;

    public AnimationMainState state;

}

public enum AnimationMainState
{
    Move = 0,
    Skill
}



public sealed partial class GameEntity : Entity
{
    // public AnimationComponent Animation => (AnimationComponent)GetComponent(GameComponentsLookup.Animation);

    // public bool hasAnimation => HasComponent(GameComponentsLookup.Animation);

    public const string XKEY = "angle";
    public const string YKEY = "speed";

    public const int SCALE_X = 5;
    public const int SCALE_Z = 5;

    public const int WALK_SCALE = 3;

    public bool IsRotating = false;

    public bool IsLeftRotate = false;

    //TODO:临时写法

    public Animator animator;

    /// <summary>
    /// 开始播放时，调度 Animator
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="entity"></param>
    public void ReadyPlayAnimation(AnimationComponent animation, GameEntity entity)
    {
        //开始播放动画
        animation.readyPlay = false;
        animation.PlaySinceStartFrameKey = Contexts.sharedInstance.gameState.tick.value;

        //TODO:临时写法

        if (animator == null)
        {
            var obj = EntityUtil.GetEntityGameObject(entity.localId.value);
            animator = obj.GetComponentInChildren<Animator>();
        }
        Debug.Log($"播放动画 {animation.animationName}");
        animator.CrossFadeInFixedTime(animation.animationName, 0.1f);

    }

    /// <summary>
    /// 逐帧写位移
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="entity"></param>
    public void ExecuteAnimation(AnimationComponent animation, GameEntity entity)
    {
        var name = animation.animationName;
        uint index = Contexts.sharedInstance.gameState.tick.value - animation.PlaySinceStartFrameKey;
        uint frameCount = 0;
        int frameId = (int)index;

        if (string.IsNullOrEmpty(name))
        {
            Debug.LogError("动画异常");
            return;
        }

        // Debug.LogError($"--------------------------{Contexts.sharedInstance.gameState.tick.value}");

        // if (name == GameSetting.WalkMove)
        // {
        if (animation.state == AnimationMainState.Move)
        {
            ExecuteMoveBlender(entity, animation, name, out frameCount);
        }
        else if (animation.state == AnimationMainState.Skill)
        {
            ExecuteAnyMotion(entity, animation, name, frameId, out frameCount);
        }


        // }
        // else
        // {
        //     ExecuteAnyMotion(entity, animation, name, frameId, out frameCount);
        // }

        if (IsRotating)
        {
            ExecuteRotate(entity, animation);
        }

        if (index == frameCount)
        {
            CompleteMotionFrame(animation);
        }
    }

    private void ExecuteRotate(GameEntity entity, AnimationComponent animation)
    {
        // LFloat heroYAngle = position.rotate.eulerAngles.y;

        LFloat rotateAngleSpeedLf = new LFloat(true, GameSetting.ARC_ROTATE_SPEED);

        LFloat deltaAngleLf = rotateAngleSpeedLf * GameSetting.Key_Time;

        if (IsLeftRotate) deltaAngleLf *= -1;

        var lq = entity.position.rotate;

        entity.position.rotate = LQuaternion.Euler(lq.eulerAngles + (LVector3.up * deltaAngleLf));

        var dir = entity.position.rotate * LVector3.forward;

        Debug.DrawRay(entity.position.value.ToVector3(), dir.ToVector3() * 3f, Color.red, 0.3f);

        var worldMoveDir = animation.inputParams;
        var heroDir = dir;

        var w2D = worldMoveDir.YZero();
        var h2D = heroDir.YZero();

        LFloat angleLf = LMath.AngleInt(h2D, w2D);

        Debug.LogFormat($" angleLF {angleLf} ");

        if (angleLf._val <= 10 * LFloat.Precision)
        {
            entity.position.rotate = LQuaternion.LookRotation(w2D);
            StopRotate(animation);
        }


    }


    private void ExecuteMoveBlender(GameEntity entity, AnimationComponent animation, string name, out uint frameCount)
    {
        /// <summary>
        /// 世界坐标系下的目标移动方向
        /// </summary>
        var worldMoveDir = animation.inputParams;
        var heroDir = entity.position.rotate * LVector3.forward;

        var inputOrigin = animation.originInput;

        // Debug.LogError($"worldMoveDir{worldMoveDir}");

        if (worldMoveDir.x == 0 && worldMoveDir.z == 0)
        {
            //代表 Stop,所有基础移动都应该停止

            if (IsRotating)
            {
                // Debug.LogError($"立即暂停");
                StopRotate(animation);
            }
        }



        var worldMoveDir2D = worldMoveDir.YZero().Normalize();
        var heroDir2D = heroDir.YZero().Normalize();

        Debug.DrawRay(entity.position.value.ToVector3(), heroDir2D.ToVector3() * 3, Color.blue, 10f);

        // Debug.DrawRay(entity.position.value.ToVector3(), worldMoveDir2D.ToVector3() * 3, Color.yellow, 10f);

        LFloat angleLf = LMath.AngleInt(heroDir2D, worldMoveDir2D);
        if (worldMoveDir2D.magnitude._val < 100)
        {
            angleLf = LFloat.zero;
        }
        var crossV3 = LVector3.Cross(heroDir2D, worldMoveDir2D);

        bool isRotateToLeft = crossV3._y < 0;


        //先旋转 再移动
        if (angleLf._val >= 30 * LFloat.Precision)
        {
            var clip = isRotateToLeft ? GameSetting.ARC_ROTATE_LEFT : GameSetting.ARC_ROTATE_RIGHT;
            //大角度旋转
            var rotateCfg = AnimationUtil.GetMoveMotion(clip);
            frameCount = (uint)rotateCfg.FrameCount;

            Debug.Log($"<color=red> 大角度旋转 isRotateToLeft {isRotateToLeft} frameCount  {frameCount}</color>");

            PlayAnimation(animation, clip);

            IsRotating = true;

            IsLeftRotate = isRotateToLeft;

        }
        else if (angleLf._val > 10 * LFloat.Precision)
        {
            var clip = isRotateToLeft ? GameSetting.ARC_ROTATE_LEFT : GameSetting.ARC_ROTATE_RIGHT;
            //小角度旋转
            frameCount = uint.MaxValue;

            Debug.Log($"<color=red> 小角度旋转 isRotateToLeft {isRotateToLeft} frameCount  {frameCount}</color>");

            // IsRotating = true;

            // IsLeftRotate = isRotateToLeft;
            entity.position.rotate = LQuaternion.LookRotation(worldMoveDir2D);
            StopRotate(animation);
        }
        else
        {
            //容忍范围内，无需表现上的旋转
            //直接移动
            var cfg = AnimationUtil.GetBlender(name);
            if (cfg == null)
            {
                frameCount = uint.MaxValue;
            }
            else
            {
                var node = cfg.NodeList[1];
                frameCount = (uint)node.FrameCount;
            }

            // var inputMoveDir = animation.inputParams;
            // inputMoveDir._z = (inputMoveDir._z < 0) ? 0 : inputMoveDir._z;


            // var intX = inputMoveDir._x / SCALE_X;
            // var intZ = inputMoveDir._z / SCALE_Z;

            var entityForward = entity.position.rotate * LVector3.forward;
            entityForward = entityForward.Normalize();
            entity.position.value += entityForward * (inputOrigin.y / SCALE_Z) / WALK_SCALE;

            // float x = inputMoveDir._x * LFloat.PrecisionFactor;
            float y = inputOrigin._y * LFloat.PrecisionFactor;

            // Debug.Log($"<color=red>  delta {entityForward}       {inputOrigin.y}  </color>");

            // animator.SetFloat(XKEY, x);
            animator.SetFloat(YKEY, y);

            PlayAnimation(animation, GameSetting.WalkMove);
        }



        // Debug.Log($"<color=red>  angleLf {angleLf._val}   angleLf  {angleLf}    worldMoveDir2D {worldMoveDir2D.magnitude}   isRotateToLeft {isRotateToLeft} </color>");






        // var inputMoveDir = animation.inputParams;
        // var cfg = AnimationUtil.GetBlender(name);
        // //TODO:临时写法
        // if (animator == null)
        // {
        //     var obj = EntityUtil.GetEntityGameObject(entity.localId.value);
        //     animator = obj.GetComponentInChildren<Animator>();
        // }

        // //通过当前玩家朝向，得到目标转向与速度
        // //用相机朝向

        // var characterForward = entity.entityForwardLv3;

        // LFloat val = LVector3.Dot(inputMoveDir, characterForward);



        // LFloat angle = LMath.AngleInt(characterForward, inputMoveDir);


        // // Debug.Log($"<color=red>  angle {angle} val  {val} </color>");

        // var lv3 = inputMoveDir;
        // lv3 = lv3.normalized;

        // Debug.DrawRay(entity.position.value.ToVector3(), lv3.ToVector3(), Color.blue, 3f);

        // // var forward = CameraManager.Instance.forwardZeroY;
        // // //计算输入方向
        // // MoveDir moveDir = GetMoveDir(inputMoveDir);
        // // GetTargetDir(moveDir, forward);



        // //NOTE: inputMoveDir 是玩家输入，
        // //需要通过计算采样得到最终的混合速度

        // var node = cfg.NodeList[1];
        // frameCount = (uint)node.FrameCount;
        // var motion = node.Motion;
        // // Debug.Log($"<color=red> Motion </color>{motion == null}   ");

        // // var intX = (int)(node.XPostion * LFloat.Precision) / SCALE_X;
        // // var intZ = (int)(node.YPostion * LFloat.Precision) / SCALE_Z;

        // //TODO:
        // inputMoveDir._z = (inputMoveDir._z < 0) ? 0 : inputMoveDir._z;


        // var intX = inputMoveDir._x / SCALE_X;
        // var intZ = inputMoveDir._z / SCALE_Z;

        // entity.position.value += new LVector3(true, intX, 0, intZ);

        // float x = inputMoveDir._x * LFloat.PrecisionFactor;
        // float y = inputMoveDir._z * LFloat.PrecisionFactor;

        // // Debug.Log($"<color=red>   x {x}  y {y}  </color>");

        // animator.SetFloat(XKEY, x);
        // animator.SetFloat(YKEY, y);
    }

    private void StopRotate(AnimationComponent animation)
    {
        IsRotating = false;
        PlayAnimation(animation, "Idle_Wait_A");
    }

    private void ExecuteAnyMotion(GameEntity entity, AnimationComponent animation, string name, int frameId, out uint frameCount)
    {
        var cfg = AnimationUtil.GetMoveMotion(name);
        frameCount = (uint)cfg.FrameCount;

        var deltaX = (frameId < cfg.PositionXCurveCount) ? cfg.GetPositionXCurve(frameId).Value : 0f;
        var deltaY = (frameId < cfg.PositionYCurveCount) ? cfg.GetPositionYCurve(frameId).Value : 0f;
        var deltaZ = (frameId < cfg.PositionZCurveCount) ? cfg.GetPositionZCurve(frameId).Value : 0f;


        var deltaDir = new Vector3(deltaX / SCALE_X, 0, deltaZ / SCALE_Z);
        var realAngle = entity.position.rotate.eulerAngles.ToVector3().y;
        var realDelta = Quaternion.AngleAxis(realAngle, Vector3.up) * deltaDir;

        entity.position.value += realDelta.ToLVector3();


        // var intX = (int)(deltaX * LFloat.Precision) / SCALE_X;

        // var intZ = (int)(deltaZ * LFloat.Precision) / SCALE_Z;

        // var rotate = entity.position.rotate;
        // var forward = rotate * LVector3.forward;
        // var _deltaX = (forward * intX).x;
        // var _deltaY = (forward * intZ).z;
        // entity.position.value += new LVector3(true, _deltaX, 0, _deltaY);

    }



    /// <summary>
    /// 执行完动画最后一帧，是否进行动画重置，例如重置到Idle
    /// </summary>
    private void CompleteMotionFrame(AnimationComponent animation)
    {
        ReturnDefault(animation);
    }

    public void PlayAnimation(AnimationComponent animation, string name)
    {
        if (animation.animationName == name) return;
        animation.animationName = name;
        animation.PlaySinceStartFrameKey = Contexts.sharedInstance.gameState.tick.value;
        animation.readyPlay = true;
    }


    private void ReturnDefault(AnimationComponent animation)
    {
        Debug.Log($"  {animation.animationName}  播放结束, 重置动画为 {GameSetting.ReturnIDLE}  ");
        PlayAnimation(animation, GameSetting.ReturnIDLE);
        animation.state = AnimationMainState.Move;
        // animation.animationName = GameSetting.ReturnIDLE;
        // animation.PlaySinceStartFrameKey = Contexts.sharedInstance.gameState.tick.value;
        // animation.readyPlay = true;
    }

    private void RandomAnimation(GameEntity entity, string name, AnimationComponent animation)
    {
        Debug.Log($"  name  {name}  播放结束 ");

        uint sum = entity.id.value + Contexts.sharedInstance.gameState.tick.value;
        int type = (int)sum % 4;

        switch (type)
        {
            case 0:
                animation.animationName = "Esc_BoostDash_Left_Root";
                break;
            case 1:
                animation.animationName = "Esc_BoostDash_Right_Root";
                break;

            case 2:
                animation.animationName = "Esc_BoostDash_Front_Root";
                break;
            case 3:
                animation.animationName = "Esc_BoostDash_Back_Root";
                break;

        }

        animation.PlaySinceStartFrameKey = Contexts.sharedInstance.gameState.tick.value;
        animation.readyPlay = true;
    }

}

// public sealed partial class GameMatcher
// {
//     private static IMatcher<GameEntity> _animation;
//     public static IMatcher<GameEntity> Animation
//     {
//         get
//         {
//             if (_animation == null)
//             {
//                 Matcher<GameEntity> val = Matcher<GameEntity>.AllOf(new int[1] { GameComponentsLookup.Animation }) as Matcher<GameEntity>;
//                 val.componentNames = GameComponentsLookup.componentNames;
//                 _animation = val;
//             }
//             return _animation;
//         }
//     }
// }

public enum MoveDir
{
    Idle = 0,
    Forward = 1 << 1,
    Right = 1 << 2,
    Back = 1 << 3,
    Left = 1 << 4,

    FR = Forward | Right,
    FL = Forward | Left,
    BR = Back | Right,
    BL = Back | Left
}