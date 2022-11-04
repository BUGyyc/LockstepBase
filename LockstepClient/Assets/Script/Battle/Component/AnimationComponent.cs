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


    public string animationName;


    public LVector3 inputParams;


}



public sealed partial class GameEntity : Entity
{
    public AnimationComponent Animation => (AnimationComponent)GetComponent(GameComponentsLookup.Animation);

    public bool hasAnimation => HasComponent(GameComponentsLookup.Animation);

    public const string WalkMove = "WalkMove";
    public const string XKEY = "angle";
    public const string YKEY = "speed";

    public const int SCALE_X = 10;
    public const int SCALE_Z = 10;

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
            animator = obj.GetComponent<Animator>();
        }
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

        if (name == WalkMove)
        {
            ExecuteMoveBlender(entity, animation, name, out frameCount);
        }
        else
        {
            ExecuteAnyMotion(entity, animation, name, frameId, out frameCount);
        }

        if (index == frameCount)
        {
            CompleteMotionFrame(animation);
        }
    }


    private void ExecuteMoveBlender(GameEntity entity, AnimationComponent animation, string name, out uint frameCount)
    {
        var inputMoveDir = animation.inputParams;
        var cfg = AnimationUtil.GetBlender(name);
        //TODO:临时写法
        if (animator == null)
        {
            var obj = EntityUtil.GetEntityGameObject(entity.localId.value);
            animator = obj.GetComponent<Animator>();
        }

        //通过当前玩家朝向，得到目标转向与速度
        //用相机朝向

        var characterForward = entity.entityForwardLv3;
        
        LFloat val = LVector3.Dot(inputMoveDir, characterForward);



        LFloat angle = LMath.AngleInt(characterForward, inputMoveDir);


        Debug.Log($"<color=red>  angle {angle} val  {val} </color>");

        var lv3 = inputMoveDir;
        lv3 = lv3.normalized;

        Debug.DrawRay(entity.position.value.ToVector3(), lv3.ToVector3(), Color.blue, 3f);

        // var forward = CameraManager.Instance.forwardZeroY;
        // //计算输入方向
        // MoveDir moveDir = GetMoveDir(inputMoveDir);
        // GetTargetDir(moveDir, forward);



        //NOTE: inputMoveDir 是玩家输入，
        //需要通过计算采样得到最终的混合速度

        var node = cfg.NodeList[1];
        frameCount = (uint)node.FrameCount;
        var motion = node.Motion;
        // Debug.Log($"<color=red> Motion </color>{motion == null}   ");

        // var intX = (int)(node.XPostion * LFloat.Precision) / SCALE_X;
        // var intZ = (int)(node.YPostion * LFloat.Precision) / SCALE_Z;

        //TODO:
        inputMoveDir._z = (inputMoveDir._z < 0) ? 0 : inputMoveDir._z;


        var intX = inputMoveDir._x / SCALE_X;
        var intZ = inputMoveDir._z / SCALE_Z;

        entity.position.value += new LVector3(true, intX, 0, intZ);

        float x = inputMoveDir._x * LFloat.PrecisionFactor;
        float y = inputMoveDir._z * LFloat.PrecisionFactor;

        // Debug.Log($"<color=red>   x {x}  y {y}  </color>");

        animator.SetFloat(XKEY, x);
        animator.SetFloat(YKEY, y);
    }

    private void ExecuteAnyMotion(GameEntity entity, AnimationComponent animation, string name, int frameId, out uint frameCount)
    {
        var cfg = AnimationUtil.GetMoveMotion(name);
        frameCount = (uint)cfg.FrameCount;

        var deltaX = (frameId < cfg.PositionXCurveCount) ? cfg.GetPositionXCurve(frameId).Value : 0f;
        var deltaY = (frameId < cfg.PositionYCurveCount) ? cfg.GetPositionYCurve(frameId).Value : 0f;
        var deltaZ = (frameId < cfg.PositionZCurveCount) ? cfg.GetPositionZCurve(frameId).Value : 0f;

        var intX = (int)(deltaX * LFloat.Precision) / SCALE_X;

        var intZ = (int)(deltaZ * LFloat.Precision) / SCALE_Z;

        entity.position.value += new LVector3(true, intX, 0, intZ);

    }



    /// <summary>
    /// 执行完动画最后一帧，是否进行动画重置，例如重置到Idle
    /// </summary>
    private void CompleteMotionFrame(AnimationComponent animation)
    {
        ReturnDefault(animation);
    }


    private void ReturnDefault(AnimationComponent animation)
    {
        animation.animationName = "Idle_Wait_C";
        animation.PlaySinceStartFrameKey = Contexts.sharedInstance.gameState.tick.value;
        animation.readyPlay = true;
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

public sealed partial class GameMatcher
{
    private static IMatcher<GameEntity> _animation;
    public static IMatcher<GameEntity> Animation
    {
        get
        {
            if (_animation == null)
            {
                Matcher<GameEntity> val = Matcher<GameEntity>.AllOf(new int[1] { GameComponentsLookup.Animation }) as Matcher<GameEntity>;
                val.componentNames = GameComponentsLookup.componentNames;
                _animation = val;
            }
            return _animation;
        }
    }
}

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