using System;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class UnityObjectTypeDrawer : ITypeDrawer
{
	public bool HandlesType(Type type)
	{
		if (type != typeof(Object))
		{
			return type.IsSubclassOf(typeof(Object));
		}
		return true;
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		return EditorGUILayout.ObjectField(memberName, (Object)value, memberType, true, (GUILayoutOption[])(object)new GUILayoutOption[0]);
	}
}
