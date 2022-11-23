// using System.Collections.Generic;
// using UnityEditor;
// using UnityEditor.Animations;
// using UnityEngine;
// using Protocol;
// public partial class MoveBlendTreeTool : EditorWindow
// {
//     private void DrawCommonUI(AnimatorController ac)
//     {
//         mCfgName = EditorGUILayout.TextField("配置名称:", mCfgName);

//         if (mExportBound || mExportWeapon || mExportHead)
//         {
//             rootNode = EditorGUILayout.TextField("根节点名称", rootNode);
//         }

//         mExportBound = EditorGUILayout.Toggle("导出受击盒", mExportBound);

//         if (mExportBound)
//         {
//             if (boundPointPath == null)
//             {
//                 boundPointPath = new List<string>();
//             }

//             for (int i = 0; i < boundPointPath.Count; i++)
//             {
//                 GameObject obj = (GameObject)EditorGUILayout.ObjectField("受击盒节点" + i, null, typeof(GameObject), true);

//                 if (obj) boundPointPath[i] = AnimationToolUtil.GetChildPath(obj.transform, rootNode);

//                 boundPointPath[i] = EditorGUILayout.TextField("受击盒节点" + i, boundPointPath[i]);
//             }

//             GUI.color = Color.red;

//             if (boundPointPath.Count > 2 && GUILayout.Button("删除"))
//             {
//                 boundPointPath.RemoveAt(boundPointPath.Count - 1);
//             }

//             GUI.color = Color.green;

//             if (GUILayout.Button("添加"))
//             {
//                 boundPointPath.Add("");
//             }

//             GUI.color = Color.white;

//             //var select = EditorGUILayout.ObjectField
//         }

//         mExportWeapon = EditorGUILayout.Toggle("导出武器挂点", mExportWeapon);

//         if (mExportWeapon)
//         {
//             GameObject obj = (GameObject)EditorGUILayout.ObjectField("武器节点：", null, typeof(GameObject), true);

//             if (obj) weaponPointPath = AnimationToolUtil.GetChildPath(obj.transform, rootNode);

//             weaponPointPath = EditorGUILayout.TextField("武器节点：", weaponPointPath);
//         }

//         mExportHead = EditorGUILayout.Toggle("导出头部", mExportHead);

//         if (mExportHead)
//         {
//             GameObject obj = (GameObject)EditorGUILayout.ObjectField("头部节点：", null, typeof(GameObject), true);

//             if (obj) headPointPath = AnimationToolUtil.GetChildPath(obj.transform, rootNode);

//             headPointPath = EditorGUILayout.TextField("头部节点：", headPointPath);

//             headSize = EditorGUILayout.FloatField("头部半径:", headSize);
//         }

//         GUI.color = Color.white;

//         //EditorGUILayout.EndToggleGroup();

//         if (GUILayout.Button("导出全部"))
//         {
//             ExportAllAnim(ac, null);
//         }

//         if (mLastBlender != null)
//         {
//             GUI.color = new Color(0.5f, 1, 0.5f);

//             if (GUILayout.Button("导出增量（包含移动配置改动）"))
//             {
//                 ExportAllAnim(ac, mLastBlender);
//             }

//             GUI.color = Color.white;
//         }

//         GUILayout.Space(5);
//     }

//     public void DrawMoveBlenderUI(AnimatorController ac)
//     {
//         if (blenderNames == null) return;

//         GUI.color = Color.cyan;

//         selectIndex = EditorGUILayout.Popup("选择移动blender:", selectIndex, blenderNames);

//         GUI.color = Color.white;

//         if (selectIndex >= 0 && selectIndex < addMotionList.Count && addMotionList.TryGetValue(blenderNames[selectIndex], out var overrideList))
//         {
//             mScrollPostion = GUILayout.BeginScrollView(mScrollPostion);

//             for (int i = 0; i < overrideList.Count; i++)
//             {
//                 GUI.color = i % 2 == 0 ? new Color(1f, 1f, 1f) : new Color(1.4f, 1.3f, 1f);

//                 drawMotion(overrideList[i]);

//                 GUI.color = Color.red;

//                 if (GUILayout.Button("删除", GUILayout.Width(50), GUILayout.Height(37)))
//                 {
//                     overrideList.RemoveAt(i);
//                 }

//                 GUILayout.EndHorizontal();

//                 GUILayout.Space(10);
//             }

//             GUILayout.EndScrollView();

//             GUILayout.BeginHorizontal();

//             GUI.color = Color.green;

//             if (GUILayout.Button("添加Motion"))
//             {
//                 overrideList.Add(new BlendNode
//                 {
//                     MoveDir = new Float3 { Z = 1 },
//                     IsAddtion = true,
//                     BoundCenter = (Vector3.up * 0.8f).ToFloat3(),
//                     BoundSize = new Float3 { X = 0.6f, Y = 1.6f, Z = 0.6f },
//                     Length = 1
//                 });
//             }

//             GUI.color = Color.white;

//             if (GUILayout.Button("打印速度"))
//             {
//                 foreach (var item in ac.layers[0].stateMachine.states)
//                 {
//                     if (item.state.name == blenderNames[selectIndex])
//                     {
//                         ShowSpeed(GetBlender(item.state, IsFreeMoveDirState(item.state.name), false));
//                         break;
//                     }
//                 }
//             }

//             GUI.color = Color.yellow;

//             if (overrideList.Count == 0 && GUILayout.Button("生成"))
//             {
//                 foreach (var item in ac.layers[0].stateMachine.states)
//                 {
//                     if (item.state.name == blenderNames[selectIndex])
//                     {
//                         var blender = GetBlender(item.state, IsFreeMoveDirState(item.state.name), false);

//                         for (int i = 0; i < blender.NodeList.Count; i++)
//                         {
//                             blender.NodeList[i].IsAddtion = true;

//                             overrideList.Add(blender.NodeList[i]);
//                         }

//                         break;
//                     }
//                 }
//             }

//             GUILayout.EndHorizontal();
//         }
//     }
// }

