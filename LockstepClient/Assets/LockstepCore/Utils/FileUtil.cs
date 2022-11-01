using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class FileUtil
{
    public static void GetDir(string dirPath, string exName, ref List<string> dirs)
    {
        foreach (string path in Directory.GetFiles(dirPath))
        {
            //获取所有文件夹中包含后缀为 .prefab 的路径
            if (System.IO.Path.GetExtension(path) == exName)
            {
                var aniPath = path.Substring(path.IndexOf("Assets"));
                if (dirs.Contains(aniPath) == false)
                {
                    dirs.Add(aniPath);
                }
            }
        }

        if (Directory.GetDirectories(dirPath).Length > 0)  //遍历所有文件夹
        {
            foreach (string path in Directory.GetDirectories(dirPath))
            {
                GetDir(path, exName, ref dirs);
            }
        }
    }

#if UNITY_EDITOR
    public static bool HasFile(string fullPath)
    {
        var b = System.IO.File.Exists(fullPath);
        if (b) Debug.LogError($"已存在文件 {fullPath} ");
        return b;
    }
#endif

#if UNITY_EDITOR
    public static bool GenerateEnum(List<string> list)
    {
        var arg = "\t 空 = 0,\n";
        foreach (var tag in list)
        {
            int val = Animator.StringToHash(tag);
            arg += "\t" + tag + " = " + val + ",\n";
        }
        var res = "public enum AnimationStateTag\n{\n" + arg + "}\n";
        var path = Application.dataPath + "/GameCore/Battle/animation/AnimationStateTag.cs";
        File.WriteAllText(path, res, Encoding.UTF8);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return false;
    }

#endif

#if UNITY_EDITOR
    public static byte[] LoadFileOnEditor(string bytesName)
    {
        string path = string.Format("{0}/{1}/{2}.bytes", UnityEngine.Application.streamingAssetsPath, "Animators", bytesName);
        return LoadFile(path);
    }
#endif


    public static byte[] LoadFile(string path)
    {
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            BinaryReader reader = new BinaryReader(fs);

            int numBytesToRead = (int)fs.Length;

            byte[] bytes = reader.ReadBytes(numBytesToRead);

            reader.Close();
            fs.Close();

            return bytes;

        }
        catch (FileNotFoundException ioEx)
        {
            Debug.LogError(ioEx.Message);
        }

        return null;
    }

    public static bool SaveFile(byte[] bytes, string path)
    {
        FileStream fs = new FileStream(path, FileMode.Create);

        try
        {
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(bytes);

            fs.Close();
            fs.Dispose();
        }
        catch (IOException e)
        {

            Debug.LogError(e.Message);

            return false;
        }

        return true;
    }

    //是否处在视野之中
    public static bool isInEye(Matrix4x4 vp, Vector3 position, Vector3 cameraPos, float bias = 2.2f)
    {
        Vector4 homogeneous = position;                 //计算投影之后的齐次坐标
        homogeneous.w = 1;
        homogeneous = vp * homogeneous;
        homogeneous.x = homogeneous.x / homogeneous.w;
        homogeneous.y = homogeneous.y / homogeneous.w;
        homogeneous.z = homogeneous.z / homogeneous.w - 1f;

        if (Mathf.Abs(homogeneous.x) < 1.05f && Mathf.Abs(homogeneous.y) < 1.05f && homogeneous.z > 0)
        {
            Vector3 dir = position - cameraPos;

            float distance = dir.magnitude;

            dir.Normalize();

            Ray ray = new Ray(position, -dir);

            RaycastHit hitInfo;

            if (!Physics.Raycast(ray, out hitInfo, distance, LayerMaskUtil.LAYER_WALL_MASK) || (cameraPos - hitInfo.point).sqrMagnitude < bias * bias)
            {
                return true;
            }
        }

        return false;
    }

    public static void SendTips(uint entityId, string errorMsg)
    {
#if OASIS_SERVER
            var msg = new ErrorTipsMsg();
            var connectionId = ServerRoomManager.instance.GetConnectionByPlayerEntityId(entityId);

            msg.EntityId = entityId;
            msg.ErrorTips = errorMsg;
            NetManager.instance.Send(ProtocolID.PidErrorTipsRsp, msg, new List<int> { connectionId });
#endif
    }

    public static float RangeAngleTo360(float rawAngle)
    {
        if (rawAngle < 0) rawAngle = 360 + rawAngle;
        else if (rawAngle > 360) rawAngle -= 360;
        return rawAngle;
    }

    public static float RangeAngleTo180(float rawAngle)
    {
        float tmpAngle = RangeAngleTo360(rawAngle);
        if (tmpAngle > 180)
            tmpAngle = 360 - tmpAngle;
        tmpAngle = Mathf.Abs(tmpAngle);
        return tmpAngle;
    }

    /// <summary>
    /// 获取匹配的角度值
    /// </summary>
    /// <returns></returns>
    public static int GetMatchDegree(float source)
    {
        var index = source / 45;
        var result = (int)index * 45;
        var mid = Mathf.Abs(source % 45) > 22.5f ? 45 : 0;

        result += source > 0 ? mid : -mid;
        return result / 45;
    }

    public static float GetDistanceOfPlane(Vector3 pointOfPlane, Vector3 normal, Vector3 point)
    {
        return Vector3.Dot(point - pointOfPlane, normal);
    }
}
