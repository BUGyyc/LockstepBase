/*
 * @Author: delevin.ying 
 * @Date: 2022-11-03 16:56:16 
 * @Last Modified by: delevin.ying
 * @Last Modified time: 2022-11-17 16:30:26
 */
using Entitas;
using Lockstep;

[Input]
[Game]
public class CharacterInputComponent : IComponent
{
    //public MotionState state;


    /// <summary>
    /// 移动速度
    /// </summary>
    // public LVector3 moveSpeed;

    // public LVector2 inputOriginData;

    public uint entityId;

    public LVector2 moveDir;
    public LVector3 viewDir;

    // public bool leftMousePressed;
    // public bool rightMousePressed;

    /// <summary>
    /// 相机视角
    /// </summary>
    //public LVector3 viewDir;
}

// public sealed partial class InputEntity : Entity
// {
//     public CharacterInputComponent characterInput => (CharacterInputComponent)GetComponent(InputComponentsLookup.CharacterInput);

//     public bool HasCharacterInput => HasComponent(InputComponentsLookup.CharacterInput);


//     public void AddCharacterInputSpeed(LVector3 newValue, LVector2 v2)
//     {
//         int num = InputComponentsLookup.CharacterInput;
//         CharacterInputComponent characterInput = (CharacterInputComponent)CreateComponent(num, typeof(CharacterInputComponent));
//         characterInput.moveSpeed = newValue;
//         characterInput.inputOriginData = v2;
//         AddComponent(num, characterInput);
//     }

//     public void ReplaceCharacterInputSpeed(LVector3 newValue)
//     {
//         int num = InputComponentsLookup.CharacterInput;
//         CharacterInputComponent characterInput = (CharacterInputComponent)CreateComponent(num, typeof(CharacterInputComponent));
//         characterInput.moveSpeed = newValue;
//         ReplaceComponent(num, characterInput);
//     }
// }


public enum MotionState
{
    Idle = 0,
    Walk,
    Run
}

