using UnityEngine;
using Lockstep;

public class PositionListener : MonoBehaviour, IEventListener, IPositionListener
{
    private GameEntity _entity;

    //private LVector3 lv3;

    private LFloat FrameStep = new LFloat(true, 20);

    private LVector3 target;

    private LQuaternion targetRotate;
    public void RegisterListeners(GameEntity entity)
    {
        _entity = entity;
        _entity.AddPositionListener(this);

        //lv3 = this.transform.position.ToLVector3();

        //transform.position = 
    }

    public void UnregisterListeners()
    {
        _entity.RemovePositionListener(this);
    }

    public void OnPosition(GameEntity entity, LVector3 position, LQuaternion _rotate)
    {
        //暂时忽略重力
        //var temp = LVector3

        //transform.position = temp;

        target = position;
        targetRotate = _rotate;
    }

    private void FixedUpdate()
    {
        //当前坐标
        var lTransformPos = transform.position.ToLVector3();

        //与目标位置的距离
        LFloat distance = LMath.Distance(target, lTransformPos);

        // Debug.LogFormat($"  dis  {distance}");

        if (distance._val < 200)
        {
            transform.position = target.ToVector3();
        }
        else
        {

            LVector3 framePos = LVector3.Lerp(lTransformPos, target, distance * FrameStep);

            transform.position = framePos.ToVector3();
        }


        LQuaternion q = transform.rotation.ToLQuaternion();
        transform.rotation = LQuaternion.Lerp(q, targetRotate, FrameStep * 30).ToQuaternion();

    }


}

