using System;
using System.Collections;
using System.Linq;
using DesperateDevs.Unity.Editor;
using DesperateDevs.Utils;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class ListTypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		return type.GetInterfaces().Contains(typeof(IList));
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		IList list = (IList)value;
		Type type = memberType.GetGenericArguments()[0];
		if (list.Count == 0)
		{
			list = drawAddElement(list, memberName, type);
		}
		else
		{
			EditorGUILayout.LabelField(memberName, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		}
		int indentLevel = EditorGUI.get_indentLevel();
		EditorGUI.set_indentLevel(indentLevel + 1);
		Func<IList> func = null;
		for (int i = 0; i < list.Count; i++)
		{
			int localIndex = i;
			EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
			EntityDrawer.DrawObjectMember(type, memberName + "[" + localIndex + "]", list[localIndex], target, delegate(object newComponent, object newValue)
			{
				list[localIndex] = newValue;
			});
			Func<IList> func2 = drawEditActions(list, type, localIndex);
			if (func2 != null)
			{
				func = func2;
			}
			EditorGUILayout.EndHorizontal();
		}
		if (func != null)
		{
			list = func();
		}
		EditorGUI.set_indentLevel(indentLevel);
		return list;
	}

	private static Func<IList> drawEditActions(IList list, Type elementType, int index)
	{
		if (EditorLayout.MiniButtonLeft("↑") && index > 0)
		{
			return delegate
			{
				int index3 = index - 1;
				object value2 = list[index3];
				list[index3] = list[index];
				list[index] = value2;
				return list;
			};
		}
		if (EditorLayout.MiniButtonMid("↓") && index < list.Count - 1)
		{
			return delegate
			{
				int index2 = index + 1;
				object value = list[index2];
				list[index2] = list[index];
				list[index] = value;
				return list;
			};
		}
		if (EditorLayout.MiniButtonMid("+"))
		{
			if (EntityDrawer.CreateDefault(elementType, out var defaultValue))
			{
				int insertAt = index + 1;
				return delegate
				{
					list.Insert(insertAt, defaultValue);
					return list;
				};
			}
		}
		if (EditorLayout.MiniButtonRight("-"))
		{
			int removeAt = index;
			return delegate
			{
				list.RemoveAt(removeAt);
				return list;
			};
		}
		return null;
	}

	private IList drawAddElement(IList list, string memberName, Type elementType)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorGUILayout.LabelField(memberName, "empty", (GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (EditorLayout.MiniButton("add " + SerializationTypeExtension.ShortTypeName(SerializationTypeExtension.ToCompilableString(elementType))) && EntityDrawer.CreateDefault(elementType, out var defaultValue))
		{
			list.Add(defaultValue);
		}
		EditorGUILayout.EndHorizontal();
		return list;
	}
}
