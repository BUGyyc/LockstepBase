using Lockstep;

public interface IPositionListener
{
    void OnPosition(GameEntity entity, LVector3 value, LQuaternion rotate);

    void SetLocationRightNow(GameEntity entity, LVector3 value, LQuaternion rotate);
}
