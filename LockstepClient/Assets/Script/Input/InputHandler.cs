using UnityEngine;
using UnityEngine.InputSystem;
using Entitas;
using Lockstep;
public class InputHandler : MonoBehaviour, PlayerInput.IPlayerActions, PlayerInput.IUIActions
{
    private PlayerInput playerInput;

    public void OnCancel(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
        //Debug.Log();

        var look = context.ReadValue<Vector2>();
        CharacterCameraController.Instance.UpdateCameraDir(look);
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();

        var move = context.ReadValue<Vector2>();

        var v3 = new Vector3(move.x, 0, move.y);

        Debug.Log($"<color=yello>  move {move}   </color>");



        //这里应当传入本地EntityID
        ActionWorld.Instance.Execute(new CharacterInputCommand
        {
            moveSpeed = v3.ToLVector3(),
            entityId = 0
        });

    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void Init()
    {
        playerInput = new PlayerInput();
        playerInput.Player.SetCallbacks(this);
        playerInput.Enable();
    }



    // Start is called before the first frame update





}
