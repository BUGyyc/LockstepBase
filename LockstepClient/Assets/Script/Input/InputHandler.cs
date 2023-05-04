﻿using UnityEngine;
using UnityEngine.InputSystem;
using Entitas;
using Lockstep;
using UnityEngine.InputSystem.HID;

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

    void Update()
    {
        //playerInput.Player.Look.ReadValue
        //var pos = Mouse.current.w;
        //Debug.DrawRay(pos, Vector3.up, Color.red, 1);
    }

    private bool lastFireState;

    public void OnFire(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
        if (context.performed)
        {

            var origin = InputManager.Instance.localGameEntity.position.value.ToVector3();
            var dir = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = default;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(" 当前鼠标点击物体的名字是————" + hit.collider.name);
                var targetPos = hit.point;
                dir = targetPos - origin;
                dir.y = 0;

            }

            Debug.Log($"<color=yellow>  键盘输入  Click dir：{dir} </color>");

            ActionWorld.Instance.Execute(new SkillInputCommand
            {
                skillId = 1,
                entityId = ActionWorld.Instance.LocalCharacterEntityId,
                leftMousePressed = true,
                shootDir = dir.ToLVector3()
            });
            //lastFireState = true;
        }

        //if (context.canceled && lastFireState)
        //{
        //    lastFireState = false;
        //    ActionWorld.Instance.Execute(new SkillInputCommand
        //    {
        //        skillId = 1,
        //        entityId = ActionWorld.Instance.LocalCharacterEntityId,
        //        leftMousePressed = false,
        //        shootDir = dir.ToLVector3()
        //    });
        //}



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
        // var lv3 = v3.ToLVector3();
        var moveDir = GetMoveDir(v3);
        var viewDir = Camera.main.transform.forward;
        var lv3 = GetTargetDir(moveDir, viewDir);
        var lv2 = new LVector2(lv3.x, lv3.z);
        Debug.Log($"<color=yellow>  键盘输入   key {lv2}   target {lv3}   </color>");

        //这里应当传入本地EntityID
        ActionWorld.Instance.Execute(new CharacterInputCommand
        {
            moveDir = lv2,
            viewDir = viewDir.ToLVector3(),
            entityId = ActionWorld.Instance.LocalCharacterEntityId
        });

    }

    private MoveDir GetMoveDir(Vector3 inputMoveDir)
    {
        MoveDir moveDir = MoveDir.Idle;
        float x = inputMoveDir.x;
        float z = inputMoveDir.z;
        if (x == 0 && z == 0)
        {
            moveDir = MoveDir.Idle;
        }
        else if (x > 0)
        {
            if (z == 0)
            {
                moveDir = MoveDir.Right;
            }
            else if (z > 0)
            {
                moveDir = MoveDir.FR;
            }
            else if (z < 0)
            {
                moveDir = MoveDir.BR;
            }
        }
        else if (x < 0)
        {
            if (z == 0)
            {
                moveDir = MoveDir.Left;
            }
            else if (z > 0)
            {
                moveDir = MoveDir.FL;
            }
            else if (z < 0)
            {
                moveDir = MoveDir.BL;
            }
        }
        else if (x == 0)
        {
            if (z > 0)
            {
                moveDir = MoveDir.Forward;
            }
            else
            {
                moveDir = MoveDir.Back;
            }
        }
        return moveDir;
    }

    private LVector3 GetTargetDir(MoveDir dir, Vector3 forward)
    {
        forward = new Vector3(forward.x, 0, forward.z);
        switch (dir)
        {
            case MoveDir.Idle:
                return LVector3.zero;
            case MoveDir.Forward:
                return forward.ToLVector3();
            case MoveDir.Back:
                return -1 * forward.ToLVector3();
            case MoveDir.Right:
                return (Quaternion.AngleAxis(90, Vector3.up) * forward).ToLVector3();
            case MoveDir.FR:
                return (Quaternion.AngleAxis(45, Vector3.up) * forward).ToLVector3();
            case MoveDir.BR:
                return (Quaternion.AngleAxis(135, Vector3.up) * forward).ToLVector3();
            case MoveDir.Left:
                return (Quaternion.AngleAxis(-90, Vector3.up) * forward).ToLVector3();
            case MoveDir.FL:
                return (Quaternion.AngleAxis(-45, Vector3.up) * forward).ToLVector3();
            case MoveDir.BL:
                return (Quaternion.AngleAxis(-135, Vector3.up) * forward).ToLVector3();
            default:
                return LVector3.zero;
        }
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
