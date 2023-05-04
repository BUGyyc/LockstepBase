using Entitas;
using Lockstep;

[Game]
public class SkillComponent : IComponent
{
    public uint skillId;

    public bool shootSkill;

    public LVector3 shootDir;

    //public uint lastStartTick;
}