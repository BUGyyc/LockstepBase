using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CameraViewMode mode;

    public GameObject lookAtObj;

    private Transform lookAtTf;
    private Transform cameraTf;
    private void Start()
    {
        cameraTf = Camera.main.transform;

        if (lookAtObj != null)
        {
            SetLookAt(lookAtObj.transform);
        }
    }

    public void ChangeViewMode(CameraViewMode val)
    {
        mode = val;
    }

    public void SetLookAt(Transform target)
    {
        if (target == null) return;

        lookAtTf = target;

        mode = CameraViewMode.LookAt;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraTf == null) return;

        switch (mode)
        {
            case CameraViewMode.Default:
            default:
                break;
            case CameraViewMode.LookAt:
                if (lookAtTf == null) return;

                Quaternion q = Quaternion.Euler(new Vector3(75, 0, 0));

                var pos = lookAtTf.transform.position + q * Vector3.back * 8f;

                cameraTf.transform.rotation = q;

                cameraTf.transform.position = Vector3.Lerp(cameraTf.transform.position, pos, Time.deltaTime * 2f);
                break;
        }
    }
}

public enum CameraViewMode
{
    Default = 0,
    LookAt,
    Random,

}
