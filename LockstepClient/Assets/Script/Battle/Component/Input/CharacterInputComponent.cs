/*
 * @Author: delevin.ying 
 * @Date: 2022-11-03 16:56:16 
 * @Last Modified by:   delevin.ying 
 * @Last Modified time: 2022-11-03 16:56:16 
 */
using Entitas;
using Lockstep;

[Input]
public class CharacterInputComponent : IComponent
{
    //public MotionState state;


    /// <summary>
    /// 移动速度
    /// </summary>
    public LVector3 moveSpeed;

    public uint entityId;

    /// <summary>
    /// 相机视角
    /// </summary>
    //public LVector3 viewDir;
}

public sealed partial class InputEntity : Entity
{
    public CharacterInputComponent characterInput => (CharacterInputComponent)GetComponent(InputComponentsLookup.CharacterInput);

    public bool HasCharacterInput => HasComponent(InputComponentsLookup.CharacterInput);


    public void AddCharacterInputSpeed(LVector3 newValue)
    {
        int num = InputComponentsLookup.CharacterInput;
        CharacterInputComponent characterInput = (CharacterInputComponent)CreateComponent(num, typeof(CharacterInputComponent));
        characterInput.moveSpeed = newValue;
        AddComponent(num, characterInput);
    }

    public void ReplaceCharacterInputSpeed(LVector3 newValue)
    {
        int num = InputComponentsLookup.CharacterInput;
        CharacterInputComponent characterInput = (CharacterInputComponent)CreateComponent(num, typeof(CharacterInputComponent));
        characterInput.moveSpeed = newValue;
        ReplaceComponent(num, characterInput);
    }
}


public enum MotionState
{
    Idle = 0,
    Walk,
    Run
}

