using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class ColorTypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		return type == typeof(Color);
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		return EditorGUILayout.ColorField(memberName, (Color)value, (GUILayoutOption[])(object)new GUILayoutOption[0]);
	}
}
