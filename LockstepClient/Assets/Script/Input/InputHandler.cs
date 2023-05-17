using UnityEngine;
using UnityEngine.InputSystem;
using Entitas;
using Lockstep;
using UnityEngine.InputSystem.HID;
using Lockstep.Game.Commands;
using UnityEngine.UIElements;
using System.Linq;

public class InputHandler : MonoBehaviour, PlayerInput.IPlayerActions, PlayerInput.IUIActions
{
    private PlayerInput playerInput;

    private LVector3 GetWorldPos(Vector3 p, out bool flag)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var lf3 = hit.point.ToLVector3();
            flag = true;
            return lf3;
        }
        else
        {
            flag = false;
            return LVector3.zero;
        }
    }

    private void Update()
    {



        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("state : " + Contexts.sharedInstance.gameState.isPredicting);
            // return;

            var lf3 = GetWorldPos(Input.mousePosition, out bool flag);
            if (flag)
            {
                ActionWorld.Instance.Execute(new SpawnCommand { Position = lf3 });
            }
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit))
            //{
            //    var lf3 = hit.point.ToLVector3();
            //    ActionWorld.Instance.Execute(new SpawnCommand { Position = lf3 });
            //}
        }

        if (Input.GetKeyDown(KeyCode.X))
        {

            var lf3 = GetWorldPos(Input.mousePosition, out bool flag);

            if (flag)
            {
                var e = Contexts.sharedInstance.game
               .GetEntities(GameMatcher.LocalId)
               .Where(entity => entity.actorId.value == ActionWorld.Instance?.Simulation.LocalActorId)
               .Select(entity => entity.id.value)
               .ToArray();

                ActionWorld.Instance?.Execute(
                    new NavigateCommand
                    {
                        Destination = lf3,
                        Selection = e
                    }
                );

            }


        }

#if UNITY_EDITOR


        //注意这种模式的追帧需要限制输入，让追帧的操作是纯净的
        if (Input.GetKeyDown(KeyCode.R))
        {
            var tick = Contexts.sharedInstance.gameState.tick.value;

            uint offset = 600;

            offset = (uint)Mathf.Min(offset, tick);

            // if (tick <= offset)
            //     return;

            var target = tick - offset;

            Debug.LogFormat($"  手动触发回滚  from {tick}  to {target}    ");

            ActionWorld.Instance.Simulation.GetWorld().RevertToTick(target);
        }
#endif
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    private bool lastFireState;

    public void OnFire(InputAction.CallbackContext context)
    {
        // return;

        //throw new System.NotImplementedException();
        if (context.performed)
        {
            // Debug.Log($"<color=yellow>  键盘输入  Click  </color>");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //ActionWorld.Instance.Simulation.GetWorld().

                var lf3 = hit.point.ToLVector3();

                //Debug.Log(" 当前鼠标点击————" + lf3);

                ActionWorld.Instance.Execute(
                    new SkillInputCommand
                    {
                        skillId = 1,
                        entityId = ActionWorld.Instance.LocalCharacterEntityId,
                        shootX = lf3.x,
                        shootY = lf3.y,
                        shootZ = lf3.z,
                        leftMousePressed = true
                    }
                );
                lastFireState = true;
            }
        }

        //if (context.canceled && lastFireState)
        //{
        //    lastFireState = false;
        //    ActionWorld.Instance.Execute(new SkillInputCommand
        //    {
        //        skillId = 1,
        //        entityId = ActionWorld.Instance.LocalCharacterEntityId,
        //        leftMousePressed = false
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
        return;

        if (context.performed == false && context.canceled == false)
            return;

        //throw new System.NotImplementedException();
        var move = context.ReadValue<Vector2>();

        var v3 = new Vector3(move.x, 0, move.y);
        // var lv3 = v3.ToLVector3();
        var moveDir = GetMoveDir(v3);
        var viewDir = Camera.main.transform.forward;
        var lv3 = GetTargetDir(moveDir, viewDir);
        var lv2 = new LVector2(lv3.x, lv3.z);

        // Debug.Log($"<color=yellow>  键盘输入   key: {lv2}   actor: {ActionWorld.Instance.LocalCharacterEntityId}   </color>");

        //这里应当传入本地EntityID
        ActionWorld.Instance.Execute(
            new CharacterInputCommand
            {
                moveDir = lv2,
                viewDir = viewDir.ToLVector3(),
                entityId = ActionWorld.Instance.LocalCharacterEntityId
            }
        );
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
