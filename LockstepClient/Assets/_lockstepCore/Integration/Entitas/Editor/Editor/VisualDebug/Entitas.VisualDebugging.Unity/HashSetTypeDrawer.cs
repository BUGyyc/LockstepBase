using System;
using System.Collections;
using System.Collections.Generic;
using DesperateDevs.Unity.Editor;
using DesperateDevs.Utils;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class HashSetTypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		if (type.IsGenericType)
		{
			return type.GetGenericTypeDefinition() == typeof(HashSet<>);
		}
		return false;
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		Type type = memberType.GetGenericArguments()[0];
		ArrayList itemsToRemove = new ArrayList();
		ArrayList itemsToAdd = new ArrayList();
		bool flag = !((IEnumerable)value).GetEnumerator().MoveNext();
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (flag)
		{
			EditorGUILayout.LabelField(memberName, "empty", (GUILayoutOption[])(object)new GUILayoutOption[0]);
		}
		else
		{
			EditorGUILayout.LabelField(memberName, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		}
		if (EditorLayout.MiniButton("new " + SerializationTypeExtension.ShortTypeName(SerializationTypeExtension.ToCompilableString(type))) && EntityDrawer.CreateDefault(type, out var defaultValue))
		{
			itemsToAdd.Add(defaultValue);
		}
		EditorGUILayout.EndHorizontal();
		if (!flag)
		{
			EditorGUILayout.Space();
			int indentLevel = EditorGUI.get_indentLevel();
			EditorGUI.set_indentLevel(indentLevel + 1);
			foreach (object item in (IEnumerable)value)
			{
				EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
				EntityDrawer.DrawObjectMember(type, string.Empty, item, target, delegate(object newComponent, object newValue)
				{
					itemsToRemove.Add(item);
					itemsToAdd.Add(newValue);
				});
				if (EditorLayout.MiniButton("-"))
				{
					itemsToRemove.Add(item);
				}
				EditorGUILayout.EndHorizontal();
			}
			EditorGUI.set_indentLevel(indentLevel);
		}
		foreach (object item2 in itemsToRemove)
		{
			memberType.GetMethod("Remove").Invoke(value, new object[1] { item2 });
		}
		foreach (object item3 in itemsToAdd)
		{
			memberType.GetMethod("Add").Invoke(value, new object[1] { item3 });
		}
		return value;
	}
}
