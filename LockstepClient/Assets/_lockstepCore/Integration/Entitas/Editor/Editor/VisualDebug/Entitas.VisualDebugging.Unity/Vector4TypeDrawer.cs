using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class Vector4TypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		return type == typeof(Vector4);
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		return EditorGUILayout.Vector4Field(memberName, (Vector4)value, (GUILayoutOption[])(object)new GUILayoutOption[0]);
	}
}
