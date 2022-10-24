using System.Collections.Generic;

namespace Entitas.CodeGeneration.Plugins;

public static class ContextsComponentDataExtension
{
	public const string COMPONENT_CONTEXTS = "Component.ContextNames";

	public static string[] GetContextNames(this ComponentData data)
	{
		return (string[])((Dictionary<string, object>)(object)data)["Component.ContextNames"];
	}

	public static void SetContextNames(this ComponentData data, string[] contextNames)
	{
		((Dictionary<string, object>)(object)data)["Component.ContextNames"] = contextNames;
	}
}
