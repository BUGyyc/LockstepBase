/*
 * @Author: delevin.ying 
 * @Date: 2022-11-17 19:55:43 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-17 19:58:22
 */
using Entitas;
using Lockstep;

[Game]
public class AIComponent : IComponent
{
    public int RandomVal;

    public AIAction action;

    public uint actionStartTick;
}

public enum AIAction
{
    Idle = 0,
    Attack = 1,
    RandomRun = 2
}
