//using BEPUutilities;
using Entitas;
using Lockstep;
using UnityEngine;
namespace Lockstep.Core.State.Game
{
    [Game]
    //[Event(/*Could not decode attribute arguments.*/)]
    public class PositionComponent : IComponent
    {
        //public Vector2 value;

        public LVector3 value;

        public LQuaternion rotate;
    }



}


public sealed partial class GameEntity : Entity
{
    public LVector3 entityForwardLv3
    {
        get
        {
            if (hasPosition == false)
            {

                return LVector3.zero;
            }

            return position.rotate * LVector3.forward;
        }
    }

    /// <summary>
    /// 不建议在逻辑层使用
    /// </summary>
    /// <value></value>
    public Vector3 entityForward
    {
        get
        {
            if (hasPosition == false)
            {
                return Vector3.zero;
            }
            return position.rotate.ToQuaternion() * Vector3.forward;
        }
    }
}