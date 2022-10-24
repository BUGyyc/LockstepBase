using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class AnimationCurveTypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		return type == typeof(AnimationCurve);
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		return EditorGUILayout.CurveField(memberName, (AnimationCurve)value, (GUILayoutOption[])(object)new GUILayoutOption[0]);
	}
}
