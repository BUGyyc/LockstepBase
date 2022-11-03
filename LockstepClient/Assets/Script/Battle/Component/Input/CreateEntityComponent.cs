/*
 * @Author: delevin.ying 
 * @Date: 2022-11-03 16:56:10 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-03 17:11:58
 */
using Entitas;
using Lockstep;

[Input]
public class CreateEntityComponent : IComponent
{
    public LVector3 position;
    public LVector3 rotateEuler;

    public uint type;
}