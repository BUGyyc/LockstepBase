/*
 * @Author: delevin.ying 
 * @Date: 2022-11-04 10:22:24 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-04 11:39:53
 */
using Entitas;
using UnityEngine;
using Lockstep;
using System.Collections.Generic;
//NOTE:动画驱动位移
//TODO: 后续改成用 ReactiveSystem 减少计算量
public class CharacterMoveSystem : IExecuteSystem, ISystem
{



    public Vector3 forward1;
    public Vector3 forward2;
    public Vector3 left1;
    public Vector3 left2;
    public Vector3 right1;
    public Vector3 right2;

    public List<Vector3> compareList;


    private readonly IGroup<GameEntity> _characterGroup;

    public CharacterMoveSystem(Contexts contexts)
    {

        _characterGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Animation, GameMatcher.LocalId));

        var cfg = AnimationUtil.GetBlender(GameSetting.WalkMove);

        var nodeList = cfg.NodeList;

        var originNode = nodeList[0];

        // forward1 = GetNodeDir(nodeList[1], originNode);
        // right1 = GetNodeDir(nodeList[2], originNode);
        // left1 = GetNodeDir(nodeList[3], originNode);
        // forward2 = GetNodeDir(nodeList[4], originNode);
        // left2 = GetNodeDir(nodeList[5], originNode);
        // right2 = GetNodeDir(nodeList[6], originNode);

        // compareList = new List<Vector3>()
        // {
        //     forward1,left1,right1
        // };
    }

    private Vector3 GetNodeDir(Protocol.BlendNode target, Protocol.BlendNode origin)
    {
        Vector3 t3 = new Vector3(target.XPostion, 0, target.YPostion);
        Vector3 o3 = new Vector3(origin.XPostion, 0, origin.YPostion);
        return t3 - o3;
    }


    public void Execute()
    {
        var entites = _characterGroup.GetEntities();

        foreach (var entity in entites)
        {

            if (entity.hasAnimation == false) continue;

            var animation = entity.Animation;
            if (animation.readyPlay)
            {
                entity.ReadyPlayAnimation(animation, entity);
            }
            else
            {
                entity.ExecuteAnimation(animation, entity);
            }
        }
    }


}

