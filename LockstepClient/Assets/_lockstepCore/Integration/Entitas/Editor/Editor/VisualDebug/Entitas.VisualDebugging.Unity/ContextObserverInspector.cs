using System.Linq;
using DesperateDevs.Unity.Editor;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

[CustomEditor(typeof(ContextObserverBehaviour))]
public class ContextObserverInspector : Editor
{
	public override void OnInspectorGUI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		ContextObserver contextObserver = ((ContextObserverBehaviour)((Editor)this).get_target()).get_contextObserver();
		EditorLayout.BeginVerticalBox();
		EditorGUILayout.LabelField(contextObserver.get_context().get_contextInfo().name, EditorStyles.get_boldLabel(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorGUILayout.LabelField("Entities", contextObserver.get_context().get_count().ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorGUILayout.LabelField("Reusable entities", contextObserver.get_context().get_reusableEntitiesCount().ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
		int retainedEntitiesCount = contextObserver.get_context().get_retainedEntitiesCount();
		if (retainedEntitiesCount != 0)
		{
			Color color = GUI.get_color();
			GUI.set_color(Color.get_red());
			EditorGUILayout.LabelField("Retained entities", retainedEntitiesCount.ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			GUI.set_color(color);
			EditorGUILayout.HelpBox("WARNING: There are retained entities.\nDid you call entity.Retain(owner) and forgot to call entity.Release(owner)?", (MessageType)2);
		}
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (GUILayout.Button("Create Entity", (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			IEntity entity = contextObserver.get_context().CreateEntity();
			Selection.set_activeGameObject(((Component)Object.FindObjectsOfType<EntityBehaviour>().Single((EntityBehaviour eb) => eb.get_entity() == entity)).get_gameObject());
		}
		Color backgroundColor = GUI.get_backgroundColor();
		GUI.set_backgroundColor(Color.get_red());
		if (GUILayout.Button("Destroy All Entities", (GUILayoutOption[])(object)new GUILayoutOption[0]))
		{
			contextObserver.get_context().DestroyAllEntities();
		}
		GUI.set_backgroundColor(backgroundColor);
		EditorGUILayout.EndHorizontal();
		EditorLayout.EndVerticalBox();
		IGroup[] groups = contextObserver.get_groups();
		if (groups.Length != 0)
		{
			EditorLayout.BeginVerticalBox();
			EditorGUILayout.LabelField("Groups (" + groups.Length + ")", EditorStyles.get_boldLabel(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
			foreach (IGroup item in groups.OrderByDescending((IGroup g) => g.get_count()))
			{
				EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
				EditorGUILayout.LabelField(((object)item).ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
				EditorGUILayout.LabelField(item.get_count().ToString(), (GUILayoutOption[])(object)new GUILayoutOption[1] { GUILayout.Width(48f) });
				EditorGUILayout.EndHorizontal();
			}
			EditorLayout.EndVerticalBox();
		}
		EditorUtility.SetDirty(((Editor)this).get_target());
	}
}
