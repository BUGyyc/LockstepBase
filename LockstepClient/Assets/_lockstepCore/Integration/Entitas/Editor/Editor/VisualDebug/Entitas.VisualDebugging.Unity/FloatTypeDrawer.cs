using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class FloatTypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		return type == typeof(float);
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		return EditorGUILayout.FloatField(memberName, (float)value, (GUILayoutOption[])(object)new GUILayoutOption[0]);
	}
}