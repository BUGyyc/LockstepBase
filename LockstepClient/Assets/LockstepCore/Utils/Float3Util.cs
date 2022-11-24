using Protocol;
using UnityEngine;
using System.Collections.Generic;

public static class VectorUtil
{
    // public static Float3 ToFloat3(this Vector3 src)
    // {
    //     return new Float3()
    //     {
    //         X = src.x,
    //         Y = src.y,
    //         Z = src.z
    //     };
    // }

    // public static Vector3 ToVector3(this Float3 src)
    // {
    //     if (src == null) return Vector3.zero;

    //     return new Vector3(src.X, src.Y, src.Z);
    // }

    // public static Int3 ToInt3(this Vector3 src)
    // {
    //     return new Int3()
    //     {
    //         X = (int)(src.x * 1000),
    //         Y = (int)(src.y * 1000),
    //         Z = (int)(src.z * 1000)
    //     };
    // }

    // public static Float3 ToFloat3(this Int3 src)
    // {
    //     return new Float3()
    //     {
    //         X = src.X * 0.001f,
    //         Y = src.Y * 0.001f,
    //         Z = src.Z * 0.001f
    //     };
    // }

    // public static Int3 ToInt3(this Float3 src)
    // {
    //     return new Int3()
    //     {
    //         X = (int)(src.X * 1000),
    //         Y = (int)(src.Y * 1000),
    //         Z = (int)(src.Z * 1000)
    //     };
    // }

    // public static Vector3 ToVector3(this Int3 src)
    // {
    //     return new Vector3(src.X * 0.001f, src.Y * 0.001f, src.Z * 0.001f);
    // }

    // public static Quaternion ToQuaternion(this Float4 src)
    // {
    //     return new Quaternion(src.X, src.Y, src.Z, src.W);
    // }

    // public static Float4 ToFloat4(this Vector4 src)
    // {
    //     return new Float4()
    //     {
    //         X = src.x,
    //         Y = src.y,
    //         Z = src.z,
    //         W = src.w
    //     };
    // }

    // public static List<Vector3> ToVector3List(this IList<Float3> srcList)
    // {
    //     List<Vector3> result = new List<Vector3>();

    //     for (int i = 0; i < srcList.Count; i++)
    //     {
    //         result.Add(srcList[i].ToVector3());
    //     }

    //     return result;
    // }

    public static float AngleXZ(Vector3 beginPoint1, Vector3 endPoint1, Vector3 f2)
    {
        return Vector3.Angle(new Vector3(endPoint1.x - beginPoint1.x, 0, endPoint1.z - beginPoint1.z), new Vector3(f2.x, 0, f2.z));
    }

    public static bool IsEqualXZ(Vector3 pos1, Vector3 pos2)
    {
        return pos1.x == pos2.x && pos1.z == pos2.z;
    }

    public static float SqrMagnitudeXZ(Vector3 f1, Vector3 f2)
    {
        return (f1.x - f2.x) * (f1.x - f2.x) + (f1.z - f2.z) * (f1.z - f2.z);
    }

    // public static Keyframe ToKeyframe(this MoveKeyFrame mKeyframe)
    // {
    //     return new Keyframe(mKeyframe.Time, mKeyframe.Value, mKeyframe.InTangent, mKeyframe.OutTangent, mKeyframe.InWeight, mKeyframe.OutWeight);
    // }

    // public static MoveKeyFrame ToMoveKeyFrame(this Keyframe keyframe, float scale = 1, float speed = 1)
    // {
    //     return new MoveKeyFrame()
    //     {
    //         Time = keyframe.time / speed,
    //         Value = keyframe.value * scale,
    //         InTangent = keyframe.inTangent,
    //         OutTangent = keyframe.outTangent,
    //         InWeight = keyframe.inWeight,
    //         OutWeight = keyframe.outWeight,
    //     };
    // }
}
