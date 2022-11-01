using Entitas;

using UnityEngine;

//NOTE:动画驱动位移
//TODO: 后续改成用 ReactiveSystem 减少计算量
public class CharacterMoveSystem : IExecuteSystem, ISystem
{
    public void Execute()
    {
        var entites = Contexts.sharedInstance.game.GetEntities();

        foreach (var entity in entites)
        {
            if (entity.hasCharacter == false || entity.hasBackup) continue;

            if (entity.hasAnimtion == false) continue;

            var animation = entity.animation;
            if (animation.readyPlay)
            {
                animation.readyPlay = false;
                var name = animation.animationName;

                var cfg = AnimationUtil.GetBlender(name);

                Debug.Log($" blender  {cfg == null}  ");
                Debug.Log($" blender  {cfg}  ");
            }
        }
    }
}

