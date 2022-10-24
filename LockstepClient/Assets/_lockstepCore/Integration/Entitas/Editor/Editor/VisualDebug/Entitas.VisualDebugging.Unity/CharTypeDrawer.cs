using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class CharTypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		return type == typeof(char);
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		string text = EditorGUILayout.TextField(memberName, ((char)value).ToString(), (GUILayoutOption[])(object)new GUILayoutOption[0]);
		return (text.Length > 0) ? text[0] : '\0';
	}
}
