using Entitas;
using Lockstep;

public class AnimationComponent : IComponent
{
    /// <summary>
    /// 所播放的动画的帧号
    /// </summary>
    public uint FrameID;

    /// <summary>
    /// 动画 HashID
    /// </summary>
    public uint AnimationHashID;


    /// <summary>
    /// 动画播放的起始帧号
    /// </summary>
    public uint PlaySinceStartFrameKey;


    /// <summary>
    /// 动画长度
    /// </summary>
    public uint FrameLength;


    /// <summary>
    /// 标记是否当前帧播放
    /// </summary>
    public bool readyPlay;


    public string animationName;


    public LVector3 inputParams;
}



public sealed partial class GameEnttiy : Entity
{
    public AnimationComponent animation => (AnimationComponent)GetComponent(GameComponentsLookup.Animation);

    public bool hasAnimation => HasComponent(GameComponentsLookup.Animation);

}