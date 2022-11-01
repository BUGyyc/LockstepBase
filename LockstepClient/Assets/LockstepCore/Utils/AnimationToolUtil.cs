using Protocol;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class AnimationToolUtil
{
    public const float FRAME_TIME = 0.033f;

    public const uint EXPORT_POSTION = 1;
    public const uint EXPORT_ROTATION = 1 << 1;
    public const uint EXPORT_SCALE = 1 << 2;

    public static bool GetCurve(string name, AnimationClip motion, out AnimationCurve[] curves, bool isRootMotion, uint exportModel = (EXPORT_POSTION | EXPORT_ROTATION))
    {
        int count;
        if ((exportModel & EXPORT_SCALE) > 0) count = 10;
        else if ((exportModel & EXPORT_ROTATION) > 0) count = 7;
        else if ((exportModel & EXPORT_POSTION) > 0) count = 3;
        else
        {
            curves = null;
            return false;
        }

        Debug.LogFormat("GetCurve   count {0}", count);
        curves = new AnimationCurve[count];

        var bindings = AnimationUtility.GetCurveBindings(motion);
        var events = AnimationUtility.GetAnimationEvents(motion);



        count = 0;

        string posStart = isRootMotion ? "RootT.x" : "m_LocalPosition.x";
        string rotStart = isRootMotion ? "RootQ.x" : "m_LocalRotation.x";

        for (int i = 0; i < bindings.Length; i++)
        {
            if (bindings[i].path == name)
            {
                if ((exportModel & EXPORT_POSTION) > 0 && bindings[i].propertyName == posStart)
                {
                    curves[0] = AnimationUtility.GetEditorCurve(motion, bindings[i]);
                    curves[1] = AnimationUtility.GetEditorCurve(motion, bindings[i + 1]);
                    curves[2] = AnimationUtility.GetEditorCurve(motion, bindings[i + 2]);

                    count += 3;
                }
                else if ((exportModel & EXPORT_ROTATION) > 0 && bindings[i].propertyName == rotStart)
                {
                    curves[3] = AnimationUtility.GetEditorCurve(motion, bindings[i]);
                    curves[4] = AnimationUtility.GetEditorCurve(motion, bindings[i + 1]);
                    curves[5] = AnimationUtility.GetEditorCurve(motion, bindings[i + 2]);
                    curves[6] = AnimationUtility.GetEditorCurve(motion, bindings[i + 3]);

                    count += 4;
                }
                else if ((exportModel & EXPORT_SCALE) > 0 && bindings[i].propertyName == "m_LocalScale.x")
                {
                    curves[7] = AnimationUtility.GetEditorCurve(motion, bindings[i]);
                    curves[8] = AnimationUtility.GetEditorCurve(motion, bindings[i + 1]);
                    curves[9] = AnimationUtility.GetEditorCurve(motion, bindings[i + 2]);

                    count += 3;
                }
            }
        }

        return count == curves.Length;
    }

    public static MoveMotion CreateMotion(string name, float speed, float length, bool isLoop, AnimationCurve[] curves, uint Type = 0 /*MoveBlender.MOTION_TYPE_DEFAULT*/)
    {
        var moveMotion = new MoveMotion
        {
            Length = length / speed,
            MotionName = name,
            IsLoop = isLoop,
            MotionType = Type
        };

        //λ�Ƶ���
        if (curves.Length >= 3)
        {
            InitCurve(curves[0], length, speed, moveMotion.AddPositionXCurve);
            InitCurve(curves[1], length, speed, moveMotion.AddPositionYCurve);
            InitCurve(curves[2], length, speed, moveMotion.AddPositionZCurve);
            //InitCurve(curves[0], length, speed, moveMotion.PositionXCurve.Add);
            //InitCurve(curves[1], length, speed, moveMotion.PositionYCurve.Add);
            //InitCurve(curves[2], length, speed, moveMotion.PositionZCurve.Add);
        }

        //��ת����
        if (curves.Length >= 7)
        {
            InitCurve(curves[3], length, speed, moveMotion.AddRotationXCurve);
            InitCurve(curves[4], length, speed, moveMotion.AddRotationYCurve);
            InitCurve(curves[5], length, speed, moveMotion.AddRotationZCurve);
            InitCurve(curves[6], length, speed, moveMotion.AddRotationWCurve);

            //InitCurve(curves[3], length, speed, moveMotion.RotationXCurve.Add);
            //InitCurve(curves[4], length, speed, moveMotion.RotationYCurve.Add);
            //InitCurve(curves[5], length, speed, moveMotion.RotationZCurve.Add);
            //InitCurve(curves[6], length, speed, moveMotion.RotationWCurve.Add);
        }

        //���ţ�size������
        if (curves.Length >= 10)
        {
            InitCurve(curves[7], length, speed, moveMotion.AddScaleXCurve);
            InitCurve(curves[8], length, speed, moveMotion.AddScaleYCurve);
            InitCurve(curves[9], length, speed, moveMotion.AddScaleZCurve);
        }

        return moveMotion;
    }

    public static void InitCurve(AnimationCurve unityCurve, float length, float speed, Action<MoveKeyFrame> addMethod)
    {
        if (unityCurve == null || unityCurve.keys.Length == 0) return;

        foreach (var item in unityCurve.keys)
        {
            var a = item.ToMoveKeyFrame(1, speed);
            //addMethod(a);
            addMethod(a);
        }
    }

    /// <summary>
    /// ͨ���������ڵ���ƶ���������ڵ�Ķ�������
    /// </summary>
    /// <param name="path"></param>
    /// <param name="bindings"></param>
    /// <param name="motion"></param>
    /// <returns></returns>
    public static MoveMotion CreateChildMotion(string childPath, string name, float speed, AnimationClip motion, uint type)
    {
        if (!GetChildCurves(childPath, motion, out var outCurves))
        {
            //Debug.LogErrorFormat("��ȡchild����ʧ��,clip:{0},path:{1}", motion.name, childPath);
        }

        return CreateMotion(name, speed, motion.length, false, outCurves, type);
    }

    public static bool GetChildCurves(string childPath, AnimationClip motion, out AnimationCurve[] outCurves)
    {
        string[] paths = childPath.Split('/');

        var length = 0;

        outCurves = new AnimationCurve[7];

        float time = 0;

        for (int i = 0; i < outCurves.Length; i++)
        {
            outCurves[i] = new AnimationCurve();
        }

        while (time < motion.length)
        {
            outCurves[0].AddKey(time, 0);
            outCurves[1].AddKey(time, 0);
            outCurves[2].AddKey(time, 0);

            outCurves[3].AddKey(time, Quaternion.identity.x);
            outCurves[4].AddKey(time, Quaternion.identity.y);
            outCurves[5].AddKey(time, Quaternion.identity.z);
            outCurves[6].AddKey(time, Quaternion.identity.w);

            time += FRAME_TIME;
        }

        for (int i = 0; i < paths.Length; i++)
        {
            length += i > 0 ? paths[i].Length + 1 : paths[i].Length;

            //�������ڵ�
            if (i == 0) continue;

            var path = childPath.Substring(0, length);

            if (GetCurve(path, motion, out var curves, false))
            {
                time = 0;

                int index = 0;
                while (time < motion.length)
                {
                    var curPos = new Vector3(outCurves[0].Evaluate(time), outCurves[1].Evaluate(time), outCurves[2].Evaluate(time));
                    var curRot = new Quaternion(outCurves[3].Evaluate(time), outCurves[4].Evaluate(time), outCurves[5].Evaluate(time), outCurves[6].Evaluate(time));

                    var nextPos = new Vector3(curves[0].Evaluate(time), curves[1].Evaluate(time), curves[2].Evaluate(time));
                    var nextRot = new Quaternion(curves[3].Evaluate(time), curves[4].Evaluate(time), curves[5].Evaluate(time), curves[6].Evaluate(time));

                    nextPos = curRot * nextPos;
                    nextPos += curPos;

                    nextRot = curRot * nextRot;

                    outCurves[0].MoveKey(index, new Keyframe(time, nextPos.x));
                    outCurves[1].MoveKey(index, new Keyframe(time, nextPos.y));
                    outCurves[2].MoveKey(index, new Keyframe(time, nextPos.z));

                    outCurves[3].MoveKey(index, new Keyframe(time, nextRot.x));
                    outCurves[4].MoveKey(index, new Keyframe(time, nextRot.y));
                    outCurves[5].MoveKey(index, new Keyframe(time, nextRot.z));
                    outCurves[6].MoveKey(index, new Keyframe(time, nextRot.w));

                    time += FRAME_TIME;

                    index++;
                }
            }
            else
            {
                Debug.LogWarning(motion.name + " " + path);

                continue;
            }
        }

        return true;
    }

    //��ȡ�ܻ��ж���
    public static MoveMotion CreateBoundMotion(string[] childPaths, string name, float speed, AnimationClip motion)
    {
        var animationCurves = new List<AnimationCurve[]>();

        float time;

        for (int k = 0; k < childPaths.Length; k++)
        {
            var childPath = childPaths[k];

            if (GetChildCurves(childPath, motion, out var outCurves))
            {
                animationCurves.Add(outCurves);
            }
            else
            {
                //Debug.LogErrorFormat("��ȡbound����ʧ��,clip:{0},path:{1}", motion.name, childPath);
            }
        }

        var resultCurves = new AnimationCurve[10];

        time = 0;

        for (int i = 0; i < resultCurves.Length; i++)
        {
            resultCurves[i] = new AnimationCurve();
        }

        while (time < motion.length)
        {
            resultCurves[0].AddKey(time, 0);
            resultCurves[1].AddKey(time, 0);
            resultCurves[2].AddKey(time, 0);

            resultCurves[7].AddKey(time, 0);
            resultCurves[8].AddKey(time, 0);
            resultCurves[9].AddKey(time, 0);

            time += FRAME_TIME;
        }

        time = 0;
        int index = 0;

        while (time < motion.length)
        {
            var postionList = new Vector3[animationCurves.Count];

            for (int i = 0; i < animationCurves.Count; i++)
            {
                var item = animationCurves[i];

                postionList[i] = new Vector3(item[0].Evaluate(time), item[1].Evaluate(time), item[2].Evaluate(time));
            }

            GetAABBBound(out Vector3 center, out Vector3 size, postionList);

            resultCurves[0].MoveKey(index, new Keyframe(time, center.x));
            resultCurves[1].MoveKey(index, new Keyframe(time, center.y));
            resultCurves[2].MoveKey(index, new Keyframe(time, center.z));

            resultCurves[7].MoveKey(index, new Keyframe(time, size.x));
            resultCurves[8].MoveKey(index, new Keyframe(time, size.y));
            resultCurves[9].MoveKey(index, new Keyframe(time, size.z));

            time += FRAME_TIME;

            index++;
        }

        return CreateMotion(name, speed, motion.length, false, resultCurves, 2/*MoveBlender.MOTION_TYPE_BOUND*/);
    }

    //��ȡƽ���İ�Χ�У���Move
    public static void GetAvgBound(string[] childPaths, AnimationClip motion, out Vector3 center, out Vector3 size)
    {
        size = Vector3.zero;
        center = Vector3.zero;

        var animationCurves = new List<AnimationCurve[]>();

        for (int k = 0; k < childPaths.Length; k++)
        {
            var childPath = childPaths[k];

            if (GetChildCurves(childPath, motion, out var outCurves))
            {
                animationCurves.Add(outCurves);
            }
            else
            {
                //Debug.LogErrorFormat("��ȡbound����ʧ��,clip:{0},path:{1}", motion.name, childPath);
            }
        }

        float time = 0;
        int index = 0;

        while (time < motion.length)
        {
            var postionList = new Vector3[animationCurves.Count];

            for (int i = 0; i < animationCurves.Count; i++)
            {
                var item = animationCurves[i];

                postionList[i] = new Vector3(item[0].Evaluate(time), item[1].Evaluate(time), item[2].Evaluate(time));
            }

            GetAABBBound(out Vector3 frameCenter, out Vector3 frameSize, postionList);

            center += frameCenter;
            size += frameSize;

            time += FRAME_TIME;

            index++;
        }

        center /= index;
        size /= index;
    }

    /// <summary>
    /// ��ȡ��Χ��
    /// </summary>
    /// <param name="center"></param>
    /// <param name="size"></param>
    /// <param name="points"></param>
    public static void GetAABBBound(out Vector3 center, out Vector3 size, params Vector3[] points)
    {
        float top, bottom, left, right, front, back;

        top = right = front = float.MinValue;
        bottom = left = back = float.MaxValue;

        foreach (var item in points)
        {
            if (item.x > right) right = item.x;
            if (item.x < left) left = item.x;
            if (item.y > top) top = item.y;
            if (item.y < bottom) bottom = item.y;
            if (item.z > front) front = item.z;
            if (item.z < back) back = item.z;
        }

        center = new Vector3((left + right) * 0.5f, (top + bottom) * 0.5f, (front + back) * 0.5f);
        size = new Vector3(right - left, top - bottom, front - back);
    }

    public static string GetChildPath(Transform target, string rootName)
    {
        string str = target.name;

        while (target.parent && target.name != rootName)
        {
            target = target.parent;

            str = target.name + "/" + str;
        }

        return str;
    }
}