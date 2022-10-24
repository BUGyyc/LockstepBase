using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class BoolTypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		return type == typeof(bool);
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		return EditorGUILayout.Toggle(memberName, (bool)value, (GUILayoutOption[])(object)new GUILayoutOption[0]);
	}
}
