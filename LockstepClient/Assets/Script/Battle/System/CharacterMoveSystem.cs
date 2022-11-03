using Entitas;
using UnityEngine;
using Lockstep;
//NOTE:动画驱动位移
//TODO: 后续改成用 ReactiveSystem 减少计算量
public class CharacterMoveSystem : IExecuteSystem, ISystem
{
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
                var ani = obj.GetComponent<Animator>();
                ani.CrossFadeInFixedTime(animation.animationName, 0.1f);
            }
            else
            {
                var name = animation.animationName;
                var cfg = AnimationUtil.GetMoveMotion(name);
                uint index = Contexts.sharedInstance.gameState.tick.value - animation.PlaySinceStartFrameKey;

                //Debug.Log($"  name {name} index {index}  cfg {cfg == null}  {cfg.FrameCount}   {cfg.PositionYCurveCount}  {cfg.PositionXCurveCount}  {cfg.PositionZCurveCount} ");
                //index = (uint)(index % cfg.FrameCount);

                int frameId = (int)index;


                var deltaX = (frameId < cfg.PositionXCurveCount) ? cfg.GetPositionXCurve(frameId).Value : 0f;

                //frame = frame % cfg.PositionYCurveCount;

                var deltaY = (frameId < cfg.PositionYCurveCount) ? cfg.GetPositionYCurve(frameId).Value : 0f;

                //frame = frame % cfg.PositionZCurveCount;

                var deltaZ = (frameId < cfg.PositionZCurveCount) ? cfg.GetPositionZCurve((int)index).Value : 0f;

                var intX = (int)(deltaX * LFloat.Precision) / 5;
                //var intY = (int)(deltaY * LFloat.Precision);
                var intZ = (int)(deltaZ * LFloat.Precision) / 5;
                //Fix64 x = new Fix64(intX);
                //Fix64 z = new Fix64(intZ);

                entity.position.value += new LVector3(true, intX, 0, intZ);


                if (index == cfg.FrameCount)
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

