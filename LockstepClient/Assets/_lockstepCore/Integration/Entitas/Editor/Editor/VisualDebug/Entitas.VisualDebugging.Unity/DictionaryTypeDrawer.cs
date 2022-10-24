using System;
using System.Collections;
using System.Collections.Generic;
using DesperateDevs.Unity.Editor;
using DesperateDevs.Utils;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public class DictionaryTypeDrawer : ITypeDrawer
{
	private static Dictionary<Type, string> _keySearchTexts = new Dictionary<Type, string>();

	public bool HandlesType(Type type)
	{
		if (type.IsGenericType)
		{
			return type.GetGenericTypeDefinition() == typeof(Dictionary<, >);
		}
		return false;
	}

	public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		IDictionary dictionary = (IDictionary)value;
		Type type = memberType.GetGenericArguments()[0];
		Type type2 = memberType.GetGenericArguments()[1];
		Type type3 = target.GetType();
		if (!_keySearchTexts.ContainsKey(type3))
		{
			_keySearchTexts.Add(type3, string.Empty);
		}
		EditorGUILayout.BeginHorizontal((GUILayoutOption[])(object)new GUILayoutOption[0]);
		if (dictionary.Count == 0)
		{
			EditorGUILayout.LabelField(memberName, "empty", (GUILayoutOption[])(object)new GUILayoutOption[0]);
			_keySearchTexts[type3] = string.Empty;
		}
		else
		{
			EditorGUILayout.LabelField(memberName, (GUILayoutOption[])(object)new GUILayoutOption[0]);
		}
		string text = SerializationTypeExtension.ShortTypeName(SerializationTypeExtension.ToCompilableString(type));
		string text2 = SerializationTypeExtension.ShortTypeName(SerializationTypeExtension.ToCompilableString(type2));
		if (EditorLayout.MiniButton("new <" + text + ", " + text2 + ">") && EntityDrawer.CreateDefault(type, out var defaultValue) && EntityDrawer.CreateDefault(type2, out var defaultValue2))
		{
			dictionary[defaultValue] = defaultValue2;
		}
		EditorGUILayout.EndHorizontal();
		if (dictionary.Count > 0)
		{
			int indentLevel = EditorGUI.get_indentLevel();
			EditorGUI.set_indentLevel(indentLevel + 1);
			if (dictionary.Count > 5)
			{
				EditorGUILayout.Space();
				_keySearchTexts[type3] = EditorLayout.SearchTextField(_keySearchTexts[type3]);
			}
			EditorGUILayout.Space();
			ArrayList arrayList = new ArrayList(dictionary.Keys);
			for (int i = 0; i < arrayList.Count; i++)
			{
				object key = arrayList[i];
				if (!EditorLayout.MatchesSearchString(key.ToString().ToLower(), _keySearchTexts[type3].ToLower()))
				{
					continue;
				}
				EntityDrawer.DrawObjectMember(type, "key", key, target, delegate(object newComponent, object newValue)
				{
					object value2 = dictionary[key];
					dictionary.Remove(key);
					if (newValue != null)
					{
						dictionary[newValue] = value2;
					}
				});
				EntityDrawer.DrawObjectMember(type2, "value", dictionary[key], target, delegate(object newComponent, object newValue)
				{
					dictionary[key] = newValue;
				});
				EditorGUILayout.Space();
			}
			EditorGUI.set_indentLevel(indentLevel);
		}
		return dictionary;
	}
}
