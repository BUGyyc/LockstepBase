using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using Protocol;


public partial class MoveBlendTreeTool : EditorWindow
{
    public static readonly string PATH = Application.streamingAssetsPath + "/MoveCtrl/";

    public static readonly string[] FREE_MOVE_DIR_STATES = { "Move", "BowMove", "CrouchMove" };

    public static readonly string[] FUNCTION_TYPE = { "通用配置", "移动配置" };

    public const string DEFAULT_STATE = "Move";

    public const float FRAME_TIME = 0.033f;

    [MenuItem("项目工具/资源导出/动画导出")]
    public static void OpenLevelConfigWindow()
    {
        MoveBlendTreeTool lvcWin = EditorWindow.GetWindow<MoveBlendTreeTool>("动画导出工具");
        lvcWin.Show();
    }

    private static string mCfgName;
    private static AnimatorController mAC;

    private static Vector2 mScrollPostion;
    private static bool mExportBound;
    private static bool mExportWeapon;
    private static string weaponPointPath;
    private static List<string> boundPointPath;

    private static bool mExportHead;
    private static string headPointPath;
    public static float headSize;

    private static int mSelectTool;

    private static string rootNode = "Main";


    private static Dictionary<string, List<BlendNode>> addMotionList;

    private static string[] blenderNames;

    private static int selectIndex;

    MoveControllerCfg mLastBlender;

    private void OnGUI()
    {
        var ac = EditorGUILayout.ObjectField("动画控制器:", mAC, typeof(AnimatorController), false) as AnimatorController;

        if (!ac) mSelectTool = 0;

        if (mAC != ac)
        {
            if (ac != null) mCfgName = ac.name;
            mAC = ac;

            mExportHead = false;
            mExportBound = false;
            mExportWeapon = false;

            var has = FileUtil.HasFile(PATH + mCfgName + ".bytes");

            byte[] exsitConfig = default;



            if (has)
            {
                //exsitConfig = FileUtil.LoadFile(PATH + mCfgName + ".bytes");
                //mLastBlender = MoveControllerCfg.ParseFrom(exsitConfig);
                ////MoveControllerCfg.Parser.ParseFrom(exsitConfig);

                //addMotionList = new Dictionary<string, List<BlendNode>>();

                //foreach (var item in mLastBlender.ExportBlenders)
                //{
                //    if (item.BlendType == BlendType.Cartesian)
                //    {
                //        var nodes = new List<BlendNode>();

                //        for (int i = 0; i < item.NodeList.Count; i++)
                //        {
                //            if (item.NodeList[i].IsAddtion) nodes.Add(item.NodeList[i]);
                //        }

                //        addMotionList[item.Name] = nodes;
                //    }
                //}

                //InitBlenderName();

                //if (mLastBlender.HasWeaponPath && !string.IsNullOrEmpty(mLastBlender.WeaponPath))
                //{
                //    weaponPointPath = mLastBlender.WeaponPath;

                //    mExportWeapon = true;
                //}

                //if (mLastBlender.HasHeadPath && !string.IsNullOrEmpty(mLastBlender.HeadPath))
                //{
                //    headPointPath = mLastBlender.HeadPath;
                //    mExportHead = true;
                //}

                //if (mLastBlender.HasHeadSize)
                //{
                //    headSize = mLastBlender.HeadSize;
                //}

                //if (mLastBlender.BoundPathList.Count > 0)
                //{
                //    boundPointPath = new List<string>();

                //    for (int i = 0; i < mLastBlender.BoundPathList.Count; i++)
                //    {
                //        boundPointPath.Add(mLastBlender.BoundPathList[i]);
                //    }

                //    mExportBound = true;
                //}
            }
            else
            {
                addMotionList = new Dictionary<string, List<BlendNode>>();

                var allState = ac.layers[0].stateMachine.states;

                foreach (var item in allState)
                {
                    if (item.state.motion is BlendTree)
                    {
                        var blender = item.state.motion as BlendTree;

                        if (blender.blendType != BlendTreeType.Simple1D)
                        {
                            addMotionList.Add(item.state.name, new List<BlendNode>());
                        }
                    }
                }

                InitBlenderName();

                mLastBlender = null;
            }
        }

        EditorGUI.BeginDisabledGroup(!ac);

        mSelectTool = GUILayout.Toolbar(mSelectTool, FUNCTION_TYPE);

        switch (mSelectTool)
        {
            case 0:
                DrawCommonUI(ac);
                break;
            case 1:
                DrawMoveBlenderUI(ac);
                break;
            default:
                break;
        }

        EditorGUI.EndDisabledGroup();
    }

    private static bool IsFreeMoveDirState(string state)
    {
        foreach (var freeState in FREE_MOVE_DIR_STATES)
        {
            if (state == freeState)
            {
                return true;
            }
        }

        return false;
    }



    public static void ShowSpeed(Blender blender)
    {
        for (int j = 0; j < blender.NodeList.Count; j++)
        {
            var node = blender.NodeList[j];

            Debug.Log("移动速度：" + node.MoveSpeed + " 转向速度：" + node.AngleSpeed + " " + new Vector2(node.XPostion, node.YPostion));
        }
    }

    public static void ExportAllAnim(AnimatorController ac, MoveControllerCfg lastCfg)
    {
        if (string.IsNullOrEmpty(mCfgName))
        {
            mCfgName = ac.name;
        }

        List<AnimatorState> exportActions = new List<AnimatorState>();

        var allState = ac.layers[0].stateMachine.states;

        Dictionary<string, bool> alreadyHasStates = new Dictionary<string, bool>();

        if (lastCfg != null)
        {
            foreach (var item in lastCfg.ExportBlenders)
            {
                if (addMotionList.TryGetValue(item.Name, out var overrideNodes))
                {
                    foreach (var node in overrideNodes)
                    {
                        var addNode = node;

                        for (int j = 0; j < item.NodeList.Count; j++)
                        {
                            var nowNode = item.NodeList[j];

                            if (addNode.XPostion == nowNode.XPostion && addNode.YPostion == nowNode.YPostion)
                            {
                                //复制信息
                                if (nowNode.HasHeadPosition) addNode.HeadPosition = nowNode.HeadPosition;
                                if (nowNode.HasHeadRotation) addNode.HeadRotation = nowNode.HeadRotation;
                                if (nowNode.HasBoundCenter) addNode.BoundCenter = nowNode.BoundCenter;
                                if (nowNode.HasBoundSize) addNode.BoundSize = nowNode.BoundSize;

                                //item.NodeList[j] = addNode;
                                item.SetNodeList(j, addNode);
                                addNode = null;
                                break;
                            }
                        }

                        if (addNode != null) item.NodeList.Add(addNode);
                    }
                }

                if (alreadyHasStates.ContainsKey(item.Name)) continue;

                alreadyHasStates.Add(item.Name, false);
            }

            foreach (var item in lastCfg.ExportMotions)
            {
                if (alreadyHasStates.ContainsKey(item.MotionName)) continue;

                alreadyHasStates.Add(item.MotionName, false);
            }
        }

        for (int i = 0; i < allState.Length; i++)
        {
            var state = allState[i].state;

            if (alreadyHasStates.ContainsKey(state.name)) continue;

            if (state.motion is BlendTree)
            {
                exportActions.Add(state);
            }
            else if (state.motion is AnimationClip)
            {
                exportActions.Add(state);
            }

        }

        Export(exportActions, lastCfg);
    }

    private void OnEnable()
    {
        if (addMotionList == null)
            addMotionList = new Dictionary<string, List<BlendNode>>();
    }

    //创建
    private static void InitBlenderName()
    {
        blenderNames = new string[addMotionList.Count];

        int index = 0;

        selectIndex = 0;

        foreach (var item in addMotionList.Keys)
        {
            blenderNames[index] = item;

            if (item == DEFAULT_STATE) selectIndex = index;

            index++;
        }
    }

    private static Blender GetBlender(AnimatorState state, bool hasAngle, bool exportBound)
    {
        var tree = state.motion as BlendTree;

        if (tree != null)
        {
            var nodes = tree.children;

            var blender = new Blender();

            blender.Name = state.name;
            blender.BlendType = BlendType.Cartesian;
            blender.IsFixAngle = !hasAngle;

            for (int i = 0; i < nodes.Length; i++)
            {
                var motion = nodes[i].motion as AnimationClip;
                var bindings = AnimationUtility.GetCurveBindings(motion);

                var blendNode = new BlendNode();

                blendNode.XPostion = nodes[i].position.x;
                blendNode.YPostion = nodes[i].position.y;

                blendNode.Length = motion.length / nodes[i].timeScale;
                blendNode.MoveDir = Vector3.forward.ToFloat3();

                for (int n = 0; n < bindings.Length; n++)
                {
                    if (bindings[n].path == "")
                    {
                        if (bindings[n].propertyName == "RootT.x")
                        {
                            Vector3 nowPos = Vector3.zero;

                            var curveX = AnimationUtility.GetEditorCurve(motion, bindings[n]);
                            var curveZ = AnimationUtility.GetEditorCurve(motion, bindings[n + 2]);

                            float distance = 0;
                            float curTime = 0;

                            var setting = AnimationUtility.GetAnimationClipSettings(motion);

                            if (!setting.loopBlendPositionXZ)
                            {
                                Vector3 start = new Vector3(curveX.Evaluate(0), 0, curveZ.Evaluate(0));
                                Vector3 end = new Vector3(curveX.Evaluate(motion.length), 0, curveZ.Evaluate(motion.length));

                                //30帧的速度采样
                                do
                                {
                                    Vector3 target = new Vector3(curveX.Evaluate(curTime), 0, curveZ.Evaluate(curTime));

                                    distance += (nowPos - target).magnitude;

                                    nowPos = target;

                                    curTime += FRAME_TIME;
                                } while (curTime < motion.length);

                                distance += (nowPos - end).magnitude;

                                var lineDistance = (end - start).magnitude;

                                if (lineDistance > distance)
                                {
                                    distance = lineDistance;

                                    //OasisDebug.LogError(nodes[i].position);
                                }

                                //实际距离
                                if (!hasAngle)
                                {
                                    blendNode.MoveDir = (end - start).normalized.ToFloat3();
                                }

                                //OasisDebug.LogError(nodes[i].position + " " + distance + " " + (end - start).magnitude);
                            }
                            else
                            {
                                blendNode.MoveDir = Vector3.zero.ToFloat3();
                            }

                            n += 2;

                            blendNode.MoveSpeed = distance / motion.length * nodes[i].timeScale;
                        }
                        else if (bindings[n].propertyName == "RootQ.x")
                        {
                            Quaternion startRotation = Quaternion.identity;
                            Quaternion midRotation = Quaternion.identity;//中间角度防止旋转超过180导致实际结果脱节
                            Quaternion endRotation = Quaternion.identity;

                            var curve = AnimationUtility.GetEditorCurve(motion, bindings[n]);

                            startRotation.x = curve.Evaluate(0);
                            midRotation.x = curve.Evaluate(motion.length / 2);
                            endRotation.x = curve.Evaluate(motion.length);

                            curve = AnimationUtility.GetEditorCurve(motion, bindings[n + 1]);

                            startRotation.y = curve.Evaluate(0);
                            midRotation.y = curve.Evaluate(motion.length / 2);
                            endRotation.y = curve.Evaluate(motion.length);

                            curve = AnimationUtility.GetEditorCurve(motion, bindings[n + 2]);

                            startRotation.z = curve.Evaluate(0);
                            midRotation.z = curve.Evaluate(motion.length / 2);
                            endRotation.z = curve.Evaluate(motion.length);

                            curve = AnimationUtility.GetEditorCurve(motion, bindings[n + 3]);

                            startRotation.w = curve.Evaluate(0);
                            midRotation.w = curve.Evaluate(motion.length / 2);
                            endRotation.w = curve.Evaluate(motion.length);

                            float degree = Quaternion.Angle(startRotation, midRotation) + Quaternion.Angle(midRotation, endRotation);

                            //比较trick的方式来判断
                            degree = nodes[i].position.x > 0 ? degree : -degree;

                            n += 3;

                            blendNode.AngleSpeed = degree / motion.length * nodes[i].timeScale;

                            //OasisDebug.LogError(nodes[i].position + " " + degree + " " + blendNode.AngleSpeed);
                        }
                    }
                }

                if (exportBound && boundPointPath.Count >= 2)
                {
                    AnimationToolUtil.GetAvgBound(boundPointPath.ToArray(), motion, out Vector3 center, out Vector3 size);

                    blendNode.BoundCenter = center.ToFloat3();
                    blendNode.BoundSize = size.ToFloat3();
                }

                if (mExportHead && AnimationToolUtil.GetChildCurves(headPointPath, motion, out var curves))
                {
                    float time = 0;

                    var startPosition = new Vector3(curves[0].Evaluate(time), curves[1].Evaluate(time), curves[2].Evaluate(time));
                    var startRotation = new Quaternion(curves[3].Evaluate(time), curves[4].Evaluate(time), curves[5].Evaluate(time), curves[6].Evaluate(time));

                    time = motion.length;

                    var endPosition = new Vector3(curves[0].Evaluate(time), curves[1].Evaluate(time), curves[2].Evaluate(time));
                    var endRotation = new Quaternion(curves[3].Evaluate(time), curves[4].Evaluate(time), curves[5].Evaluate(time), curves[6].Evaluate(time));

                    blendNode.HeadPosition = Vector3.Lerp(startPosition, endPosition, 0.5f).ToFloat3();
                    blendNode.HeadRotation = Quaternion.Lerp(startRotation, endRotation, 0.5f).eulerAngles.ToFloat3();
                }

                //blender.NodeList.Add(blendNode);

                blender.AddNodeList(blendNode);

                /*if (!hasAngle)
                {
                    Debug.LogErrorFormat("({0},{1}) {2} {3} {4}", blendNode.XPostion, blendNode.YPostion, blendNode.MoveDir.ToVector3(), blendNode.AngleSpeed, blendNode.MoveSpeed);
                }*/
            }

            return blender;
        }

        return null;
    }

    //带曲线的blender 
    private static Blender GetMotionBlender(AnimatorState state)
    {
        var tree = state.motion as BlendTree;

        if (tree != null)
        {
            var nodes = tree.children;

            var blender = new Blender();

            blender.Name = state.name;
            blender.BlendType = BlendType.Simple1D;

            float length = 0;


            //导出混合树中的每一个节点
            for (int i = 0; i < nodes.Length; i++)
            {
                var motion = nodes[i].motion as AnimationClip;

                var blendNode = new BlendNode();

                blendNode.XPostion = nodes[i].threshold;
                blendNode.YPostion = 0;

                //时长
                blendNode.Length = motion.length / nodes[i].timeScale;
                blendNode.MoveDir = Vector3.forward.ToFloat3();
                blendNode.MoveSpeed = blendNode.AngleSpeed = 0;

                blendNode.FrameCount = (int)(blendNode.Length / AnimationToolUtil.FRAME_TIME);

                AnimationToolUtil.GetCurve("", motion, out var curves, true);

                int frameCount = (int)(blendNode.Length / AnimationToolUtil.FRAME_TIME);

                blendNode.Motion = AnimationToolUtil.CreateMotion(motion.name, nodes[i].timeScale, motion.length, motion.isLooping, curves, frame: frameCount);

                blender.NodeList.Add(blendNode);

                if (mExportBound && boundPointPath.Count >= 2)
                {
                    AnimationToolUtil.GetAvgBound(boundPointPath.ToArray(), motion, out Vector3 center, out Vector3 size);

                    blendNode.BoundCenter = center.ToFloat3();
                    blendNode.BoundSize = size.ToFloat3();
                }

                if (mExportHead && AnimationToolUtil.GetChildCurves(headPointPath, motion, out curves))
                {
                    float time = 0;

                    var startPosition = new Vector3(curves[0].Evaluate(time), curves[1].Evaluate(time), curves[2].Evaluate(time));
                    var startRotation = new Quaternion(curves[3].Evaluate(time), curves[4].Evaluate(time), curves[5].Evaluate(time), curves[6].Evaluate(time));

                    time = motion.length;

                    var endPosition = new Vector3(curves[0].Evaluate(time), curves[1].Evaluate(time), curves[2].Evaluate(time));
                    var endRotation = new Quaternion(curves[3].Evaluate(time), curves[4].Evaluate(time), curves[5].Evaluate(time), curves[6].Evaluate(time));

                    blendNode.HeadPosition = ((startPosition + endPosition) * 0.5f).ToFloat3();
                    blendNode.HeadRotation = Quaternion.Lerp(startRotation, endRotation, 0.5f).eulerAngles.ToFloat3();
                }


                //用子节点中最大节点对应上
                length = Mathf.Max(length, blendNode.Length);
            }

            blender.Length = length;

            return blender;
        }

        return null;
    }

    //如果要添加更多动画 blender在此拓展
    private static void Export(List<AnimatorState> animatorStates, MoveControllerCfg cfg)
    {
        if (cfg == null) cfg = new MoveControllerCfg();

        var animationExportStr = "导出动画：";

        for (int i = 0; i < animatorStates.Count; i++)
        {
            animationExportStr += animatorStates[i].name + " ";

            EditorUtility.DisplayProgressBar("导出动画中", "正在导出:" + animatorStates[i].name, i * 1f / animatorStates.Count);

            var state = animatorStates[i];

            if (state.motion is BlendTree)
            {
                var tree = state.motion as BlendTree;

                if (tree.blendType == BlendTreeType.Simple1D)
                {
                    cfg.ExportBlenders.Add(GetMotionBlender(state));
                }
                else
                {
                    var blender = GetBlender(state, IsFreeMoveDirState(state.name), mExportBound);

                    //用配置的节点覆盖动画节点
                    if (addMotionList.TryGetValue(state.name, out var overrideNodes))
                    {
                        foreach (var item in overrideNodes)
                        {
                            var addNode = item;

                            for (int j = 0; j < blender.NodeList.Count; j++)
                            {
                                var nowNode = blender.NodeList[j];

                                if (addNode.XPostion == nowNode.XPostion && addNode.YPostion == nowNode.YPostion)
                                {
                                    //复制信息
                                    if (nowNode.HasHeadPosition) addNode.HeadPosition = nowNode.HeadPosition;
                                    if (nowNode.HasHeadRotation) addNode.HeadRotation = nowNode.HeadRotation;
                                    if (nowNode.HasBoundCenter) addNode.BoundCenter = nowNode.BoundCenter;
                                    if (nowNode.HasBoundSize) addNode.BoundSize = nowNode.BoundSize;

                                    //blender.NodeList[j] = addNode;
                                    blender.SetNodeList(j, addNode);

                                    addNode = null;
                                    break;
                                }
                            }

                            if (addNode != null) blender.NodeList.Add(addNode);
                        }
                    }

                    Debug.Log($"Blender   <color=red> Create  blender</color> {blender}");
                    cfg.AddExportBlenders(blender);
                }
            }
            else
            {
                var motion = animatorStates[i].motion as AnimationClip;

                int frameCount = (int)(motion.length / FRAME_TIME);

                AnimationToolUtil.GetCurve("", motion, out var curves, true);

                //cfg.ExportMotions.Add(AnimtionToolUtil.CreateMotion(animatorStates[i].name, animatorStates[i].speed, motion.length, motion.isLooping, curves));
                var val = AnimationToolUtil.CreateMotion(animatorStates[i].name, animatorStates[i].speed, motion.length, motion.isLooping, curves, frame: frameCount);

                Debug.LogFormat("CreateMotion MotionName:{0} <color=yellow> frameCount {1} </color>", val.MotionName, frameCount);

                Debug.LogFormat("CreateMotion mExportBound:{0}  ", mExportBound);

                if (boundPointPath != null) Debug.LogFormat("CreateMotion boundPointPath:{0} ", boundPointPath.Count);

                cfg.AddExportMotions(val);
                //cfg.ExportMotions.Add(val);

                if (mExportBound && boundPointPath.Count >= 2)
                {
                    //cfg.ExportMotions.Add(AnimtionToolUtil.CreateBoundMotion(boundPointPath.ToArray(), animatorStates[i].name, animatorStates[i].speed, motion));
                    cfg.AddExportMotions(AnimationToolUtil.CreateBoundMotion(boundPointPath.ToArray(), animatorStates[i].name, animatorStates[i].speed, motion));
                }

                if (mExportWeapon)
                {
                    //cfg.ExportMotions.Add(AnimtionToolUtil.CreateChildMotion(weaponPointPath, animatorStates[i].name, animatorStates[i].speed, motion, MoveBlender.MOTION_TYPE_WEAPON));
                    cfg.AddExportMotions(AnimationToolUtil.CreateChildMotion(weaponPointPath, animatorStates[i].name, animatorStates[i].speed, motion, 1/*MoveBlender.MOTION_TYPE_WEAPON*/));
                }

                if (mExportHead)
                {
                    //cfg.ExportMotions.Add(AnimtionToolUtil.CreateChildMotion(headPointPath, animatorStates[i].name, animatorStates[i].speed, motion, MoveBlender.MOTION_TYPE_HEAD));
                    cfg.AddExportMotions(AnimationToolUtil.CreateChildMotion(headPointPath, animatorStates[i].name, animatorStates[i].speed, motion, 3/*MoveBlender.MOTION_TYPE_HEAD*/));
                }
            }

        }

        EditorUtility.ClearProgressBar();

        if (mExportBound && boundPointPath.Count >= 2)
        {
            cfg.BoundPathList.Clear();
            //cfg.BoundPathList.AddRage(boundPointPath);
            cfg.AddRangeBoundPathList(boundPointPath);
        }

        if (mExportWeapon)
        {
            cfg.WeaponPath = weaponPointPath;
        }

        if (mExportHead)
        {
            cfg.HeadPath = headPointPath;
            cfg.HeadSize = headSize;
        }

        //if (Toolkit.SaveFile(cfg.ToByteArray(WriteScope.FULL, false), PATH + mCfgName + ".bytes"))
        if (FileUtil.SaveFile(cfg.ToByteArray(), PATH + mCfgName + ".bytes"))
        {
            //MoveControllerCfg.ParseFrom(cfg.ToByteArray(WriteScope.FULL, false));
            MoveControllerCfg.ParseFrom(cfg.ToByteArray());
            Debug.Log(animationExportStr);
            Debug.Log("保存完毕:" + PATH + mCfgName + ".bytes");
        }
    }

    private void drawMotion(BlendNode node)
    {
        node.MoveSpeed = EditorGUILayout.FloatField("移动速度", node.MoveSpeed);
        node.AngleSpeed = EditorGUILayout.FloatField("转向速度", node.AngleSpeed);

        Vector2 postion = EditorGUILayout.Vector2Field("位置", new Vector2(node.XPostion, node.YPostion));

        node.XPostion = postion.x;
        node.YPostion = postion.y;

        GUILayout.BeginHorizontal();

        Vector3 moveDir = EditorGUILayout.Vector3Field("移动方向", node.MoveDir.ToVector3());

        node.MoveDir = moveDir.ToFloat3();
    }
}
