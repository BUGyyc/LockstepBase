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

        if (_entity.position != null)
        {
            transform.position = _entity.position.value.ToVector3();
            transform.rotation = _entity.position.rotate.ToQuaternion();
        }


        _entity.OnDestroyEntity += (eve) =>
        {
            //Debug.Log("GameEntity 销毁");

            GameObject.Destroy(gameObject);
        };

        //_entity.OnDestroyEntity += GameEntityDestroy();

        //lv3 = this.transform.position.ToLVector3();

        //transform.position =
    }

    //private Entitas.EntityEvent GameEntityDestroy()
    //{
    //    _entity.RemovePositionListener(this);

    //    return default;
    //}

    public void UnregisterListeners()
    {
        _entity.RemovePositionListener(this);
    }

    public void OnPosition(GameEntity entity, LVector3 position, LQuaternion _rotate)
    {
        //暂时忽略重力
        //var temp = LVector3

        //transform.position = temp;

        //数据层的坐标和朝向
        target = position;
        targetRotate = _rotate;
    }

    private void Update()
    {
        var lastViewPos = transform.position;
        //var lastViewRot = transform.rotation;
        var nowViewPos = target.ToVector3();

        var distance = Vector3.Distance(lastViewPos, nowViewPos);
        if (distance < 1)
        {
            transform.position = Vector3.Lerp(transform.position, nowViewPos, 0.4f);
            //Debug.Log($"显示层 插值位置  {nowViewPos} ");
        }
        else if (distance < 2)
        {
            transform.position = Vector3.Lerp(transform.position, nowViewPos, 0.8f);
            //Debug.Log("网络波动很大，纠正位置");
        }
        else
        {
            //Debug.Log("网络波动巨大，直接设定位置");
            transform.position = nowViewPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(target.ToVector3(), 1f);
    }

    private void FixedUpdate()
    {
        //当前坐标
        //var lTransformPos = transform.position.ToLVector3();

        ////与目标位置的距离
        //LFloat distance = LMath.Distance(target, lTransformPos);

        //// Debug.LogFormat($"  dis  {distance}");

        //if (distance._val < 200)
        //{
        //    transform.position = target.ToVector3();
        //}
        //else
        //{

        //    LVector3 framePos = LVector3.Lerp(lTransformPos, target, distance * FrameStep);

        //    transform.position = framePos.ToVector3();
        //}


        //LQuaternion q = transform.rotation.ToLQuaternion();
        //transform.rotation = LQuaternion.Lerp(q, targetRotate, FrameStep * 30).ToQuaternion();
    }

    public void SetLocationRightNow(GameEntity entity, LVector3 value, LQuaternion rotate)
    {
        transform.position = value.ToVector3();
        transform.rotation = rotate.ToQuaternion();
    }
}
