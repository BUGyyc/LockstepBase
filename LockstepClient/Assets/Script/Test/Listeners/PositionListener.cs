using UnityEngine;
using Lockstep;

public class PositionListener : MonoBehaviour, IEventListener, IPositionListener
{
    private GameEntity _entity;

    //private LVector3 lv3;

    private LFloat FrameStep = new LFloat(0.02f);

    private LVector3 target;
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

    public void OnPosition(GameEntity entity, LVector3 newPosition)
    {
        //暂时忽略重力
        //var temp = LVector3

        //transform.position = temp;

        target = newPosition;
    }

    private void FixedUpdate()
    {

        var lTransformPos = transform.position.ToLVector3();

        LFloat distance = LMath.Distance(target, lTransformPos);


        LVector3 framePos = LVector3.Lerp(lTransformPos, target, distance * FrameStep);

        transform.position = framePos.ToVector3();
    }


}

