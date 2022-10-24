using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class DateTimeTypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		return type == typeof(DateTime);
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		string text = value.ToString();
		string text2 = EditorGUILayout.TextField(memberName, text, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (!(text2 != text))
		{
			return value;
		}
		return DateTime.Parse(text2);
	}
}
