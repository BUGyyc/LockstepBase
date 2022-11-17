/*
 * @Author: delevin.ying 
 * @Date: 2022-11-11 17:31:49 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-11 17:45:03
 */

using Entitas;
using Lockstep;

[Input]
public class SkillInputComponent : IComponent
{
    public uint skillId;
    public uint entityId;

}

// public sealed partial class InputEntity : Entity
// {
//     public SkillInputComponent SkillInput => (SkillInputComponent)GetComponent(InputComponentsLookup.SkillInput);

//     public bool HasSkillInput => HasComponent(InputComponentsLookup.SkillInput);

//     public void AddSkillInput(uint skillID, uint entityId)
//     {
//         int num = InputComponentsLookup.SkillInput;
//         SkillInputComponent skillInput = (SkillInputComponent)CreateComponent(num, typeof(SkillInputComponent));
//         skillInput.skillId = skillID;
//         skillInput.entityId = entityId;
//         AddComponent(num, skillInput);
//     }
// }