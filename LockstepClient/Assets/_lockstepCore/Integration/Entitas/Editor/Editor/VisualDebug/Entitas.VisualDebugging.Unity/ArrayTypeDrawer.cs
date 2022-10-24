using System;
using System.Collections;
using DesperateDevs.Unity.Editor;
using DesperateDevs.Utils;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class ArrayTypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		return type.IsArray;
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		Array array = (Array)value;
		Type elementType = memberType.GetElementType();
		int indentLevel = EditorGUI.get_indentLevel();
		if (array.Rank == 1)
		{
			array = drawRank1(array, memberName, elementType, indentLevel, target);
		}
		else if (array.Rank == 2)
		{
			array = drawRank2(array, memberName, elementType, target);
		}
		else if (array.Rank == 3)
		{
			array = drawRank3(array, memberName, elementType, target);
		}
		EditorGUI.set_indentLevel(indentLevel);
		return array;
	}

	private Array drawRank1(Array array, string memberName, Type elementType, int indent, object target)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		int length = array.GetLength(0);
		if (length == 0)
		{
			array = drawAddElement(array, memberName, elementType);
		}
		else
		{
			EditorGUILayout.LabelField(memberName, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		}
		EditorGUI.set_indentLevel(indent + 1);
		Func<Array> func = null;
		for (int i = 0; i < length; i++)
		{
			int localIndex = i;
			EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
			EntityDrawer.DrawObjectMember(elementType, memberName + "[" + localIndex + "]", array.GetValue(localIndex), target, delegate(object newComponent, object newValue)
			{
				array.SetValue(newValue, localIndex);
			});
			Func<Array> func2 = drawEditActions(array, elementType, localIndex);
			if (func2 != null)
			{
				func = func2;
			}
			EditorGUILayout.EndHorizontal();
		}
		if (func != null)
		{
			array = func();
		}
		return array;
	}

	private Array drawAddElement(Array array, string memberName, Type elementType)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		EditorGUILayout.LabelField(memberName, "empty", (GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (EditorLayout.MiniButton("add " + SerializationTypeExtension.ShortTypeName(SerializationTypeExtension.ToCompilableString(elementType))) && EntityDrawer.CreateDefault(elementType, out var defaultValue))
		{
			Array array2 = Array.CreateInstance(elementType, 1);
			array2.SetValue(defaultValue, 0);
			array = array2;
		}
		EditorGUILayout.EndHorizontal();
		return array;
	}

	private Array drawRank2(Array array, string memberName, Type elementType, object target)
	{
		EditorGUILayout.LabelField(memberName, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		for (int i = 0; i < array.GetLength(0); i++)
		{
			int localIndex1 = i;
			for (int j = 0; j < array.GetLength(1); j++)
			{
				int localIndex2 = j;
				EntityDrawer.DrawObjectMember(elementType, memberName + "[" + localIndex1 + ", " + localIndex2 + "]", array.GetValue(localIndex1, localIndex2), target, delegate(object newComponent, object newValue)
				{
					array.SetValue(newValue, localIndex1, localIndex2);
				});
			}
			EditorGUILayout.Space();
		}
		return array;
	}

	private Array drawRank3(Array array, string memberName, Type elementType, object target)
	{
		EditorGUILayout.LabelField(memberName, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		for (int i = 0; i < array.GetLength(0); i++)
		{
			int localIndex1 = i;
			for (int j = 0; j < array.GetLength(1); j++)
			{
				int localIndex2 = j;
				for (int k = 0; k < array.GetLength(2); k++)
				{
					int localIndex3 = k;
					EntityDrawer.DrawObjectMember(elementType, memberName + "[" + localIndex1 + ", " + localIndex2 + " ," + localIndex3 + "]", array.GetValue(localIndex1, localIndex2, localIndex3), target, delegate(object newComponent, object newValue)
					{
						array.SetValue(newValue, localIndex1, localIndex2, localIndex3);
					});
				}
				EditorGUILayout.Space();
			}
			EditorGUILayout.Space();
		}
		return array;
	}

	private static Func<Array> drawEditActions(Array array, Type elementType, int index)
	{
		if (EditorLayout.MiniButtonLeft("↑") && index > 0)
		{
			return delegate
			{
				int index3 = index - 1;
				object value2 = array.GetValue(index3);
				array.SetValue(array.GetValue(index), index3);
				array.SetValue(value2, index);
				return array;
			};
		}
		if (EditorLayout.MiniButtonMid("↓") && index < array.Length - 1)
		{
			return delegate
			{
				int index2 = index + 1;
				object value = array.GetValue(index2);
				array.SetValue(array.GetValue(index), index2);
				array.SetValue(value, index);
				return array;
			};
		}
		if (EditorLayout.MiniButtonMid("+"))
		{
			if (EntityDrawer.CreateDefault(elementType, out var defaultValue))
			{
				return () => arrayInsertAt(array, elementType, defaultValue, index + 1);
			}
		}
		if (EditorLayout.MiniButtonRight("-"))
		{
			return () => arrayRemoveAt(array, elementType, index);
		}
		return null;
	}

	private static Array arrayRemoveAt(Array array, Type elementType, int removeAt)
	{
		ArrayList arrayList = new ArrayList(array);
		arrayList.RemoveAt(removeAt);
		return arrayList.ToArray(elementType);
	}

	private static Array arrayInsertAt(Array array, Type elementType, object value, int insertAt)
	{
		ArrayList arrayList = new ArrayList(array);
		arrayList.Insert(insertAt, value);
		return arrayList.ToArray(elementType);
	}
}
