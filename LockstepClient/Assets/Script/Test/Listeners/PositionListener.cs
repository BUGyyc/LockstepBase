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

    public void OnPosition(GameEntity entity, BEPUutilities.Vector2 newPosition)
    {
        var temp = new Vector3((float)newPosition.X, 1, (float)newPosition.Y);

        //transform.position = temp;

        target = temp.ToLVector3();
    }

    private void FixedUpdate()
    {

        var lTransformPos = transform.position.ToLVector3();

        LFloat distance = LMath.Distance(target, lTransformPos);

        //distance *= 10;

        LVector3 framePos = LVector3.Lerp(lTransformPos, target, distance * FrameStep);

        transform.position = framePos.ToVector3();
    }


}

//public struct FixV3
//{
//    public Fix64 fixX;
//    public Fix64 fixY;
//    public Fix64 fixZ;
//    public const int Mul = 10000;
//    public FixV3(Fix64 x, Fix64 y, Fix64 z)
//    {
//        fixX = x;
//        fixY = y;
//        fixZ = z;
//    }

//    public FixV3(float x, float y, float z)
//    {
//        int _x = (int)(x * Mul);
//        int _y = (int)(y * Mul);
//        int _z = (int)(z * Mul);
//        fixX = _x;
//        fixY = _y;
//        fixZ = _z;
//    }

//    public FixV3(int x, int y, int z)
//    {
//        fixX = x;
//        fixY = y;
//        fixZ = z;
//    }

//    public Fix64 Distance(FixV3 a, FixV3 b)
//    {

//    }
//}