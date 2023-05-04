/*
 * @Author: delevin.ying 
 * @Date: 2022-11-11 17:31:49 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-17 16:38:15
 */

using Entitas;
using Lockstep;

[Input]
public class SkillInputComponent : IComponent
{
    public uint skillId;
    public uint entityId;

    public bool leftMousePressed;
    public bool rightMousePressed;

    public LVector3 shootDir;

}
