using Entitas;
using UnityEngine;
using Lockstep;
using System.Collections.Generic;
//NOTE:动画驱动位移
//TODO: 后续改成用 ReactiveSystem 减少计算量
public class CharacterMoveSystem : IExecuteSystem, ISystem
{
    public const string WalkMove = "WalkMove";
    public Vector3 forward1;
    public Vector3 forward2;
    public Vector3 left1;
    public Vector3 left2;
    public Vector3 right1;
    public Vector3 right2;

    public List<Vector3> compareList;

    public const string XKEY = "angle";
    public const string YKEY = "speed";

    public CharacterMoveSystem()
    {
        var cfg = AnimationUtil.GetBlender(WalkMove);

        var nodeList = cfg.NodeList;

        var originNode = nodeList[0];

        forward1 = GetNodeDir(nodeList[1], originNode);
        right1 = GetNodeDir(nodeList[2], originNode);
        left1 = GetNodeDir(nodeList[3], originNode);
        forward2 = GetNodeDir(nodeList[4], originNode);
        left2 = GetNodeDir(nodeList[5], originNode);
        right2 = GetNodeDir(nodeList[6], originNode);

        compareList = new List<Vector3>()
        {
            forward1,left1,right1
        };
    }

    private Vector3 GetNodeDir(Protocol.BlendNode target, Protocol.BlendNode origin)
    {
        Vector3 t3 = new Vector3(target.XPostion, 0, target.YPostion);
        Vector3 o3 = new Vector3(origin.XPostion, 0, origin.YPostion);
        return t3 - o3;
    }


    public void Execute()
    {
        var entites = Contexts.sharedInstance.game.GetEntities();

        foreach (var entity in entites)
        {
            if (/*entity.hasCharacter == false ||*/ entity.hasBackup) continue;

            if (entity.hasAnimtion == false) continue;

            var animation = entity.animation;
            if (animation.readyPlay)
            {
                //开始播放动画
                animation.readyPlay = false;
                animation.PlaySinceStartFrameKey = Contexts.sharedInstance.gameState.tick.value;

                //TODO:临时写法
                var obj = EntityUtil.GetEntityGameObject(entity.localId.value);
                // if (obj == null) continue;
                var ani = obj.GetComponent<Animator>();
                ani.CrossFadeInFixedTime(animation.animationName, 0.1f);
            }
            else
            {
                var name = animation.animationName;
                uint index = Contexts.sharedInstance.gameState.tick.value - animation.PlaySinceStartFrameKey;
                var frameCount = 0;
                int frameId = (int)index;

                if (name == WalkMove)
                {
                    var inputMoveDir = animation.inputParams;
                    var cfg = AnimationUtil.GetBlender(name);


                    var obj = EntityUtil.GetEntityGameObject(entity.localId.value);
                    // if (obj == null) continue;
                    var ani = obj.GetComponent<Animator>();
                    //ani.CrossFadeInFixedTime(animation.animationName, 0.1f);



                    //var inputX = inputMoveDir.x;
                    //var inputY = inputMoveDir.z;

                    //var max = float.MinValue;
                    //var nearDir = Vector3.zero;
                    //for (var i = 0; i < compareList.Count; i++)
                    //{
                    //    var dir = compareList[i];
                    //    var val = Vector3.Dot(dir, inputMoveDir.ToVector3());
                    //    if (val > max)
                    //    {
                    //        nearDir = dir;
                    //    }
                    //}

                    var node = cfg.NodeList[1];
                    frameCount = node.FrameCount;

                    var intX = (int)(node.XPostion * LFloat.Precision) / 5;
                    var intZ = (int)(node.YPostion * LFloat.Precision) / 5;

                    //var motion = node.Motion;

                    //var deltaX = (frameId < motion.PositionXCurveCount) ? motion.GetPositionXCurve(frameId).Value : 0f;

                    ////frame = frame % cfg.PositionYCurveCount;

                    ////var deltaY = (frameId < motion.PositionYCurveCount) ? motion.GetPositionYCurve(frameId).Value : 0f;

                    ////frame = frame % cfg.PositionZCurveCount;

                    //var deltaZ = (frameId < motion.PositionZCurveCount) ? motion.GetPositionZCurve((int)index).Value : 0f;

                    //var intX = (int)(deltaX * LFloat.Precision) / 5;
                    ////var intY = (int)(deltaY * LFloat.Precision);
                    //var intZ = (int)(deltaZ * LFloat.Precision) / 5;
                    entity.position.value += new LVector3(true, intX, 0, intZ);

                    ani.SetFloat(XKEY, node.XPostion);
                    ani.SetFloat(YKEY, node.YPostion);

                }
                else
                {
                    var cfg = AnimationUtil.GetMoveMotion(name);
                    frameCount = cfg.FrameCount;

                    //Debug.Log($"  name {name} index {index}  cfg {cfg == null}  {cfg.FrameCount}   {cfg.PositionYCurveCount}  {cfg.PositionXCurveCount}  {cfg.PositionZCurveCount} ");
                    //index = (uint)(index % cfg.FrameCount);




                    var deltaX = (frameId < cfg.PositionXCurveCount) ? cfg.GetPositionXCurve(frameId).Value : 0f;

                    //frame = frame % cfg.PositionYCurveCount;

                    var deltaY = (frameId < cfg.PositionYCurveCount) ? cfg.GetPositionYCurve(frameId).Value : 0f;

                    //frame = frame % cfg.PositionZCurveCount;

                    var deltaZ = (frameId < cfg.PositionZCurveCount) ? cfg.GetPositionZCurve((int)index).Value : 0f;

                    var intX = (int)(deltaX * LFloat.Precision) / 5;
                    //var intY = (int)(deltaY * LFloat.Precision);
                    var intZ = (int)(deltaZ * LFloat.Precision) / 5;


                    entity.position.value += new LVector3(true, intX, 0, intZ);
                }




                if (index == frameCount)
                {

                    //RandomAnimation(entity, name, animation);

                    RetureDefault(animation);

                }
            }
        }
    }

    private void RetureDefault(AnimationComponent animation)
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

