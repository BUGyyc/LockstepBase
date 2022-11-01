using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CharacterCameraController : MonoBehaviour
{
    /// <summary>
    /// 旋转相机的缩放、降低灵敏度
    /// </summary>
    public const float YawRotateScaleValue = 0.05f;
    public const float PitchRotateScaleValue = 0.03f;

    public static CharacterCameraController Instance;

    public float Yaw
    {
        private set
        {
            if (value > 360)
            {
                value -= 360;
            }
            if (value < -360)
            {
                value += 360;
            }
            //var val = value % 360;
            //mYaw = val;

            //var val = value % 360;
            mYaw = value;
        }
        get
        {
            return mYaw;
        }
    }
    public float Pitch
    {
        private set
        {
            if (value > 180)
            {
                value -= 360;
            }
            else if (value <= -180)
            {
                value += 360;
            }
            mPitch = Mathf.Clamp(value, -30, 70);
        }
        get
        {
            return mPitch;
        }
    }
    [HideInInspector]
    public Transform targetTf;

    private const float MAX_LEN = 20f;
    private const float MIN_LEN = 0.1f;

    private float mYaw;
    private float mPitch;

    private Quaternion mRotation;
    private Transform cameraTf;
    private Transform mViewDummyTran;

    private Transform _virtualParentRoot;

    private void Awake()
    {
        Instance = this;
    }

    public void InitCharacterCamera(Transform virtualParentRoot)
    {
        _virtualParentRoot = virtualParentRoot;
        var virtualCameraList = new GameObject("VirtualCameraList");
        virtualCameraList.transform.SetParent(virtualParentRoot);
        virtualCameraList.transform.localPosition = Vector3.zero;
        virtualCameraList.transform.localEulerAngles = Vector3.zero;

        var virtualCamera = new GameObject("VirtualCamera");
        virtualCamera.transform.SetParent(virtualCameraList.transform);
        virtualCamera.transform.localPosition = GlobalSetting.CameraInitPos;
        virtualCamera.transform.localRotation = GlobalSetting.CameraInitQ;
        virtualCamera.AddComponent<CinemachineVirtualCamera>();

        mViewDummyTran = virtualCameraList.transform;
        cameraTf = Camera.main.transform;

        Pitch = Camera.main.transform.rotation.eulerAngles.x;
        Yaw = Camera.main.transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        UpdateView();
    }

    public void UpdateView()
    {
        if (mViewDummyTran == null) return;


        if (HasWallBlock(out var point, out var dir))
        {
            mViewDummyTran.position = point - 0.3f * dir;
        }

        //if (HasLookAtTf())
        //{
        //    var direction = targetTf.position - this.logic.position;
        //    mRotation = Quaternion.LookRotation(direction, Vector3.up);
        //}

        mViewDummyTran.rotation = Quaternion.Euler(Pitch, Yaw, 0);
    }

    //public bool HasLookAtTf()
    //{
    //    return this.logic.character.HasLookAt && targetTf != null;
    //}

    public bool HasWallBlock(out Vector3 point, out Vector3 dir)
    {
        point = default;
        dir = cameraTf.position - _virtualParentRoot.position;
        var max = Vector3.Distance(cameraTf.position, _virtualParentRoot.position);
        RaycastHit[] hits = Physics.RaycastAll(_virtualParentRoot.position, dir, max, LayerMaskUtil.LAYER_WALL_MASK | LayerMaskUtil.LAYER_DEFAULT_MASK);
        bool has = false;
        if (hits.Length > 0)
        {
            var minLen = float.MaxValue;
            foreach (var item in hits)
            {
                if (item.distance < minLen)
                {
                    minLen = item.distance;
                    point = item.point;
                    has = true;
                }
            }
        }
        return has;
    }

    public void UpdateCameraDir(Vector2 deltaViewDir)
    {
        Pitch += deltaViewDir.y * PitchRotateScaleValue;
        Yaw += deltaViewDir.x * YawRotateScaleValue;
    }

    private bool CheckWall()
    {
        return false;
    }
}