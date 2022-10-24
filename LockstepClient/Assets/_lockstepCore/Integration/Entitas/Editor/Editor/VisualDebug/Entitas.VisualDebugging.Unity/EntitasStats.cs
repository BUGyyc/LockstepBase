using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Utils;
using Entitas.CodeGeneration.Attributes;
using UnityEditor;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity.Editor;

public static class EntitasStats
{
	[MenuItem("Tools/Entitas/Show Stats", false, 200)]
	public static void ShowStats()
	{
		string text = string.Join("\n", (from kv in GetStats()
			select kv.Key + ": " + kv.Value).ToArray());
		Debug.Log((object)text);
		EditorUtility.DisplayDialog("Entitas Stats", text, "Close");
	}

	public static Dictionary<string, int> GetStats()
	{
		Type[] allTypes = AppDomainExtension.GetAllTypes(AppDomain.CurrentDomain);
		Type[] array = allTypes.Where((Type type) => InterfaceTypeExtension.ImplementsInterface<IComponent>(type)).ToArray();
		Type[] array2 = allTypes.Where(isSystem).ToArray();
		Dictionary<string, int> contexts = getContexts(array);
		Dictionary<string, int> dictionary = new Dictionary<string, int>
		{
			{ "Total Components", array.Length },
			{ "Systems", array2.Length }
		};
		foreach (KeyValuePair<string, int> item in contexts)
		{
			dictionary.Add("Components in " + item.Key, item.Value);
		}
		return dictionary;
	}

	private static Dictionary<string, int> getContexts(Type[] components)
	{
		return components.Aggregate(new Dictionary<string, int>(), delegate(Dictionary<string, int> contexts, Type type)
		{
			string[] contextNamesOrDefault = getContextNamesOrDefault(type);
			foreach (string key in contextNamesOrDefault)
			{
				if (!contexts.ContainsKey(key))
				{
					contexts.Add(key, 0);
				}
				contexts[key]++;
			}
			return contexts;
		});
	}

	private static string[] getContextNames(Type type)
	{
		return (from attr in Attribute.GetCustomAttributes(type).OfType<ContextAttribute>()
			select attr.contextName).ToArray();
	}

	private static string[] getContextNamesOrDefault(Type type)
	{
		string[] array = getContextNames(type);
		if (array.Length == 0)
		{
			array = new string[1] { "Default" };
		}
		return array;
	}

	private static bool isSystem(Type type)
	{
		if (InterfaceTypeExtension.ImplementsInterface<ISystem>(type) && type != typeof(ReactiveSystem<>) && type != typeof(MultiReactiveSystem<, >) && type != typeof(Systems) && type != typeof(DebugSystems) && type != typeof(JobSystem<>))
		{
			return type.FullName != "Feature";
		}
		return false;
	}
}
